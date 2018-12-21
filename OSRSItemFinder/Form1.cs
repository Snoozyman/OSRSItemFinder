using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSRSItemFinder
{
    public partial class Form1 : Form
    {

        Dictionary<int, string> asd = new Dictionary<int, string>();
        
        
        
        public Form1()
        {
            InitializeComponent();
            if (!ItemHandler.CheckFile())
            {
                openFile();
            }
        }

        

        public void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            ClearForm();
            listBox1.Items.Clear();
            asd.Clear();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            asd.Clear();
            ClearForm();

            foreach (KeyValuePair<int, string> item in ItemHandler.SelectItem(textBox1.Text))
            {
                listBox1.Items.Add(item.Value);
                asd.Add(item.Key, item.Value);
            }
            
        }
        private void ClearForm()
        {
            
            checkBox1.Checked = false;
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            webBrowser1.DocumentText = "";
     

        }
        private void listBox1_SelectedItem (object sender, EventArgs e)
        {
           
            // textBox2.Text = listBox1.SelectedItem.ToString();
            foreach (KeyValuePair<int,string> item in asd)
            {
                if(listBox1.SelectedItem.ToString() == item.Value)
                {
                    textBox2.Text = item.Value;
                    textBox3.Text = item.Key.ToString();
                    textBox4.Text = ItemHandler.GetItemInfo(item.Key, 'p');
                    webBrowser1.DocumentText = "<img src=\"" + ItemHandler.GetItemInfo(item.Key, 'i') + "\" />";
                    if (ItemHandler.GetItemInfo(item.Key, 'm')){
                        checkBox1.Checked = true;
                    }
                    else { checkBox1.Checked = false; }
                }
            }
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void openJSONFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile();
        }
        public void openFile()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "";
                openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    toolStripStatusLabel1.Text = filePath;
                    ItemHandler.jsonUrl = filePath;
                }
            }

        }
    }
}
