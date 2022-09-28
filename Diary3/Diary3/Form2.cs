using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diary3
{
    public partial class Form2 : Form
    {
        bool notes = true;
        string dataFilename = "Data.txt";

        public Form2()
        {
            InitializeComponent();
        }

        private void update_notes_list()
        {
            listBox1.Items.Clear();

            using (StreamReader reader = new StreamReader(dataFilename))
            {
                //note:noteTheme:textNote;
                //note:noteTheme1:textNote1;
                //task:taskTheme:27.09.2001;
                //task:taskTheme1:01.03.4999;

                string? line = reader.ReadLine();

                while (line != null)
                {
                    string[] words = line.Split(':');
                    if (words[0] == "note")
                    {
                        listBox1.Items.Add(words[1]);
                    }
                    line = reader.ReadLine();
                }
            }
        }

        private void update_tasks_list()
        {
            listBox2.Items.Clear();

            using (StreamReader reader = new StreamReader(dataFilename))
            {
                //note:noteTheme:textNote;
                //note:noteTheme1:textNote1;
                //task:taskTheme:27.09.2001;
                //task:taskTheme1:01.03.4999;

                string? line = reader.ReadLine();

                while (line != null)
                {
                    string[] words = line.Split(':');
                    if (words[0] == "task")
                    {
                        listBox2.Items.Add(words[1]);
                    }
                    line = reader.ReadLine();
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            notes = true;
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel2.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (notes)
            {
                //добавление заметки
                string textNote = richTextBox2.Text;
                string themeNote = textBox4.Text;
                
                //удаление текста из темы заметки
                textBox4.Text = "";
                //удаление текста из содержания заметки
                richTextBox2.Text = "";

                using (StreamWriter writer = new StreamWriter(dataFilename, true))
                {
                    //note:noteTheme:textNote;
                    //note:noteTheme1:textNote1;
                    //task:taskTheme:27.09.2001;
                    //task:taskTheme1:01.03.4999;

                    writer.Write("note:" + themeNote + ":");
                    writer.WriteLine(textNote + ";");
                }
                update_notes_list();
            }
            else
            {
                //добавление задачи
                string themeTask = textBox3.Text;
                DateTime dateTask = dateTimePicker1.Value;

                //удаление текста из темы задачи
                textBox3.Text = "";

                using (StreamWriter writer = new StreamWriter(dataFilename, true))
                {
                    //note:noteTheme:textNote;
                    //note:noteTheme1:textNote1;
                    //task:taskTheme:27.09.2001;
                    //task:taskTheme1:01.03.4999;

                    writer.Write("task:" + themeTask + ":");
                    writer.WriteLine($"{dateTask.Day}.{dateTask.Month}.{dateTask.Year};");
                }
                update_tasks_list();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            notes = false;
            flowLayoutPanel1.Visible = true;
            flowLayoutPanel2.Visible = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "";

            using (StreamReader reader = new StreamReader(dataFilename))
            {
                //note:noteTheme:textNote;
                //note:noteTheme1:textNote1;
                //task:taskTheme:27.09.2001;
                //task:taskTheme1:01.03.4999;

                string? line = reader.ReadLine();

                while (line != null)
                {
                    string[] words = line.Split(':');
                    if (words[0] == "note" && words[1] == listBox1.SelectedItem.ToString())
                    {
                        richTextBox1.Text = words[2].Substring(0, words[2].Length - 1);
                        break;
                    }
                    line = reader.ReadLine();
                }
            }
        }
    }
}
