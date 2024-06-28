using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace 文档操作
{
    class MySQLClass1
    {
        //使用前要添加引用MySql.Data

        //"server=127.0.0.1;Uid=user;password=123;Database=zdjxh;Charset=utf8"
        //"Database=csyth;Data source=localhost;User Id=root;password=111111;pooling=false;CharSet=utf8;port=3306"
        //public const string CONN = "server=127.0.0.1;Uid=user;password=123;Database=zdjxh;Charset=utf8";
        private string CONN = "server=127.0.0.1;Uid=user;password=123;Database=zdjxh;Charset=utf8";

        public string errorstr = "";
        //public MySqlConnection con = new MySqlConnection(CONN);
        private MySqlConnection con = null;

        public MySQLClass1()
        {
            if (CONN != "")
                con.Open();
        }
        /// <summary>
        /// 打开数据库
        /// </summary>
        /// <param name="sqlname">数据库的名称</param>
        public MySQLClass1(string sqlname)
        {
            CONN = $"server=127.0.0.1;Uid=user;password=123;Database={sqlname};Charset=utf8";
            con = new MySqlConnection(CONN);
            con.Open();
        }

        ~MySQLClass1()
        {
            con.Close();
        }
        
        public bool ExecSql(string sqlstr, params MySqlParameter[] para)
        {
            using (MySqlCommand cmd = new MySqlCommand(sqlstr, con))
            {
                try
                {
                    if (para != null)
                    {
                        foreach (MySqlParameter p in para)
                        {
                            cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                        }
                    }
                    int rnum = cmd.ExecuteNonQuery();
                    Debug.WriteLine("返回结果：" + rnum);
                    return rnum >= 0;

                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    return false;
                }
            }
        }

        //获取数据库的内容以表格的形式返回
        public DataSet chaxunsql(string sqlstr, params MySqlParameter[] para)
        {
            using (MySqlCommand cmd = new MySqlCommand(sqlstr, con))
            {
                DataSet dt = new DataSet();
                try
                {
                    if (para != null)
                    {
                        foreach (MySqlParameter p in para)
                        {
                            cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                        }
                    }
                    MySqlDataAdapter mda = new MySqlDataAdapter(cmd);

                    mda.Fill(dt);
                    return dt;

                }
                catch (Exception ex)
                {
                    string str = ex.Message;
                    return dt;
                }
            }
        }

        /// <summary>
        /// 查询数据是否存在
        /// </summary>
        /// <param name="sqlstr">要执行的SQL语句</param>
        /// <param name="para">条件</param>
        /// <returns></returns>
        public int chaxun(string sqlstr, params MySqlParameter[] para)
        {
            using (MySqlCommand cmd = new MySqlCommand(sqlstr, con))
            {
                try
                {
                    if (para != null)
                    {
                        foreach (MySqlParameter p in para)
                        {
                            cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                        }
                    }

                    using (MySqlDataReader read = cmd.ExecuteReader())
                    {
                        if (read.Read())
                        {
                            return 1;
                        }
                        else
                            return 0;
                    }

                }
                catch (Exception ex)
                {
                    errorstr = ex.Message;
                    return -1;
                }
            }
        }

        
        public string chazhao(string sqlstr, params MySqlParameter[] para)
        {
            string str = "";
            using (MySqlCommand cmd = new MySqlCommand(sqlstr, con))
            {
                try
                {
                    if (para != null)
                    {
                        foreach (MySqlParameter p in para)
                        {
                            cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                        }
                    }
                    using (MySqlDataReader read = cmd.ExecuteReader())
                    {
                        if (read.Read())
                        {
                            str = read["QQ"].ToString();
                            return str;
                        }
                        else
                        {
                            str = "null";
                            return str;
                        }
                    }
                }
                catch (Exception ex)
                {
                    str = "!!!";
                    return str;
                }
            }
        }

        /// <summary>
        /// 根据要求读取某一行的全部数据
        /// </summary>
        /// <param name="sqlstr">要执行的SQL语句</param>
        /// <param name="data">读取到的数据集合</param>
        /// <param name="para">条件</param>
        /// <returns></returns>
        public int Readshuju(string sqlstr, out List<string> data, params MySqlParameter[] para)
        {
            data = new List<string>();
            using (MySqlCommand cmd = new MySqlCommand(sqlstr, con))
            {
                try
                {
                    DataSet dt = new DataSet();
                    if (para != null)
                    {
                        foreach (MySqlParameter p in para)
                        {
                            cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                        }
                    }
                    MySqlDataAdapter mda = new MySqlDataAdapter(cmd);

                    mda.Fill(dt);
                    if (dt.Tables[0].Rows.Count >= 1)
                    {
                        for (int i = 0; i < dt.Tables[0].Rows[0].ItemArray.Length; i++)
                        {
                            data.Add(dt.Tables[0].Rows[0][i].ToString());
                        }
                        return 1;
                    }
                    else
                        return 0;

                }
                catch (Exception ex)
                {
                    errorstr = ex.Message;
                    return -1;
                }
            }
        }

        /// <summary>
        /// 根据要求获取某一行的某些数据
        /// </summary>
        /// <param name="sqlstr">SQL语句</param>
        /// <param name="key">要获取数据的栏位名称</param>
        /// <param name="data">返回的数据集合</param>
        /// <param name="para">要查询的条件</param>
        /// <returns></returns>
        public bool Readshuju(string sqlstr, List<string> key, out List<string> data, params MySqlParameter[] para)
        {
            data = new List<string>();
            bool jg = false;
            using (MySqlCommand cmd = new MySqlCommand(sqlstr, con))
            {
                try
                {
                    if (para != null)
                    {
                        foreach (MySqlParameter p in para)
                        {
                            cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                        }
                    }
                    using (MySqlDataReader read = cmd.ExecuteReader())
                    {
                        if (read.Read())
                        {
                            jg = true;
                            for (int i = 0; i < key.Count; i++)
                            {
                                data.Add(read[key[i]].ToString());
                            }
                        }
                    }
                    return jg;
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    return false;
                }
            }
        }

        /// <summary>
        /// 根据某一条件获取某一数据
        /// </summary>
        /// <param name="sqlstr">SQL语句</param>
        /// <param name="key">要获取数据的栏位名称</param>
        /// <param name="data">返回的数据</param>
        /// <returns></returns>
        public bool Readshuju(string sqlstr, string key, out string data)
        {
            data = "";
            bool jg = false;
            using (MySqlCommand cmd = new MySqlCommand(sqlstr, con))
            {
                try
                {
                    using (MySqlDataReader read = cmd.ExecuteReader())
                    {
                        if (read.Read())
                        {
                            jg = true;
                            data = read[key].ToString();
                        }
                    }
                    return jg;
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    return false;
                }
            }
        }


        /// <summary>
        /// 根据要求读取某列的数据
        /// </summary>
        /// <param name="sqlstr">要执行的SQL语句</param>
        /// <param name="data">读取到的数据集合</param>
        /// <param name="para">条件</param>
        /// <returns></returns>
        public bool Readshuju1(string sqlstr, out List<string> data, params MySqlParameter[] para)
        {
            data = new List<string>();
            using (MySqlCommand cmd = new MySqlCommand(sqlstr, con))
            {
                try
                {
                    DataSet dt = new DataSet();
                    if (para != null)
                    {
                        foreach (MySqlParameter p in para)
                        {
                            cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                        }
                    }
                    MySqlDataAdapter mda = new MySqlDataAdapter(cmd);

                    mda.Fill(dt);
                    if (dt.Tables[0].Rows.Count >= 1)
                    {
                        for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                        {
                            data.Add(dt.Tables[0].Rows[i][0].ToString());
                        }
                        return true;
                    }
                    else
                        return false;

                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    return false;
                }
            }

        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="Tabname">要添加数据的表名</param>
        /// <param name="keys">要新增数据的栏位名称集合</param>
        /// <param name="values">要新增的数据集合</param>
        /// <returns></returns>
        public bool Addshuju(string Tabname, List<string> keys, List<string> values)
        {
            string sv1 = "", sv2 = "";
            foreach (var key in keys)
                sv1 += key + ",";
            sv1 = sv1.Remove(sv1.Length - 1);

            foreach (var value in values)
                sv2 += $"'{value}',";
            sv2 = sv2.Remove(sv2.Length - 1);

            string sqlstr = $"INSERT INTO {Tabname}({sv1})VALUES({sv2})";
            using (MySqlCommand cmd = new MySqlCommand(sqlstr, con))
            {
                try
                {
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    return false;
                }
            }

        }

        /// <summary>
        /// 新增多行数据
        /// </summary>
        /// <param name="Tabname">要添加数据的表名</param>
        /// <param name="key">要新增数据的栏位</param>
        /// <param name="values">要新增的数据集合</param>
        /// <returns></returns>
        public bool Addshujus(string Tabname, string key, List<string> values)
        {
            bool ok = false;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            try
            {
                string sqlstr = "";
                foreach (string value in values)
                {
                    sqlstr = $"INSERT INTO {Tabname}({key})VALUES('{value}')";
                    cmd.CommandText = sqlstr;
                    ok = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                ok = false;
            }
            finally
            {
                cmd.Dispose();
            }
            return ok;
        }

        /// <summary>
        /// 新增多行数据
        /// </summary>
        /// <param name="Tabname">要添加数据的表名</param>
        /// <param name="keys">要新增数据的栏位名称集合</param>
        /// <param name="values">要新增的数据集合</param>
        /// <returns></returns>
        public bool Addshujus(string Tabname, List<string> keys, List<List<string>> values)
        {
            bool ok = false;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            try
            {
                string sqlstr = "";
                string sv1 = "";
                foreach(var key in keys)
                {
                    sv1 += key + ",";
                }
                sv1 = sv1.Remove(sv1.Length - 1);

                foreach (List<string> value in values)
                {
                    string sv2 = "";
                    foreach(string data in value)
                    {
                        sv2 += $"'{data}',";
                    }
                    sv2 = sv2.Remove(sv2.Length - 1);
                    sqlstr = $"INSERT INTO {Tabname}({sv1})VALUES({sv2})";
                    cmd.CommandText = sqlstr;
                    ok = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                ok = false;
            }
            finally
            {
                cmd.Dispose();
            }
            return ok;
        }


        /// <summary>
        /// 修改数据，条件放在集合前面
        /// </summary>
        /// <param name="Tabname">要修改数据的表名</param>
        /// <param name="num">条件数量</param>
        /// <param name="key">要修改数据的栏位名称集合</param>
        /// <param name="value">要修改的数据集合</param>
        /// <returns></returns>
        public bool Xiugaishuju(string Tabname, int num, List<string> keys, List<string> values)
        {
            string sv1 = "", sv2 = "";
            for (int i = num-1; i < keys.Count - 2; i++)
            {
                sv1 += $"{keys[i]}=@{keys[i]},";
            }
            sv1 += $"{keys[keys.Count - 1]}=@{keys[keys.Count - 1]}";

            if (num == 1)
            {
                sv2 += $"{keys[0]}=@{keys[0]}";
            }
            else if (num == 2)
            {
                sv2 += $"{keys[0]}=@{keys[0]} AND {keys[1]}=@{keys[1]}";

            }

            string sqlstr = $"UPDATE {Tabname} SET {sv1} WHERE {sv2}";
            using (MySqlCommand cmd = new MySqlCommand(sqlstr, con))
            {
                try
                {
                    for (int i = num-1; i < values.Count; i++)
                    {
                        cmd.Parameters.AddWithValue(keys[i], values[i]);
                    }
                    return cmd.ExecuteNonQuery() > 0;

                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    return false;
                }
            }

        }

        public bool Xiugaishuju1(string sqlstr)
        {
            //string sv1= $"{key}=@{key}";
            //string sv1 = $"{key}=@{key}";
            //string sqlstr = $"UPDATE {Tabname} SET {sv1} WHERE {sv2}";
            using (MySqlCommand cmd = new MySqlCommand(sqlstr, con))
            {
                try
                {
                    //cmd.Parameters.AddWithValue(key, value);
                    return cmd.ExecuteNonQuery() > 0;

                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    return false;
                }
            }
        }


        /// <summary>
        /// 修改最后一行的数据（用此方法数据库要创建一个栏位为ID的列）
        /// </summary>
        /// <param name="Tabname">要修改数据的表名</param>
        /// <param name="key">要修改数据的栏位名称</param>
        /// <param name="value">要修改的数据</param>
        /// <returns></returns>
        public bool Xiugaishuju(string Tabname,string key,string value)
        {
            //要几行就修改后面的数字即可
            string sqlstr = $"UPDATE {Tabname} SET {key}='{value}' WHERE 1 ORDER BY ID DESC LIMIT 1";
            using (MySqlCommand cmd = new MySqlCommand(sqlstr, con))
            {
                try
                {
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    return false;
                }
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Tabname">要删除数据的表名</param>
        /// <param name="num">删除的条件数量</param>
        /// <param name="key">条件的栏位名称集合</param>
        /// <param name="value">条件的值的集合</param>
        /// <returns></returns>
        public bool Deleteshuju(string Tabname, int num, List<string> key, List<string> value)
        {
            string sv2 = "";
            if (num == 1)
            {
                sv2 += $"{key[0]}=@{key[0]}";
            }
            else if (num == 2)
            {
                sv2 += $"{key[0]}=@{key[0]} AND {key[1]}=@{key[1]}";

            }

            string sqlstr = $"DELETE FROM {Tabname} WHERE {sv2}";
            using (MySqlCommand cmd = new MySqlCommand(sqlstr, con))
            {
                try
                {
                    for (int i = 0; i < value.Count; i++)
                    {
                        cmd.Parameters.AddWithValue(key[i], value[i]);
                    }
                    return cmd.ExecuteNonQuery() > 0;

                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    return false;
                }
            }

        }

        public bool Deleteshuju(string sqlstr)
        {
            using (MySqlCommand cmd = new MySqlCommand(sqlstr, con))
            {
                try
                {
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    return false;
                }
            }
        }

        /// <summary>
        /// 创建数据表
        /// </summary>
        /// <param name="sqlstr">SQL语句</param>
        /// <returns></returns>
        public bool CreatedMysqlTable(string sqlstr)
        {
            using (MySqlCommand cmd = new MySqlCommand(sqlstr, con))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    Debug.WriteLine("创建数据库失败："+s);
                    return false;
                }
            }
        }

        /// <summary>
        /// 创建数据表
        /// </summary>
        /// <param name="tablename">数据表名称</param>
        /// <param name="columns">列名</param>
        /// <param name="datatupes">列数据类型</param>
        /// <returns></returns>
        public bool CreatedMysqlTable(string tablename,List<string>columns, List<string> datatupes)
        {
            string str = "";
            for(int i=0;i<columns.Count;i++)
            {
                if (i <= columns.Count - 2)
                    str += columns[i] + " " + datatupes[i] + ",";
                else
                    str += columns[i] + " " + datatupes[i];
            }
            string sqlstr = $"CREATE TABLE {tablename} ({str})";
            using (MySqlCommand cmd = new MySqlCommand(sqlstr, con))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                    Debug.WriteLine("创建数据库成功");
                    return true;
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    Debug.WriteLine("创建数据库失败：" + s);
                    return false;
                }
            }
        }

        //关于数据表的各种操作
        private void test2_3()
        {
            //复制表（不复制数据）
            //string strsql = "CREATE TABLE 表名 LIKE 被复制的表名";
            string strsql = "CREATE TABLE table3 LIKE table2";

            //删除数据表
            //strsql = "DROP TABLE 表名";
            strsql = "DROP TABLE table4";

            //修改表名
            //strsql = "ALTER TABLE 表名 RENAME TO 新的表名";
            strsql = "ALTER TABLE table3 RENAME TO table4";

            //添加一列
            //strsql = "ALTER TABLE 表名 ADD 列名 数据类型";
            strsql = "ALTER TABLE table1 ADD num char";

            //修改列名称
            strsql = "ALTER TABLE 表名 CHANGE 列名 新列名 新数据类型";
            strsql = "ALTER TABLE 表名 MODIFY 列名 新数据类型";

            //删除列
            //strsql = "ALTER TABLE 表名 DROP 列名";
            strsql = "ALTER TABLE table1 DROP num1";

        }

    }
}
