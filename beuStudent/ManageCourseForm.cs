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
    public partial class ManageCourseForm : Form
    {
        CourseClass course = new CourseClass();
        public ManageCourseForm()
        {
            InitializeComponent();
        }

        private void ManageCourseForm_Load(object sender, EventArgs e)
        {
            showData();

        }
        // verileri kurs içerisine göster.
        private void showData()
        {
            DataGridView_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `kurs`"));
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_id.Clear();
            textBox_cName.Clear();
            textBox_chour.Clear();
            textBox_cdescription.Clear();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (textBox_cName.Text == "" || textBox_chour.Text == "" || textBox_id.Text.Equals(""))
            {
                MessageBox.Show("Veritabanı Hatası", "Dosya Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int id = Convert.ToInt32(textBox_id.Text);
                string cName = textBox_cName.Text;
                int chr = Convert.ToInt32(textBox_chour.Text);
                string desc = textBox_cdescription.Text;
                if (course.updateCourse(id,cName, chr, desc))  //   if (course.insetCourse(cName, chr, desc))
                {
                    showData();
                    MessageBox.Show("Kurs başarıyla güncellendi.", "Kurs Güncelle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Kurs Eklenemedi", "Kurs Güncelle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_id.Text== "")
            {
                MessageBox.Show("HATA- Öğrenci ID eksik!", "Not Kaldırma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int id = Convert.ToInt32(textBox_id.Text);

                if (course.deleteCourse(id))
                {
                    MessageBox.Show("Not Kaldırıldı.", "Not Kaldırma", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button_clear.PerformClick();
                }
                else
                {
                    MessageBox.Show("Not Kaldırılamadı.", "Not Kaldırma", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DataGridView_course_Click(object sender, EventArgs e)          // datagridview tıklandığında içerideki verileri textboxlara yazdırır.
        {
            textBox_id.Text = DataGridView_course.CurrentRow.Cells[0].Value.ToString();
            textBox_cName.Text = DataGridView_course.CurrentRow.Cells[1].Value.ToString();
            textBox_chour.Text = DataGridView_course.CurrentRow.Cells[2].Value.ToString();
            textBox_cdescription.Text = DataGridView_course.CurrentRow.Cells[3].Value.ToString();

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_search_Click(object sender, EventArgs e)
        {
            DataGridView_course.DataSource = course.getCourse(new MySqlCommand("SELECT* FROM `kurs` WHERE CONCAT(`KursAd`)LIKE '%" + textBox_search.Text + "%'"));
            textBox_search.Clear(); // garanti olsun diye artan verileri kaldırıyoruz.
            //concat, float verilerini tutmak için kullanılır.
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
