using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;

namespace betway_result_center_api.Repository
{
    public class DBManager
    {
        #region Public Methods        
        public static List<TEntity> Execute<TEntity>(string sql, object parameters = null) where TEntity : class, new()
        {
            List<SqlParameter> sqlParamaters = new List<SqlParameter>();
            if (parameters != null)
            {
                Type type = parameters.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties();
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    string parameterName = propertyInfo.Name;
                    object parameterValue = propertyInfo.GetValue(parameters);
                    sqlParamaters.Add(new SqlParameter(parameterName, parameterValue));
                }
            }

            DBContext dbContext = new DBContext();
            return dbContext.ExecuteDisconnected<TEntity>(sql, sqlParamaters.ToArray());
        }

        public static TEntityOut Execute<TEntityOne, TEntityTwo, TEntityOut>(string sql, object parameters = null)
            where TEntityOne : class, new()
            where TEntityTwo : class, new()
            where TEntityOut : class, new()
        {
            List<SqlParameter> sqlParamaters = new List<SqlParameter>();
            if (parameters != null)
            {
                Type type = parameters.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties();
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    string parameterName = propertyInfo.Name;
                    object parameterValue = propertyInfo.GetValue(parameters);
                    sqlParamaters.Add(new SqlParameter(parameterName, parameterValue));
                }
            }

            DBContext dbContext = new DBContext();
            return dbContext.ExecuteDisconnected<TEntityOne, TEntityTwo, TEntityOut>(sql, sqlParamaters.ToArray());
        }

        public static TEntityOut Execute<TEntityOne, TEntityTwo, TEntityThree, TEntityOut>(string sql, object parameters = null)
            where TEntityOne : class, new()
            where TEntityTwo : class, new()
            where TEntityThree : class, new()
            where TEntityOut : class, new()
        {
            List<SqlParameter> sqlParamaters = new List<SqlParameter>();
            if (parameters != null)
            {
                Type type = parameters.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties();
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    string parameterName = propertyInfo.Name;
                    object parameterValue = propertyInfo.GetValue(parameters);
                    sqlParamaters.Add(new SqlParameter(parameterName, parameterValue));
                }
            }

            DBContext dbContext = new DBContext();
            return dbContext.ExecuteDisconnected<TEntityOne, TEntityTwo, TEntityThree, TEntityOut>(sql, sqlParamaters.ToArray());
        }

        public static TEntityOut Execute<TEntityOne, TEntityTwo, TEntityThree, TEntityFour, TEntityOut>(string sql, object parameters = null)
            where TEntityOne : class, new()
            where TEntityTwo : class, new()
            where TEntityThree : class, new()
            where TEntityFour : class, new()
            where TEntityOut : class, new()
        {
            List<SqlParameter> sqlParamaters = new List<SqlParameter>();
            if (parameters != null)
            {
                Type type = parameters.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties();
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    string parameterName = propertyInfo.Name;
                    object parameterValue = propertyInfo.GetValue(parameters);
                    sqlParamaters.Add(new SqlParameter(parameterName, parameterValue));
                }
            }

            DBContext dbContext = new DBContext();
            return dbContext.ExecuteDisconnected<TEntityOne, TEntityTwo, TEntityThree, TEntityFour, TEntityOut>(sql, sqlParamaters.ToArray());
        }

        public static TEntityOut Execute<TEntityOne, TEntityTwo, TEntityThree, TEntityFour, TEntityFive, TEntityOut>(string sql, object parameters = null)
            where TEntityOne : class, new()
            where TEntityTwo : class, new()
            where TEntityThree : class, new()
            where TEntityFour : class, new()
            where TEntityFive : class, new()
            where TEntityOut : class, new()
        {
            List<SqlParameter> sqlParamaters = new List<SqlParameter>();
            if (parameters != null)
            {
                Type type = parameters.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties();
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    string parameterName = propertyInfo.Name;
                    object parameterValue = propertyInfo.GetValue(parameters);
                    sqlParamaters.Add(new SqlParameter(parameterName, parameterValue));
                }
            }

            DBContext dbContext = new DBContext();
            return dbContext.ExecuteDisconnected<TEntityOne, TEntityTwo, TEntityThree, TEntityFour, TEntityFive, TEntityOut>(sql, sqlParamaters.ToArray());
        }

        public static TEntityOut Execute<TEntityOne, TEntityTwo, TEntityThree, TEntityFour, TEntityFive, TEntitySix, TEntityOut>(string sql, object parameters = null)
             where TEntityOne : class, new()
             where TEntityTwo : class, new()
             where TEntityThree : class, new()
             where TEntityFour : class, new()
             where TEntityFive : class, new()
             where TEntitySix : class, new()
             where TEntityOut : class, new()
        {
            List<SqlParameter> sqlParamaters = new List<SqlParameter>();
            if (parameters != null)
            {
                Type type = parameters.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties();
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    string parameterName = propertyInfo.Name;
                    object parameterValue = propertyInfo.GetValue(parameters);
                    sqlParamaters.Add(new SqlParameter(parameterName, parameterValue));
                }
            }

            DBContext dbContext = new DBContext();
            return dbContext.ExecuteDisconnected<TEntityOne, TEntityTwo, TEntityThree, TEntityFour, TEntityFive, TEntitySix, TEntityOut>(sql, sqlParamaters.ToArray());
        }
        #endregion
    }
}