using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace beuStudent
{
    public partial class ScoreForm : Form
    {
        CourseClass course = new CourseClass();
        StudentClass student = new StudentClass();
        ScoreClass score = new ScoreClass();
        public ScoreForm()
        {
            InitializeComponent();
        }
        // verileri datagridview içerisindeki notlar için fonksiyon oluşturuyoruz.
        private void showScore()
        {
            DataGridView_student.DataSource = score.getList(new MySqlCommand("SELECT notlar.StdId,student.İsim,student.Soyisim,notlar.KursAd,notlar.Not,notlar.Açıklama FROM student INNER JOIN notlar ON notlar.StdId=student.StdId"));
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox_chour_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox_cName_TextChanged(object sender, EventArgs e)
        {

        }

        private void ScoreForm_Load(object sender, EventArgs e)
        {
            //kurs datagridview 
            comboBox_Course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `kurs`"));
            comboBox_Course.DisplayMember = "KursAd";  // var olan kurs adlarını getiriyoruz.
            comboBox_Course.ValueMember = "KursAd";

            // öğrenci datagridview 
            DataGridView_student.DataSource = student.getList(new MySqlCommand("SELECT`StdId`,`İsim`,`Soyisim` FROM `student`"));

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button_add_Click(object sender, EventArgs e)
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
                if (!score.checkScore(stdId, cName))     // tam çalışmıyor tekrar bakılacak.
                {

                    if (score.insetScore(stdId, cName, scor, desc))  //   if (course.insetCourse(cName, chr, desc))
                    {
                        showScore();// butona basıldığında ilgili fonksiyonun çalışması için ekliyoruz.
                        MessageBox.Show("Yeni Not Eklendi", "Not Ekle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Not Eklenemedi", "Not Ekle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Bu ders zaten var!", "Not Ekle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_stdId.Clear();
            textBox_score.Clear();
            textBox_cdescription.Clear();
            comboBox_Course.SelectedIndex = 0;

        }

        private void Datagridview_score(object sender, EventArgs e)
        {

        }

        private void DataGridView_student_Click(object sender, EventArgs e)
        {
            textBox_stdId.Text = DataGridView_student.CurrentRow.Cells[0].Value.ToString(); // tıklandığında datagridview içerisindeki ID'yi , textboxlarda gösterir.
        }

        private void button_estudent_Click(object sender, EventArgs e)
        {
            DataGridView_student.DataSource = student.getList(new MySqlCommand("SELECT`StdId`,`İsim`,`Soyisim` FROM `student`"));
        }

        private void button_escore_Click(object sender, EventArgs e)
        {
            showScore();
        }
    }
}
