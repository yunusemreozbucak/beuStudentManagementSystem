using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace beuStudent
{
    public partial class ManageScoreForm : Form
    {
        CourseClass course = new CourseClass();
        ScoreClass score = new ScoreClass();
        public ManageScoreForm()
        {
            InitializeComponent();
        }
        
        
        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_stdId.Clear();
            textBox_score.Clear();
            textBox_cdescription.Clear();
            textBox_search.Clear();
        }

        private void ManageScoreForm_Load(object sender, EventArgs e)
        {
            //kurs datagridview 
            comboBox_Course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `kurs`"));
            comboBox_Course.DisplayMember = "KursAd";  // var olan kurs adlarını getiriyoruz.
            comboBox_Course.ValueMember = "KursAd";
            showScore();
        }
        public void showScore()
        {
            DataGridView_score.DataSource = score.getList(new MySqlCommand("SELECT notlar.StdId,student.İsim,student.Soyisim,notlar.KursAd,notlar.Not,notlar.Açıklama FROM student INNER JOIN notlar ON notlar.StdId=student.StdId"));

        }

        private void button_update_Click(object sender, EventArgs e)     //güncellemede sadece else fonksiyonu dönüyor.
        {
            if (textBox_stdId.Text == "" || textBox_score.Text == "")
            {
                MessageBox.Show("Not Ekle!", "Dosya Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int stdId = Convert.ToInt32(textBox_stdId.Text);
                string cName = comboBox_Course.Text;
                double scor = Convert.ToInt32(textBox_score.Text);
                string desc = textBox_cdescription.Text;
                {

                    if (score.updateScore(stdId,cName, scor, desc))  //   if (course.insetCourse(cName, chr, desc))
                    {
                        showScore();// butona basıldığında ilgili fonksiyonun çalışması için ekliyoruz.
                        MessageBox.Show(" Not Güncellendi", "Not Güncellemesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Not Güncellemesi Başarısız!", "Not Güncellemesi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if(textBox_stdId.Text == "")
            {
                MessageBox.Show("HATA- Öğrenci ID eksik!", "Not Kaldırma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int id = Convert.ToInt32(textBox_stdId.Text);
                
                if (score.deleteScore(id))
                {
                    showScore();
                    MessageBox.Show("Not Kaldırıldı.", "Not Kaldırma", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button_clear.PerformClick();
                }
                else
                {
                    MessageBox.Show("Not Kaldırılamadı.", "Not Kaldırma", MessageBoxButtons.OK, MessageBoxIcon.Error);      
                }
            }
        }

        private void panel2_Click(object sender, EventArgs e)
        {

        }

        private void DataGridView_course_Click(object sender, EventArgs e)
        {

        }

        private void DataGridView_score_Click(object sender, EventArgs e)
        {
            textBox_stdId.Text = DataGridView_score.CurrentRow.Cells[0].Value.ToString();
            comboBox_Course.Text = DataGridView_score.CurrentRow.Cells[3].Value.ToString();
            textBox_score.Text = DataGridView_score.CurrentRow.Cells[4].Value.ToString();
            textBox_cdescription.Text = DataGridView_score.CurrentRow.Cells[5].Value.ToString();


        }

        private void button_search_Click(object sender, EventArgs e)
        {
            DataGridView_score.DataSource = score.getList(new MySqlCommand("SELECT notlar.StdId, student.İsim, student.Soyisim, notlar.KursAd, notlar.Not, notlar.Açıklama FROM student INNER JOIN notlar ON notlar.StdId = student.StdId WHERE CONCAT(student.İsim, student.Soyisim, notlar.KursAd)LIKE '%"+textBox_search.Text+"%'"));
       
        }
    }
}
