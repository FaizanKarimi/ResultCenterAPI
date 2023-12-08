using betway_result_center_api.BLL;
using betway_result_center_api.Helpers;
using betway_result_center_api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace betway_result_center_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api")]
    public class UtilityController : ApiController
    {
        [Route("language")]
        [AcceptVerbs("POST")]
        public IHttpActionResult GetLanguage(GlobalParametersModel globalParametersModel)
        {
            ResponseModel responseModel = new ResponseModel();
            string languageCode = globalParametersModel.Language;
            string path = _GetFilePath(languageCode);

            using (StreamReader streamReader = new StreamReader(path))
            {
                string json = streamReader.ReadToEnd();
                Dictionary<object, string> languages = JsonConvert.DeserializeObject<Dictionary<object, string>>(json);
                languages.Add("Language", _GetUserLanguage(languageCode));
                responseModel.data = languages;
            }
            return Ok(responseModel);
        }

        [Route("live-match-for-sport")]
        [AcceptVerbs("POST")]
        public IHttpActionResult GetLiveSports()
        {
            ResponseModel ResponseModel = new ResponseModel();
            ResponseModel.data = CommonBLL.GetLiveSports();
            return Ok(ResponseModel);
        }

        #region Private Methods
        private string _GetUserLanguage(string languageCode)
        {
            string userLanguage = languageCode;
            return CommonHelper._GetUserLanguage(userLanguage);
        }

        private string _GetFilePath(string languageCode)
        {
            string path = string.Empty;
            string language = CommonHelper._GetUserLanguageFile(languageCode);
            if (!string.IsNullOrEmpty(language))
            {
                path = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "\\Languages\\", language.ToLower(), ".json");
                if (!File.Exists(path))
                    path = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "\\Languages\\", "en-us", ".json");
            }
            else
                path = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "\\Languages\\", "en-us", ".json");
            return path;
        }
        #endregion
    }
}