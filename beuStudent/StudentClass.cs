using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace beuStudent
{
    class StudentClass
    {
        DBconnect connect = new DBconnect();
        public bool insertStudent(string isim, string soyisim, DateTime dtarihi, string cinsiyet, string telefon ,string adres, byte[] img)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `student`(`İsim`, `Soyisim`, `Doğum Tarihi`, `Cinsiyet`, `Telefon`, `Adres`, `Resim`) VALUES(@fn, @ln, @bd, @gd, @ph, @adr, @img)", connect.GetConnection);
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = isim;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = soyisim;
            command.Parameters.Add("@bd", MySqlDbType.Date).Value = dtarihi;
            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = cinsiyet;
            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = telefon;
            command.Parameters.Add("@adr", MySqlDbType.VarChar).Value = adres;
            command.Parameters.Add("@img", MySqlDbType.Blob).Value = img;

            connect.openConnect();
            if(command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }
        public DataTable getStudentList(MySqlCommand command)     // veritabanından öğrencileri çağırıyoruz.
        {
            command.Connection =connect.GetConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);   // veri kaynağında olan ve eşleşecek şekilde ekler ve yeniler.
            return table;
        }

        public string exeCount(string query)  // toplam erkek ve kadın sayısını çağırıyoruz.
        {
            MySqlCommand command = new MySqlCommand(query, connect.GetConnection);
            connect.openConnect();
            string count = command.ExecuteScalar().ToString();
            connect.closeConnect();
            return count;
        }
        public string totalStudent()  // toplam erkek kadın sayısını ekliyoruz.
        {
            return exeCount("SELECT COUNT(*) FROM student");  // sayım işlemini execount ile sayıyoruz.
        }
        public string maleStudent()  // toplam erkek sayısını ekliyoruz.
        {
            return exeCount("SELECT COUNT(*) FROM student WHERE `Cinsiyet`='Erkek'");
        }
        public string femaleStudent()  // toplam erkek sayısını ekliyoruz.
        {
            return exeCount("SELECT COUNT(*) FROM student WHERE `Cinsiyet`='Kadın'");
        }

        // öğrenci ismi, soyadı ya da adresi arayıp, sıralamasını sağlıyoruz.
        public DataTable searchStudent(string searchdata)  
        {
            // concat string verileri toplamak ve işlemek için gerekli fonksiyon.
            MySqlCommand command = new MySqlCommand("SELECT * FROM `student` WHERE CONCAT(`İsim`,`Soyisim`,`Adres`) LIKE '%"+searchdata+"%'", connect.GetConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);   // veri kaynağında olan ve eşleşecek şekilde ekler ve yeniler.
            return table;
        }
        public bool updateStudent(int id, string isim, string soyisim, DateTime dtarihi, string cinsiyet, string telefon, string adres, byte[] img)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `student`(`İsim`, `Soyisim`, `Doğum Tarihi`, `Cinsiyet`, `Telefon`, `Adres`, `Resim`) VALUES(@fn, @ln, @bd, @gd, @ph, @adr, @img)", connect.GetConnection);
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = isim;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = soyisim;
            command.Parameters.Add("@bd", MySqlDbType.Date).Value = dtarihi;
            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = cinsiyet;
            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = telefon;
            command.Parameters.Add("@adr", MySqlDbType.VarChar).Value = adres;
            command.Parameters.Add("@img", MySqlDbType.Blob).Value = img;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }
        public bool deleteStudent(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `student` WHERE `StdId`= @id", connect.GetConnection);
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return true;
            }
        }


        //kurs için.
        public DataTable getList(MySqlCommand command)  // veritabanından veri çağırma işlemini yapıyoruz.
        {
            command.Connection = connect.GetConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);   // veri kaynağında olan ve eşleşecek şekilde ekler ve yeniler.
            return table;
        }
    }
}
