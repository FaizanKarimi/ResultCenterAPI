using betway_result_center_api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Caching;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace betway_result_center_api.Filters
{
    public class CacheFilter : ActionFilterAttribute
    {
        #region Private Members
        private int _timespan = 30;
        private int _clientTimeSpan = 15;
        private bool _anonymousOnly;
        private string _cachekey;
        private static readonly ObjectCache WebApiCache = MemoryCache.Default;
        #endregion

        #region Public Methods
        public CacheFilter(bool anonymousOnly)
        {
            _anonymousOnly = anonymousOnly;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext != null)
            {
                if (_IsCacheable(actionContext))
                {
                    GlobalParametersModel model = (GlobalParametersModel)actionContext.ActionArguments.FirstOrDefault().Value;
                    if (!model.IsLive)
                    {
                        _cachekey = string.Join("/", new string[] { actionContext.Request.RequestUri.AbsolutePath, _GetRequestData(model) });
                        if (WebApiCache.Contains(_cachekey))
                        {
                            var cacheObject = (string)WebApiCache.Get(_cachekey);
                            if (cacheObject != null)
                            {
                                var obj = JsonConvert.DeserializeObject<ResponseModel>(cacheObject);
                                obj.message = "Data returned from Cache";
                                string returnedObject = JsonConvert.SerializeObject(obj);
                                actionContext.Response = actionContext.Request.CreateResponse();
                                actionContext.Response.Content = new StringContent(returnedObject);
                                var contenttype = (MediaTypeHeaderValue)WebApiCache.Get(_cachekey + ":response-ct");
                                if (contenttype == null)
                                    contenttype = new MediaTypeHeaderValue("application/json");
                                actionContext.Response.Content.Headers.ContentType = contenttype;
                                actionContext.Response.Headers.CacheControl = _SetClientCache();
                                return;
                            }
                        }
                    }
                }
            }
            else
                throw new ArgumentNullException("actionContext");
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            try
            {
                if (!string.IsNullOrEmpty(_cachekey))
                {
                    if (!(WebApiCache.Contains(_cachekey)))
                    {
                        var body = actionExecutedContext.Response.Content.ReadAsStringAsync().Result;
                        WebApiCache.Add(_cachekey, body, DateTime.Now.AddSeconds(_timespan));
                        WebApiCache.Add(_cachekey + ":response-ct", actionExecutedContext.Response.Content.Headers.ContentType, DateTime.Now.AddSeconds(_timespan));
                    }
                    if (_IsCacheable(actionExecutedContext.ActionContext))
                        actionExecutedContext.ActionContext.Response.Headers.CacheControl = _SetClientCache();
                }
            }
            catch (Exception)
            {
                throw actionExecutedContext.Exception;
            }
        }
        #endregion

        #region Private Methods
        private string _GetRequestData(GlobalParametersModel globalParameterModel)
        {
            string value = string.Empty;
            List<object> parameters = new List<object>();
            Type myType = globalParameterModel.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(globalParameterModel, null);
                string propertyName = prop.Name;
                object parameter = propertyName + " : " + propValue;
                parameters.Add(parameter);
            }
            value = string.Join("/", parameters);
            return value;
        }

        private CacheControlHeaderValue _SetClientCache()
        {
            var cachecontrol = new CacheControlHeaderValue();
            cachecontrol.MaxAge = TimeSpan.FromSeconds(_clientTimeSpan);
            cachecontrol.MustRevalidate = true;
            return cachecontrol;
        }

        private bool _IsCacheable(HttpActionContext actionContext)
        {
            bool isCacheAble = false;
            if (_timespan > 0 && _clientTimeSpan > 0)
            {
                if (_anonymousOnly)
                    if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
                        isCacheAble = false;
                if (actionContext.Request.Method == HttpMethod.Post) isCacheAble = true;
            }
            else
                throw new InvalidOperationException("Wrong Arguments");
            return isCacheAble;
        }
        #endregion
    }
}