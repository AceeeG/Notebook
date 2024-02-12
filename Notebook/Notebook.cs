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

namespace Notebook
{
    public partial class Notebook : Form
    {
        private string name;
        private bool is_saved;
        private bool is_changed;
        public Notebook()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            name = "";
            is_saved = false;
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
                StreamWriter stream = new StreamWriter(new_name);
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
    }
}
