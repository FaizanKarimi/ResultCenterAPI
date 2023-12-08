using System;
using System.Data;
using System.Reflection;

namespace betway_result_center_api.Repository
{
    public static class ReflectPropertyInfo
    {
        public static TEntity ReflectType<TEntity>(DataRow dr) where TEntity : class, new()
        {
            TEntity instanceToPopulate = new TEntity();
            PropertyInfo[] propertyInfos = typeof(TEntity).GetProperties();
            foreach (PropertyInfo _propertyInfo in propertyInfos)
            {
                try
                {
                    Type propertyType = _propertyInfo.PropertyType;
                    string propertyName = _propertyInfo.Name;

                    object dbValue = dr[propertyName];
                    if (dbValue != DBNull.Value)
                        _propertyInfo.SetValue(instanceToPopulate, dbValue, null);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return instanceToPopulate;
        }

        public static void SetPropertyObject(object setVal, int index, object obj)
        {
            PropertyInfo[] propertyInfos = obj.GetType().GetProperties();
            int counter = 0;
            foreach (PropertyInfo _propertyInfo in propertyInfos)
            {
                if (counter == index)
                {
                    Type propertyType = _propertyInfo.PropertyType;
                    string propertyName = _propertyInfo.Name;
                    _propertyInfo.SetValue(obj, setVal);
                    break;
                }
                counter++;
            }
        }
    }
}