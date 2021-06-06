using System;
using System.IO;
using System.Windows.Forms;

namespace MitCodeBase64Converter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var ofd = new OpenFileDialog())
                {
                    ofd.Filter = @"Images (*.jpg;*.gif;*.png;*.jfif)|*.jpg;*.gif;*.png;*.jfif";
                    ofd.FilterIndex = 1;

                    if (ofd.ShowDialog() != DialogResult.OK) return;

                    textBox1.Text = ofd.FileName;
                }

                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    textBox2.Text = Convert.ToBase64String(File.ReadAllBytes(textBox1.Text));
                    Clipboard.SetText(textBox2.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
