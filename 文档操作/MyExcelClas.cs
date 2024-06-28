using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace 文档操作
{
    class MyExcelClas
    {
        /*这个操作适合做数据的记录及查询，如果要增删改查的话还是数据库更方便*/
        

        /// <summary>
        /// 从Excel读取数据到DataTable
        /// </summary>
        /// <param name="filename">Excel文件名</param>
        /// <param name="sheename">表格名</param>
        /// <param name="isFirstRowColum">第一行是否为标题</param>
        /// <returns></returns>
        public static DataTable ExcletoDataTable(string filename,string sheename,bool isFirstRowColum)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            FileStream fs = null;
            int startRow = 0;
            IWorkbook workbook = null;
            int cellCount = 0;//列数
            int rowCount = 0;//行数
            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                
                if (filename.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (filename.IndexOf(".xls") > 0)
                {
                    workbook = new HSSFWorkbook(fs);
                }
                if (sheename != null)
                {
                    //根据给定的sheet名称获取数据
                    sheet = workbook.GetSheet(sheename);
                }
                else
                {
                    //也可以根据sheet编号来获取数据
                    //获取第几个sheet表（此处表示如果没有给定sheet名称，默认是第一个sheet表）
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    //第一行最后一个cell的编号 即总的列数
                    cellCount = firstRow.LastCellNum;
                    //如果第一行是标题行
                    if (isFirstRowColum)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            //获取标题
                            DataColumn column = new DataColumn(firstRow.GetCell(i).StringCellValue);
                            data.Columns.Add(column);
                        }
                        //1（即第二行，第一行0从开始）
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                        startRow = sheet.FirstRowNum;

                    //最后一行的标号
                    rowCount = sheet.LastRowNum;

                    //循环遍历所有行
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null)
                        {
                            continue;
                        }
                        //将excel表每一行的数据添加到datatable的行中
                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null)
                            {
                                dataRow[j] = row.GetCell(j).ToString();
                            }
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                fs.Close();
                return data;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("读取Excel失败" + ex.Message);
                if (fs != null)
                    fs.Close();
                return null;
            }
        }

        /// <summary>
        /// 写数据到现有的Excel表格
        /// </summary>
        /// <param name="filename">Excel文件名</param>
        /// <param name="sheename">表格名</param>
        /// <returns></returns>
        public static bool ListToExcel(string filename, string sheename,List<List<string>> data)
        {
            ISheet sheet = null;
            IWorkbook workbook = null;
            IRow row = null;
            int lastRow = 0;
            int count = data.Count;//新增数据的行数
            FileStream fs = null;
            try
            {
                //lastRow = readExcelRowCount(filename, sheename);
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read,FileShare.ReadWrite);

                if (filename.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (filename.IndexOf(".xls") > 0)
                {
                    workbook = new HSSFWorkbook(fs);
                }
                if (sheename != null)
                {
                    //根据给定的sheet名称获取数据
                    sheet = workbook.GetSheet(sheename);
                    lastRow = sheet.LastRowNum + 1;
                }

                //for (int i = 0; i < count; i++)
                //{
                //    row = sheet.CreateRow(i);
                //    for (int j = 0; j < data[i].Count; j++)
                //    {
                //        row.CreateCell(j).SetCellValue(data[i][j]);
                //    }
                //}
                row = sheet.CreateRow(lastRow);
                row.CreateCell(0).SetCellValue(data[0][0]);
                row.CreateCell(1).SetCellValue(data[0][1]);
                row.CreateCell(2).SetCellValue(data[0][2]);

                Export(workbook, filename);

                //workbook.Write(fs);
                return true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("写入Excel失败" + ex.Message);
                if(fs!=null)
                    fs.Close();
                return false;
            }
        }

        private static bool Export(IWorkbook workbook, string path)
        {
            try
            {
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    file.Delete();
                    file = new FileInfo(path);
                }

                // 写入 ,创建其支持存储区为内存的流
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //写入内存
                workbook.Write(ms);
                workbook = null;
                //
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
                //关闭
                ms.Close();
                //释放
                ms.Dispose();
                //
                fs.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //获取表格的总行数
        public static int readExcelRowCount(string filename, string sheename)
        {
            ISheet sheet = null;
            IWorkbook workbook = null;
            int lastRow = 0;
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);

            if (filename.IndexOf(".xlsx") > 0)
            {
                workbook = new XSSFWorkbook(fs);
            }
            else if (filename.IndexOf(".xls") > 0)
            {
                workbook = new HSSFWorkbook(fs);
            }
            if (sheename != null)
            {
                //根据给定的sheet名称获取数据
                sheet = workbook.GetSheet(sheename);
                lastRow = sheet.LastRowNum;
            }
            return lastRow;
        }
       
    }
}
