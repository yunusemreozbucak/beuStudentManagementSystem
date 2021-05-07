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
    public partial class CourseForm : Form
    {
        CourseClass course = new CourseClass();
        public CourseForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button_add_Click(object sender, EventArgs e)
        {
            if (textBox_cName.Text == "" || textBox_chour.Text == "")
            {
                MessageBox.Show("Veritabanı Hatası", "Dosya Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string cName = textBox_cName.Text;
                int chr = Convert.ToInt32(textBox_chour.Text);
                string desc = textBox_cdescription.Text;
                if(course.insetcourse(cName,chr,desc))  //   if (course.insetCourse(cName, chr, desc))
                {
                    showData();
                    MessageBox.Show("Yeni Ders Eklendi", "Ders Ekle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ders Eklenemedi", "Ders Ekle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_cName.Clear();
            textBox_chour.Clear();
            textBox_cdescription.Clear();
        }

        private void CourseForm_Load(object sender, EventArgs e)
        {
            showData();
        }
        private void showData()
        {
            DataGridView_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `kurs`"));
        }

        private void DataGridView_course_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
 