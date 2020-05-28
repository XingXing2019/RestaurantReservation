using System;
using System.Data;
using System.IO;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using NUnit.Framework;

namespace UnitTest
{
    public abstract class TestBase
    {
        protected readonly string connectionString;
        private readonly Logger logger;

        protected TestBase()
        {
            logger = LogManager.GetCurrentClassLogger();
            var jsonPath = GetFilePath("", "config.json");
            connectionString = GetJsonContent(jsonPath, "DefaultConnection");
        }

        private string GetFilePath(string folderName, string fileName)
        {
            try
            {
                var directory = Path.GetDirectoryName(GetType().Assembly.Location);
                var path = directory.Substring(0, directory.Length - 24);
                path += string.IsNullOrEmpty(folderName) ? $"\\{fileName}" : $"\\{folderName}\\{fileName}";
                Assert.IsTrue(File.Exists(path));
                return path;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                logger.Error(ex);
                throw;
            }
        }

        private string GetJsonContent(string jsonPath, string contentName)
        {
            using (var jsonFile = File.OpenText(jsonPath))
            {
                using (var reader = new JsonTextReader(jsonFile))
                {
                    JObject jsonObject = (JObject)JToken.ReadFrom(reader);
                    return jsonObject[contentName].ToString();
                }
            }
        }

        protected DataTable RemoteQueryDatabase(string query)
        {
            try
            {
                var dataTable = new DataTable();
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    if (sqlConnection.State != ConnectionState.Open)
                        sqlConnection.Open();
                    var command = sqlConnection.CreateCommand();
                    command.CommandText = query;
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        dataTable.Load(reader);
                        reader.Close();
                    }
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                logger.Error(ex);
                return null;
            }
        }

        protected void RemoteSetupDatabase(string queryFileName)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    if (sqlConnection.State != ConnectionState.Open)
                        sqlConnection.Open();

                    var filePath = GetFilePath("QueryFile", queryFileName);
                    Assert.IsTrue(File.Exists(filePath));
                    var query = File.ReadAllText(filePath);
                    var command = sqlConnection.CreateCommand();
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                logger.Error(ex);
            }
        }
    }
}
