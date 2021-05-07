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
using DGVPrinterHelper;


namespace beuStudent
{
    public partial class PrintCourseForm : Form
    {
        CourseClass course = new CourseClass();
        DGVPrinter printer = new DGVPrinter();
        public PrintCourseForm()
        {
            InitializeComponent();
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            DataGridView_student.DataSource = course.getCourse(new MySqlCommand("SELECT* FROM `kurs` WHERE CONCAT(`KursAd`)LIKE '%" + textBox_search.Text + "%'"));
            textBox_search.Clear(); // garanti olsun diye artan verileri kaldırıyoruz.
            //concat, float verilerini tutmak için kullanılır.
        }

        private void PrintCourseForm_Load(object sender, EventArgs e)
        {
            DataGridView_student.DataSource = course.getCourse(new MySqlCommand("SELECT* FROM `kurs`"));
        }

        private void button_print_Click(object sender, EventArgs e)
        {
            printer.Title = "Beykoz Üniversitesi Kurs Liste";
            printer.SubTitle = string.Format("Tarih: {0}", DateTime.Now.Date); // çıktı alınan tarih.
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "Beykoz ";
            printer.FooterSpacing = 15;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(DataGridView_student);
        }
    }
}
