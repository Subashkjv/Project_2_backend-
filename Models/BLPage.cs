using System.Data;
using System.Text;

namespace TestAPI.Models
{
    public class BLPage
    {
        public class FormDataModel
        {
            public IFormFile File { get; set; }
        }
        public class EmployeeDetails
        {
            public string Emp_ID { get; set; }
            public string username { get; set; }
            public DateTime DOB { get; set; }
            public string TeamName { get; set; }
            public int TeamSize { get; set; }
        }
        public static string GetAndCreateCSV()
        {
            try
            {


                DataTable dt_Load = DBPage.GetDataTable();

                if (dt_Load.Rows.Count > 0)
                {
                    string filePath = @"D:\Subash\file\out.csv";
                    //if (!File.Exists(filePath))
                    //{
                    //    File.Create(filePath);

                    //}
                    using (StreamWriter strm = new StreamWriter(filePath))
                    {
                        for (int i = 0; i < dt_Load.Columns.Count; i++)
                        {
                            strm.Write(dt_Load.Columns[i]);
                            if (i < dt_Load.Columns.Count - 1)
                                strm.Write(",");
                        }
                        strm.WriteLine();

                        for (int i = 0; i < dt_Load.Rows.Count; i++)
                        {
                            for (int j = 0; j < dt_Load.Columns.Count; j++)
                            {
                                strm.Write(dt_Load.Rows[i][j]);
                                if (j < dt_Load.Columns.Count - 1)
                                    strm.Write(",");
                            }
                            strm.WriteLine();
                        }

                    }
                }
                else
                {
                    return "No Data Found";
                }
            }
            catch (Exception Ex)
            {

                return Ex.Message;
            }
            return "Success";
        }

        public static string insertCSVtoDatatable(FormDataModel model)
        {
            try
            {
                if (model.File != null && model.File.Length > 0)
                {
                    DataTable dataTable = new DataTable();


                    using (var reader = new StreamReader(model.File.OpenReadStream()))
                    {

                        dataTable.Columns.Add("Emp_ID", typeof(int)); // Adjust types as per your schema
                        dataTable.Columns.Add("username", typeof(string));
                        dataTable.Columns.Add("DOB", typeof(DateTime));
                        dataTable.Columns.Add("TeamName", typeof(string));
                        dataTable.Columns.Add("TeamSize", typeof(int));
                        int i =0;
                        while (reader.Peek() >= 0)
                        {
                            var line = reader.ReadLine();
                            var arraylist = line.Split(',');
                           if(i>0)
                            dataTable.Rows.Add(Convert.ToString(arraylist[0]), Convert.ToString(arraylist[1]), Convert.ToDateTime(Convert.ToString(arraylist[2])), Convert.ToString(arraylist[3]), Convert.ToInt32(Convert.ToString(arraylist[4])));
                           i++;

                        }


                    }
                    if (dataTable.Rows.Count > 0)
                    {
                        DBPage.BulkInsert(dataTable);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return "success";
        }
    }
}
