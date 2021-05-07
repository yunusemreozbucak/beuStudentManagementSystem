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
    public partial class PrintScoreForm : Form
    {
        ScoreClass score = new ScoreClass();
        DGVPrinter printer = new DGVPrinter();
        public PrintScoreForm()
        {
            InitializeComponent();
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            DataGridView_score.DataSource = score.getList(new MySqlCommand("SELECT notlar.StdId,student.İsim,student.Soyisim,notlar.KursAd,notlar.Not,notlar.Açıklama FROM student INNER JOIN notlar ON notlar.StdId=student.StdId"));
        }

        private void PrintScoreForm_Load(object sender, EventArgs e)
        {
            showScore();


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
            printer.PrintDataGridView(DataGridView_score);
        }
        public void showScore()
        {
            DataGridView_score.DataSource = score.getList(new MySqlCommand("SELECT notlar.StdId,student.İsim,student.Soyisim,notlar.KursAd,notlar.Not,notlar.Açıklama FROM student INNER JOIN notlar ON notlar.StdId=student.StdId"));

        }
    }
}
