using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zadanie
{
    public partial class Form3 : Form
    {
        public String fileContent;
        public String szyfr;
        public Form3()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;

                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
                this.textBox1.Text = fileContent.ToString();
            }

        }
        private void button4_Click(object sender, EventArgs e)
        {

        }
        static string zaszyfruj(string tekst, string klucz)
        {
            string wynik = "";
            for (int i = 0; i < tekst.Length; i++)
            {
                int znakTekst = tekst[i] - 'A';
                int znakKlucz = klucz[i % klucz.Length] - 'A';
                wynik += (char)((znakTekst ^ znakKlucz) + 'A');
            }
            return wynik;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           szyfr = zaszyfruj(fileContent, "haslo");
            textBox2.Text = szyfr;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            szyfr = zaszyfruj(fileContent, "haslo");
            textBox2.Text = szyfr;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Stream filestream = saveFileDialog1.OpenFile();
                StreamWriter sw = new StreamWriter(filestream);
                sw.WriteLine(this.textBox2.Text);
                sw.Close();
                filestream.Close();
            }
        }
    }
}
