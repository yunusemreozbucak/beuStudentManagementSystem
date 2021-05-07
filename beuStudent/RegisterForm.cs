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
    public partial class RegisterForm : Form
    {
        StudentClass student = new StudentClass();
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            showTable();
        }
        

        // çağırdığımız öğrencilere datagridview içerisine aktarıyoruz.
        public void showTable()
        {
            DataGridView_student.DataSource = student.getStudentList(new MySqlCommand("SELECT * FROM `student`"));
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[7]; // ihtiyacım olan tüm verileri göstermesini sağladım.
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

       

        

        private void button_upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif)|*.jpg;*.png;*.git";  // fotoğraf yüklemek için gerekli uzantıları tanımlıyoruz.
            if (opf.ShowDialog() == DialogResult.OK)
                pictureBox_student.Image = Image.FromFile(opf.FileName);      // resmi yönlendireceğimiz yerin yolunu gösteriyoruz.

        }

        private void pictureBox_student_Click(object sender, EventArgs e)
        {

        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            
            textBox_Fname.Clear();
            textBox_Lname.Clear();
            textBox_phone.Clear();
            textBox_address.Clear();
            radioButton_male.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
            pictureBox_student.Image = null;

        

        }

        private void button_add_Click(object sender, EventArgs e)
        {
            {
                //öğrenci ekliyoruz
                string isim = textBox_Fname.Text;
                string lname = textBox_Lname.Text;
                DateTime bdate = dateTimePicker1.Value;
                string phone = textBox_phone.Text;
                string address = textBox_address.Text;
                string gender = radioButton_male.Checked ? "Erkek" : "Kadın";  

                MemoryStream ms = new MemoryStream(); // memorystream bellekte tutulmasına yarıyor.
                pictureBox_student.Image.Save(ms, pictureBox_student.Image.RawFormat);
                byte[] img = ms.ToArray();

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
                        if (student.insertStudent(isim, lname, bdate, gender, phone, address, img))
                        {
                            showTable();
                            MessageBox.Show("Yeni Öğrenci Eklendi.", "Öğrenci Ekle", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                    catch (Exception ex)

                    {
                        MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                else
                {
                    MessageBox.Show("Geçerli Yerleri Doldurunuz.", "Öğrenci Ekle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
