using System;
using System.Collections.Generic;
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
                string error = "Item file not found, please select a new JSON file";
                toolStripStatusLabel1.Text = error;
                //MessageBox.Show(error);
                
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

            try
            {

                foreach (KeyValuePair<int, string> item in ItemHandler.SelectItem(textBox1.Text))
                {
                    listBox1.Items.Add(item.Value);
                    asd.Add(item.Key, item.Value);
                }
            }
            /*
            catch (NullReferenceException)
            {
                toolStripStatusLabel1.Text = "Error: Please open the item file"; 
                MessageBox.Show(this, "Please open the item file from the menu", "Error");
                
            }
            */
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error";
                MessageBox.Show(this, ex.ToString(), "Error");
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
                    toolStripStatusLabel1.Text = "File loaded: " + filePath;
                    ItemHandler.jsonUrl = filePath;
                }
                else
                {
                    toolStripStatusLabel1.Text = "File not loaded";
                }
            }

        }
    }
}
