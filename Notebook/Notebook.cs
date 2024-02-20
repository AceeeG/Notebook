using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace Notebook
{
    public partial class Notebook : Form
    {
        private string name;
        private bool is_saved;
        private bool is_changed;
        private bool is_color_changed;
        public Notebook()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            name = "";
            is_saved = false;
            is_color_changed = false;
            MakeTitle();
        }
        public void CreateNew(object sender, EventArgs e)
        {
            SaveUnsaved();
            name = "";
            TextBox.Text = "";
            MakeTitle();
            is_changed = false;
        }
        public void Open(object sender, EventArgs e)
        {
            SaveUnsaved();
            if (OpenDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader stream = new StreamReader(OpenDialog.FileName);
                    TextBox.Text = stream.ReadToEnd();
                    stream.Close();
                    name = OpenDialog.FileName;
                    is_changed = false;
                }
                catch 
                {
                    MessageBox.Show("Не удаётся запустить файл.");
                }
            }
        }
        public void Save(string new_name)
        {
            if(new_name == "")
            {
                if(SaveDialog.ShowDialog() == DialogResult.OK)
                {
                    new_name = SaveDialog.FileName;
                }
            }
            try
            {
                StreamWriter stream = new StreamWriter(new_name + ".txt");
                stream.Write(TextBox.Text);
                name = new_name;
                is_saved = true;
                is_changed = false;
    }
            catch
            {
                MessageBox.Show("Не удаётся сохранить файл.");
            }
            MakeTitle();
        }
        public void QuickSave(object sender, EventArgs e)
        {
            Save(name);
        }
        public void SaveAs(object sender, EventArgs e)
        {
            Save("");
        }

        private void OpenDialog_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void Strip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void SaveDialog_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void TextChange(object sender, EventArgs e)
        {
            is_changed = true;
        }
        private void MakeTitle()
        {   
            if(name != "")
            {
                Text = "N|Note" + " - " + name;
            }
            else
            {
                Text = "N|Note";
            }
        }

        public void SaveUnsaved()
        {
            if (is_changed)
            {
                DialogResult result = MessageBox.Show("Сохранить?", "Есть несохраннёные изменения", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if(result == DialogResult.Yes)
                {
                    Save(name);
                }
                else if(result == DialogResult.No)
                {

                }
            }
        }

        public void Font(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            if (font.ShowDialog() == DialogResult.OK)
            {
                TextBox.Font = font.Font;
            }
        }
        public void Close(object sender, EventArgs e)
        {
            SaveUnsaved();
            DialogResult result = MessageBox.Show("Хотите выйти?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if(result == DialogResult.Yes)
            {
                this.Close();
            }
        }
        public void Print(object sender, EventArgs e)
        {
            PrintDocument print = new PrintDocument();
            print.PrintPage += PrintPageEx;
            PrintDialog dialog = new PrintDialog();
            dialog.Document = print;
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                dialog.Document.Print();
            }
        }

        public void PrintPageEx(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(TextBox.Text, TextBox.Font, Brushes.Black, 0 , 0);
        }
        public void Copy(object sender, EventArgs e)
        {
            TextBox.Copy();
        }
        public void Paste(object sender, EventArgs e)
        {
            TextBox.Paste();
        }
        public void Cut(object sender, EventArgs e)
        {
            TextBox.Cut();
        }
        public void BlackTheme(object sender, EventArgs e)
        {
            if (!is_color_changed) 
            {
                TextBox.BackColor = Color.Black; 
                Strip.BackColor = Color.Black;
                Strip.ForeColor = Color.White;
                View.ForeColor = Color.White;
                TextBox.ForeColor = Color.White;
            }
            else
            {
                TextBox.BackColor = Color.Black;
                Strip.BackColor = Color.Black;
                Strip.ForeColor = Color.WhiteSmoke;
                View.ForeColor = Color.White;
            }
        }
        public void WhiteTheme(object sender, EventArgs e)
        {
            if (!is_color_changed)
            {
                TextBox.BackColor = Color.LightGray;
                Strip.BackColor = Color.LightGray;
                Strip.ForeColor = Color.Black;
                View.ForeColor = Color.Black;
                TextBox.ForeColor = Color.Black;
            }
            else
            {
                TextBox.BackColor = Color.White;
                Strip.BackColor = Color.LightGray;
                Strip.ForeColor = Color.Black;
                View.ForeColor = Color.Black;
            }

        }

        public void ChangeColor(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.ShowDialog();
            TextBox.SelectionColor = dialog.Color;
            is_color_changed = true;
        }

        public void Spravka(object sender, EventArgs e)
        {
            MessageBox.Show("Текстовый редактор\nВсе права защищены");
        }
    }
}
