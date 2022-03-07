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
    public partial class Form2 : Form
    {
        public String fileContent;
        public String szyfr;
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            szyfr = encrypt(fileContent, 5);
            this.textBox2.Text = szyfr;
        }
        private static String encrypt(String txt, int key)
        {
            String encrypted = "";

            for (int i = 0; i < txt.Length; i++)
            {
                if (Char.IsUpper(txt[i]))
                {
                    int characterIndex = txt[i] - (char)('A');
                    int characterShifted = (characterIndex + key) % 26 + (char)('A');
                    encrypted += (char)(characterShifted);
                }
                else if (Char.IsLower(txt[i]))
                {
                    int characterIndex = txt[i] - (char)('a');
                    int characterShifted = (characterIndex + key) % 26 + (char)('a');
                    encrypted += (char)(characterShifted);
                }
                else
                {
                    encrypted += txt[i];
                }
            }
            return encrypted;
        }
        private static String decrypt(String txt, int key)
        {
            String decrypted = "";
            key = key % 26;

            for (int i = 0; i < txt.Length; i++)
            {
                if (Char.IsUpper(txt[i]))
                {
                    int characterIndex = txt[i] - (char)('A');
                    int characterOrgPos = (characterIndex - key) % 26 + (char)('A');
                    decrypted += (char)(characterOrgPos);
                }
                else if (Char.IsLower(txt[i]))
                {
                    int characterIndex = txt[i] - (char)('a');
                    int characterOrgPos = (characterIndex - key) % 26 + (char)('a');
                    decrypted += (char)(characterOrgPos);
                }
                else
                {
                    decrypted += txt[i];
                }
            }
            return decrypted;
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            szyfr = decrypt(fileContent, 5);
            this.textBox2.Text = szyfr;
        }

        private void button4_Click(object sender, EventArgs e)
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
