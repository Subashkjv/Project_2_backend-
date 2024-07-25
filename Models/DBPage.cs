using System.Data;
using System.Data.SqlClient;
using static TestAPI.Models.BLPage;

namespace TestAPI.Models
{
    public class DBPage
    {

        public static DataTable GetDataTable()
        {
            try
            {
                DataTable dt = new DataTable();
                string COnnectionString = "Server=DEV8\\SQLEXPRESS;Database=MyDB;Trusted_Connection=True;";


                using (SqlConnection conn = new SqlConnection(COnnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("select top 100 * from StudentDetails(nolock)", conn))
                    {


                        using (SqlDataAdapter Adapter = new SqlDataAdapter(cmd))
                        {
                            Adapter.Fill(dt);
                        }


                    }

                }
                return dt;

            }
            catch (Exception Ex)
            {
                return new DataTable();
            }

        }

        

        public static void BulkInsert(DataTable dtEmployeeDetails)
        {
            try
            {
                DataTable dt = new DataTable();
                string COnnectionString = "Server=DEV8\\SQLEXPRESS;Database=MyDB;Trusted_Connection=True;";

                if (dtEmployeeDetails != null && dtEmployeeDetails.Rows.Count > 0)
                    using (SqlConnection conn = new SqlConnection(COnnectionString))
                    {
                        conn.Open();
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, null))
                        {
                            bulkCopy.DestinationTableName = "StudentDetails"; // Your SQL Table name

                            //Optional mappings. Not required, if your ‘Data Table’ column names match with ‘SQL Table’ column names
                            bulkCopy.ColumnMappings.Add("Emp_ID", "Emp_ID");
                            bulkCopy.ColumnMappings.Add("username", "username");
                            bulkCopy.ColumnMappings.Add("DOB", "DOB");
                            bulkCopy.ColumnMappings.Add("TeamName", "TeamName");
                            bulkCopy.ColumnMappings.Add("TeamSize", "TeamSize");
                            bulkCopy.WriteToServer(dtEmployeeDetails);
                           
                        }
                    }

            }
            catch (Exception Ex)
            {
            }

        }
    }
}
