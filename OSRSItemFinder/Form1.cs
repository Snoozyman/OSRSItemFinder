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
        }

        ItemHandler Kuk = new ItemHandler();

        public void button1_Click(object sender, EventArgs e)
        {
            
            listBox1.Items.Clear();
            asd.Clear();
            textBox1.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            asd.Clear();
            
            foreach (KeyValuePair<int, string> item in Kuk.SelectItem(textBox1.Text))
            {
                listBox1.Items.Add(item.Value);
                asd.Add(item.Key, item.Value);
            }
            
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
                    textBox4.Text = Kuk.GetItemInfo(item.Key, 'p');
                    webBrowser1.DocumentText = "<img src=\"" + Kuk.GetItemInfo(item.Key, 'i') + "\" />";
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        
    }
}
