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
using DGVPrinterHelper;


namespace beuStudent
{
    public partial class PrintStudent : Form
    {
        StudentClass student = new StudentClass();
        DGVPrinter printer = new DGVPrinter();   // DGVPrinter printer = new DGVPrinter(DGVPrinter); 

        public PrintStudent()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton_female_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton_male_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void PrintStudent_Load(object sender, EventArgs e)
        {
            showData(new MySqlCommand("SELECT * FROM `student`"));
        }
        private void showData(MySqlCommand command)
        {
            DataGridView_student.ReadOnly = true;
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            // DataGridView_student.Height = 80;
            DataGridView_student.DataSource = student.getList(command); // fonksiyonu tekrar yazmak yerine studentClass'ta tanımladığımız fonksiyonu ekliyoruz.
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[7];
            // eklenenen resmi düzgün bir şekilde gözükmesini sağlıyoruz.
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        private void button_search_Click(object sender, EventArgs e)   
        {
            string selectQuery;
            if(radioButton_all.Checked)
            {
                selectQuery = "SELECT * FROM `student`";  // tüm verileri getir
            }
            else if(radioButton_male.Checked)
            {
                selectQuery = "SELECT * FROM `student` WHERE `Cinsiyet`= 'Erkek'"; // erkek olanları getir.
            }
            else
            {
                selectQuery = "SELECT * FROM `student` WHERE `Cinsiyet`= 'Kadın'";  // kadın olanları getir
            }
            showData(new MySqlCommand(selectQuery));
        }

        private void button_print_Click(object sender, EventArgs e)
        {
            printer.Title = "Beykoz Üniversitesi Liste";
            printer.SubTitle = string.Format("Tarih: {0}", DateTime.Now.Date); // çıktı alınan tarih.
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "foxlearn";
            printer.FooterSpacing = 15;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(DataGridView_student);
        }
    }
}
