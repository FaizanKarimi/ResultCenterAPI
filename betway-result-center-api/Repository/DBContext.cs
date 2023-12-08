using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace betway_result_center_api.Repository
{
    public class DBContext
    {
        #region Private Properties
        private SqlConnection _connection { get; set; }
        private string _connectionString { get; set; }
        #endregion

        #region Public Constructor
        public DBContext()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["SportsDataDB"].ConnectionString;
            _connection = new SqlConnection(_connectionString);
        }
        #endregion

        #region Public Methods
        public List<TEntity> ExecuteDisconnected<TEntity>(string sql, SqlParameter[] parameters) where TEntity : class, new()
        {
            List<TEntity> entityList = new List<TEntity>();
            TEntity entity = new TEntity();
            try
            {
                using (DataTable dataTable = new DataTable())
                {
                    using (SqlCommand command = new SqlCommand(sql, _connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = _connection;
                        if (parameters.Count() != 0)
                            command.Parameters.AddRange(parameters);
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command))
                        {
                            sqlDataAdapter.Fill(dataTable);
                        }
                    }
                    foreach (DataRow _dataRow in dataTable.Rows)
                    {
                        var result = ReflectPropertyInfo.ReflectType<TEntity>(_dataRow);
                        entityList.Add(result);
                    }
                }
                return entityList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open || _connection.State == ConnectionState.Broken)
                {
                    _connection.Close();
                    _connection.Dispose();
                }
            }
        }

        public TEntityOut ExecuteDisconnected<TEntity1, TEntity2, TEntityOut>(string sql, SqlParameter[] parameters)
            where TEntity1 : class, new()
            where TEntity2 : class, new()
            where TEntityOut : class, new()
        {
            List<TEntity1> entityListOne = new List<TEntity1>();
            List<TEntity2> entityListTwo = new List<TEntity2>();
            TEntityOut returnModel = new TEntityOut();
            DataSet dataSet = new DataSet();
            try
            {
                using (SqlCommand command = new SqlCommand(sql, _connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = _connection;
                    if (parameters.Count() != 0)
                        command.Parameters.AddRange(parameters);
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command))
                    {
                        sqlDataAdapter.Fill(dataSet);
                    }
                }

                #region First Entity
                foreach (DataRow _dataRow in dataSet.Tables[0].Rows)
                {
                    var result = ReflectPropertyInfo.ReflectType<TEntity1>(_dataRow);
                    entityListOne.Add(result);
                }
                ReflectPropertyInfo.SetPropertyObject(entityListOne, 0, returnModel);
                #endregion

                #region Second Entity
                foreach (DataRow _dataRow in dataSet.Tables[1].Rows)
                {
                    var result = ReflectPropertyInfo.ReflectType<TEntity2>(_dataRow);
                    entityListTwo.Add(result);
                }
                ReflectPropertyInfo.SetPropertyObject(entityListTwo, 1, returnModel);
                #endregion

                return returnModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open || _connection.State == ConnectionState.Broken)
                {
                    _connection.Close();
                    _connection.Dispose();
                }
            }
        }

        public TEntityOut ExecuteDisconnected<TEntity1, TEntity2, TEntity3, TEntityOut>(string sql, SqlParameter[] parameters)
            where TEntity1 : class, new()
            where TEntity2 : class, new()
            where TEntity3 : class, new()
            where TEntityOut : class, new()
        {
            List<TEntity1> entityListOne = new List<TEntity1>();
            List<TEntity2> entityListTwo = new List<TEntity2>();
            List<TEntity3> entityListThree = new List<TEntity3>();
            TEntityOut returnModel = new TEntityOut();
            DataSet dataSet = new DataSet();
            try
            {
                using (SqlCommand command = new SqlCommand(sql, _connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = _connection;
                    if (parameters.Count() != 0)
                        command.Parameters.AddRange(parameters);
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command))
                    {
                        sqlDataAdapter.Fill(dataSet);
                    }
                }

                #region First Entity
                foreach (DataRow _dataRow in dataSet.Tables[0].Rows)
                {
                    var result = ReflectPropertyInfo.ReflectType<TEntity1>(_dataRow);
                    entityListOne.Add(result);
                }
                ReflectPropertyInfo.SetPropertyObject(entityListOne, 0, returnModel);
                #endregion

                #region Second Entity
                foreach (DataRow _dataRow in dataSet.Tables[1].Rows)
                {
                    var result = ReflectPropertyInfo.ReflectType<TEntity2>(_dataRow);
                    entityListTwo.Add(result);
                }
                ReflectPropertyInfo.SetPropertyObject(entityListTwo, 1, returnModel);
                #endregion

                #region Third Entity
                foreach (DataRow _dataRow in dataSet.Tables[2].Rows)
                {
                    var result = ReflectPropertyInfo.ReflectType<TEntity3>(_dataRow);
                    entityListThree.Add(result);
                }
                ReflectPropertyInfo.SetPropertyObject(entityListThree, 2, returnModel);
                #endregion

                return returnModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open || _connection.State == ConnectionState.Broken)
                {
                    _connection.Close();
                    _connection.Dispose();
                }
            }
        }

        public TEntityOut ExecuteDisconnected<TEntity1, TEntity2, TEntity3, TEntity4, TEntityOut>(string sql, SqlParameter[] parameters)
            where TEntity1 : class, new()
            where TEntity2 : class, new()
            where TEntity3 : class, new()
            where TEntity4 : class, new()
            where TEntityOut : class, new()
        {
            List<TEntity1> entityListOne = new List<TEntity1>();
            List<TEntity2> entityListTwo = new List<TEntity2>();
            List<TEntity3> entityListThree = new List<TEntity3>();
            List<TEntity4> entityListFour = new List<TEntity4>();
            TEntityOut returnModel = new TEntityOut();
            DataSet dataSet = new DataSet();
            try
            {
                using (SqlCommand command = new SqlCommand(sql, _connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = _connection;
                    if (parameters.Count() != 0)
                        command.Parameters.AddRange(parameters);
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command))
                    {
                        sqlDataAdapter.Fill(dataSet);
                    }
                }

                #region First Entity
                foreach (DataRow _dataRow in dataSet.Tables[0].Rows)
                {
                    var result = ReflectPropertyInfo.ReflectType<TEntity1>(_dataRow);
                    entityListOne.Add(result);
                }
                ReflectPropertyInfo.SetPropertyObject(entityListOne, 0, returnModel);
                #endregion

                #region Second Entity
                foreach (DataRow _dataRow in dataSet.Tables[1].Rows)
                {
                    var result = ReflectPropertyInfo.ReflectType<TEntity2>(_dataRow);
                    entityListTwo.Add(result);
                }
                ReflectPropertyInfo.SetPropertyObject(entityListTwo, 1, returnModel);
                #endregion

                #region Third Entity
                foreach (DataRow _dataRow in dataSet.Tables[2].Rows)
                {
                    var result = ReflectPropertyInfo.ReflectType<TEntity3>(_dataRow);
                    entityListThree.Add(result);
                }
                ReflectPropertyInfo.SetPropertyObject(entityListThree, 2, returnModel);
                #endregion

                #region Fourth Entity
                foreach (DataRow _dataRow in dataSet.Tables[3].Rows)
                {
                    var result = ReflectPropertyInfo.ReflectType<TEntity4>(_dataRow);
                    entityListFour.Add(result);
                }
                ReflectPropertyInfo.SetPropertyObject(entityListFour, 3, returnModel);
                #endregion

                return returnModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open || _connection.State == ConnectionState.Broken)
                {
                    _connection.Close();
                    _connection.Dispose();
                }
            }
        }

        public TEntityOut ExecuteDisconnected<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntityOut>(string sql, SqlParameter[] parameters)
            where TEntity1 : class, new()
            where TEntity2 : class, new()
            where TEntity3 : class, new()
            where TEntity4 : class, new()
            where TEntity5 : class, new()
            where TEntityOut : class, new()
        {
            List<TEntity1> entityListOne = new List<TEntity1>();
            List<TEntity2> entityListTwo = new List<TEntity2>();
            List<TEntity3> entityListThree = new List<TEntity3>();
            List<TEntity4> entityListFour = new List<TEntity4>();
            List<TEntity5> entityListFive = new List<TEntity5>();
            TEntityOut returnModel = new TEntityOut();
            DataSet dataSet = new DataSet();
            try
            {
                using (SqlCommand command = new SqlCommand(sql, _connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = _connection;
                    if (parameters.Count() != 0)
                        command.Parameters.AddRange(parameters);
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command))
                    {
                        sqlDataAdapter.Fill(dataSet);
                    }
                }

                #region First Entity
                foreach (DataRow _dataRow in dataSet.Tables[0].Rows)
                {
                    var result = ReflectPropertyInfo.ReflectType<TEntity1>(_dataRow);
                    entityListOne.Add(result);
                }
                ReflectPropertyInfo.SetPropertyObject(entityListOne, 0, returnModel);
                #endregion

                #region Second Entity
                foreach (DataRow _dataRow in dataSet.Tables[1].Rows)
                {
                    var result = ReflectPropertyInfo.ReflectType<TEntity2>(_dataRow);
                    entityListTwo.Add(result);
                }
                ReflectPropertyInfo.SetPropertyObject(entityListTwo, 1, returnModel);
                #endregion

                #region Third Entity
                foreach (DataRow _dataRow in dataSet.Tables[2].Rows)
                {
                    var result = ReflectPropertyInfo.ReflectType<TEntity3>(_dataRow);
                    entityListThree.Add(result);
                }
                ReflectPropertyInfo.SetPropertyObject(entityListThree, 2, returnModel);
                #endregion

                #region Fourth Entity
                foreach (DataRow _dataRow in dataSet.Tables[3].Rows)
                {
                    var result = ReflectPropertyInfo.ReflectType<TEntity4>(_dataRow);
                    entityListFour.Add(result);
                }
                ReflectPropertyInfo.SetPropertyObject(entityListFour, 3, returnModel);
                #endregion

                #region Fiveth Entity
                foreach (DataRow _dataRow in dataSet.Tables[4].Rows)
                {
                    var result = ReflectPropertyInfo.ReflectType<TEntity5>(_dataRow);
                    entityListFive.Add(result);
                }
                ReflectPropertyInfo.SetPropertyObject(entityListFive, 4, returnModel);
                #endregion

                return returnModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open || _connection.State == ConnectionState.Broken)
                {
                    _connection.Close();
                    _connection.Dispose();
                }
            }
        }

        public TEntityOut ExecuteDisconnected<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntityOut>(string sql, SqlParameter[] parameters)
            where TEntity1 : class, new()
            where TEntity2 : class, new()
            where TEntity3 : class, new()
            where TEntity4 : class, new()
            where TEntity5 : class, new()
            where TEntity6 : class, new()
            where TEntityOut : class, new()
        {
            List<TEntity1> entityListOne = new List<TEntity1>();
            List<TEntity2> entityListTwo = new List<TEntity2>();
            List<TEntity3> entityListThree = new List<TEntity3>();
            List<TEntity4> entityListFour = new List<TEntity4>();
            List<TEntity5> entityListFive = new List<TEntity5>();
            List<TEntity6> entityListSix = new List<TEntity6>();

            TEntityOut returnModel = new TEntityOut();
            DataSet dataSet = new DataSet();
            try
            {
                using (SqlCommand command = new SqlCommand(sql, _connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = _connection;
                    if (parameters.Count() != 0)
                        command.Parameters.AddRange(parameters);
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command))
                    {
                        sqlDataAdapter.Fill(dataSet);
                    }
                }

                #region First Entity
                foreach (DataRow _dataRow in dataSet.Tables[0].Rows)
                {
                    var result = ReflectPropertyInfo.ReflectType<TEntity1>(_dataRow);
                    entityListOne.Add(result);
                }
                ReflectPropertyInfo.SetPropertyObject(entityListOne, 0, returnModel);
                #endregion

                #region Second Entity
                foreach (DataRow _dataRow in dataSet.Tables[1].Rows)
                {
                    var result = ReflectPropertyInfo.ReflectType<TEntity2>(_dataRow);
                    entityListTwo.Add(result);
                }
                ReflectPropertyInfo.SetPropertyObject(entityListTwo, 1, returnModel);
                #endregion

                #region Third Entity
                foreach (DataRow _dataRow in dataSet.Tables[2].Rows)
                {
                    var result = ReflectPropertyInfo.ReflectType<TEntity3>(_dataRow);
                    entityListThree.Add(result);
                }
                ReflectPropertyInfo.SetPropertyObject(entityListThree, 2, returnModel);
                #endregion

                #region Fourth Entity
                foreach (DataRow _dataRow in dataSet.Tables[3].Rows)
                {
                    var result = ReflectPropertyInfo.ReflectType<TEntity4>(_dataRow);
                    entityListFour.Add(result);
                }
                ReflectPropertyInfo.SetPropertyObject(entityListFour, 3, returnModel);
                #endregion

                #region Fiveth Entity
                foreach (DataRow _dataRow in dataSet.Tables[4].Rows)
                {
                    var result = ReflectPropertyInfo.ReflectType<TEntity5>(_dataRow);
                    entityListFive.Add(result);
                }
                ReflectPropertyInfo.SetPropertyObject(entityListFive, 4, returnModel);
                #endregion

                #region Sixth Entity
                foreach (DataRow _dataRow in dataSet.Tables[5].Rows)
                {
                    var result = ReflectPropertyInfo.ReflectType<TEntity6>(_dataRow);
                    entityListSix.Add(result);
                }
                ReflectPropertyInfo.SetPropertyObject(entityListFive, 5, returnModel);
                #endregion

                return returnModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open || _connection.State == ConnectionState.Broken)
                {
                    _connection.Close();
                    _connection.Dispose();
                }
            }
        }
        #endregion
    }
}