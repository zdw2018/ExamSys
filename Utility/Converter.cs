using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class Converter
    {
        public static DataTable ExcelToDataSet(string filepath)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Title");
            dt.Columns.Add("OptionA");
            dt.Columns.Add("OptionB");
            dt.Columns.Add("OptionC");
            dt.Columns.Add("OptionD");
            dt.Columns.Add("RightOption");
            dt.Columns.Add("Analyze");
            IWorkbook workbook = WorkbookFactory.Create(filepath);
            ISheet sheet = workbook.GetSheetAt(0);//获取第一个工作薄

            //IRow row = (IRow)sheet.GetRow(0);//获取第一行
            int Temp = 0;
            foreach (IRow item in sheet)
            {
                if(Temp!=0)
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if(item.GetCell(i)!=null)
                        {
                            dr[i] = item.GetCell(i).ToString();
                        }
                        else
                        {
                            dr[i] = "";
                        }
                                              
                    }
                    dt.Rows.Add(dr);
                }
                Temp = 1;

            }
            return dt;          
        }
    }
}
