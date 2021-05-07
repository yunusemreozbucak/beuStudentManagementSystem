using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace beuStudent
{
    public partial class ManageStudentForm : Form
    {
        StudentClass student = new StudentClass();
        public ManageStudentForm()
        {
            InitializeComponent();
        }
        private void ManageStudentForm_Load(object sender, EventArgs e)
        {
            showTable();
        }
        public void showTable()
        {
            DataGridView_student.DataSource = student.getStudentList(new MySqlCommand("SELECT * FROM `student`"));
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[7]; // ihtiyacım olan tüm verileri göstermesini sağladım.
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;

        }

        

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DataGridView_student_Click(object sender, EventArgs e)
        {
            textBox_id.Text = DataGridView_student.CurrentRow.Cells[0].Value.ToString();
            textBox_Fname.Text = DataGridView_student.CurrentRow.Cells[1].Value.ToString();
            textBox_Lname.Text = DataGridView_student.CurrentRow.Cells[2].Value.ToString();

            dateTimePicker1.Value = (DateTime)DataGridView_student.CurrentRow.Cells[3].Value;
            if (DataGridView_student.CurrentRow.Cells[4].Value.ToString() == "Erkek")
                radioButton_male.Checked = true;

            textBox_phone.Text = DataGridView_student.CurrentRow.Cells[5].Value.ToString();
            textBox_address.Text = DataGridView_student.CurrentRow.Cells[6].Value.ToString();
            byte[] img = (byte[])DataGridView_student.CurrentRow.Cells[7].Value;
            MemoryStream ms = new MemoryStream(img);
            pictureBox_student.Image = Image.FromStream(ms);
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_id.Clear();
            textBox_Fname.Clear();
            textBox_Lname.Clear();
            textBox_phone.Clear();
            textBox_address.Clear();
            radioButton_male.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
            pictureBox_student.Image = null;
        }
        bool verify() //true false döndürmesini istediğim için bool kullandım.
        {
            if ((textBox_Fname.Text == "") || (textBox_Lname.Text == "") ||
                (textBox_phone.Text == "") || (textBox_address.Text == "") ||
                (pictureBox_student.Image == null))
            {
                return false;
            }
            else
                return true;

        }

        private void button_update_Click(object sender, EventArgs e)
        {
            {
                //öğrenci güncelleme
                int id = Convert.ToInt32(textBox_id.Text);
                string isim = textBox_Fname.Text;
                string lname = textBox_Lname.Text;
                DateTime bdate = dateTimePicker1.Value;
                string phone = textBox_phone.Text;
                string address = textBox_address.Text;
                string gender = radioButton_male.Checked ? "Erkek" : "Kadın";      //ikisinden birisi seçilmek zorunda olduğu için bu koşulu ekledim.

                MemoryStream ms = new MemoryStream(); // memorystream bellekte tutulmasına yarıyor.
                pictureBox_student.Image.Save(ms, pictureBox_student.Image.RawFormat);
                byte[] img = ms.ToArray();

                int yas_yili = dateTimePicker1.Value.Year;
                int son_yil = DateTime.Now.Year;
                if ((son_yil - yas_yili) < 10 || (son_yil - yas_yili) > 100)
                {
                    MessageBox.Show("Öğrencinin yaşı 10 ile 100 arasında bir değer almalıdır. Lütfen geçerli değer girişi yapınız.", "Geçersiz Doğum Tarihi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (verify())
                {
                    try
                    {
                        if (student.updateStudent(id,isim, lname, bdate, gender, phone, address, img))
                        {
                            showTable();
                            MessageBox.Show("Öğrenci Güncellemesi", "Öğrenci güncellendi.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                    catch (Exception ex)

                    {
                        MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                else
                {
                    MessageBox.Show("Geçerli Yerleri Doldurunuz.", "Öğrenci Güncelle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            bool verify() //true false döndürmesini istediğim için bool kullandım.
            {
                if ((textBox_Fname.Text == "") || (textBox_Lname.Text == "") ||
                    (textBox_phone.Text == "") || (textBox_address.Text == "") ||
                    (pictureBox_student.Image == null))
                {
                    return false;
                }
                else
                    return true;

            }

        }

        private void DataGridView_student_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button_search_Click(object sender, EventArgs e)
        {
            DataGridView_student.DataSource = student.searchStudent(textBox_search.Text); // search student fonksiyonunu ekleyerek hem kod kalabalağından kurtulup hem de istediğim özelliği ekleyebildim.
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[7];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;

        }

        private void button_upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Fotoğraf seçiniz(*.jpg;*.png;*.gif;)|*.jpg; *png;*gif;";
            if (opf.ShowDialog() == DialogResult.OK)
                pictureBox_student.Image = Image.FromFile(opf.FileName);
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox_id.Text);
            if (student.deleteStudent(id))
            {
                showTable();
                MessageBox.Show("Öğrenci Kaldırıldı.", "Öğrenci Kaldırma", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button_clear.PerformClick();
            }
            else
            {
                MessageBox.Show("Öğrenci Kaldırılamadı.", "Öğrenci Kaldırma", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }

            
        }

        private void textBox_search_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
