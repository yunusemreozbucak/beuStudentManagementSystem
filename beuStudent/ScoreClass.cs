using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beuStudent
{
    class ScoreClass
    {
        DBconnect connect = new DBconnect();
        
        public bool insetScore(int stdid, string courName, double scor, string desc) // not eklemek için fonksiyon oluşturuyoruz
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `notlar`(`StdId`, `KursAd`, `Not`, `Açıklama`) VALUES (@stid, @cn, @sco,@desc)", connect.GetConnection);
            // @stid, @cn, @sco, @desc
            command.Parameters.Add("@stid", MySqlDbType.Int32).Value = stdid;
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = courName;
            command.Parameters.Add("@sco", MySqlDbType.Double).Value = scor;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = desc;
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

        public DataTable getList(MySqlCommand command)  // veritabanından veri çağırma işlemini yapıyoruz.
        {
            command.Connection = connect.GetConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);   // veri kaynağında olan ve eşleşecek şekilde ekler ve yeniler.
            return table;
        }
        // zaten olan kursları kontrol eden fonksiyon oluşturuyoruz.
        public bool checkScore(int stdId, string cName)  // bir hata var, tekrar bakılacak.
        {
            DataTable table = getList(new MySqlCommand("SELECT * FROM `kurs` WHERE `KursId` = '"+stdId+"' AND `KursAd` = '"+cName +"'"));
            if(table.Rows.Count>0)
            {
                return false;
            }
            else
            {
                return false;
            }
        }
        // notlarda güncelleştirme yapabilmemiz için kullanıyoruz.
        public bool updateScore(int stdid, string scn, double scor, string desc) // not güncellemek için fonksiyon oluşturuyoruz
        {
            MySqlCommand command = new MySqlCommand("UPDATE `notlar` SET `Not` = @sco, `Açıklama`= @desc WHERE `StdId`= @stid AND `KursAd` = @scn ", connect.GetConnection);
            // @stid @sco, @desc, @scn
            command.Parameters.Add("@stid", MySqlDbType.Int32).Value = stdid;
            command.Parameters.Add("@sco", MySqlDbType.Double).Value = scor;
            command.Parameters.Add("@scn", MySqlDbType.VarChar).Value = scn;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = desc;
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
        // verileri kaldırmak için fonksiyonu ekliyoruz.
        public bool deleteScore(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `notlar` WHERE `StdId`= @id", connect.GetConnection);
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
    }
}
