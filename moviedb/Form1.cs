using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Linq.JsonPath;
using System.IO;
namespace moviedb
{
    public partial class Form1 : Form
    {
        public JObject ja;
        public string jsontext;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string filename = "G:\\新建文件夹\\脚本\\movie.json";
            jsontext = File.ReadAllText(filename, Encoding.UTF8);


            ja = (JObject)JsonConvert.DeserializeObject(jsontext);

            button1.Hide();
        }
        public void Showdebug()
        {
            richTextBox1.AppendText(jsontext);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            string filename = ofd.FileName;
            richTextBox1.AppendText(filename);
            jsontext = File.ReadAllText(filename, Encoding.UTF8);
            ja = (JObject)JsonConvert.DeserializeObject(jsontext);
            
            
            //MessageBox.Show(richTextBox2.TextLength + "");
           
            //Showdebug();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            comboBox2.Items.Clear();
            foreach (var namelist in ja["movie"][index].Values())
            {
                foreach (var name in namelist)
                {
                    comboBox2.Items.Add(name["name"].ToString());                    
                }                
            }

            comboBox2.SelectedIndex = 0;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int leiindex = comboBox1.SelectedIndex;
            int mindex = comboBox2.SelectedIndex;
            string infoMovie = "";
            string grade = "";
            string link = "";
            foreach (var namelist in ja["movie"][leiindex].Values())
            {               
                infoMovie = namelist[mindex]["info"].ToString();
                grade = namelist[mindex]["grade"].ToString();
                link = namelist[mindex]["link"].ToString();
            }            
            richTextBox1.Clear();
            richTextBox1.AppendText(infoMovie);
            textBox1.Text = link;
            textBox2.Text = grade;
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            button1.Show();
        }
    }
}
