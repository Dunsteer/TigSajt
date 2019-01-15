using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TIGSajt.Models;

namespace TIGSajt.DB
{
    public partial class TeorijaIgaraContext
    {
        public async Task<List<StudentModel>> GetStatistics()
        {
            List<StudentModel> Statistics = new List<StudentModel>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Statistics";
#if DEBUG
                    cmd.Parameters.AddWithValue("@type", (short)eDbEntryType.Test);
#endif

                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {

                            Statistics.Add(new StudentModel()
                            {
                                StudentId = reader.GetInt64(0).ToString(),
                                Student = reader.GetString(1),
                                Points = reader.GetInt32(2).ToString(),
                                Score = reader.GetInt32(3).ToString(),
                                Win = reader.GetInt32(4).ToString(),
                                Draw = reader.GetInt32(5).ToString(),
                                Lost = reader.GetInt32(6).ToString()
                            });
                        }
                    }
                }

                conn.Close();
            }
            return Statistics;
        }

        public async Task<List<PerUserModel>> GetPerUser(long? userId)
        {
            var Statistics = new List<PerUserModel>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_PerUser";

                    cmd.Parameters.AddWithValue("@studentId", userId ?? null);
#if DEBUG
                    cmd.Parameters.AddWithValue("@type", (short)eDbEntryType.Test);
#endif

                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {

                            Statistics.Add(new PerUserModel()
                            {
                                HomeStudentId = reader.GetInt64(0).ToString(),
                                GuestStudentId = reader.GetInt64(1).ToString(),
                                HomeStudentName = reader.GetString(2),
                                GuestStudentName = reader.GetString(3),
                                Win = reader.GetInt32(4).ToString(),
                                Draw = reader.GetInt32(5).ToString(),
                                Lost = reader.GetInt32(6).ToString()
                            });
                        }
                    }
                }

                conn.Close();
            }
            return Statistics;
        }
    }
}
