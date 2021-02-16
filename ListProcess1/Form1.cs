using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListProcess1
{
    public partial class Form1 : Form
    {
        Timer timer;
        Process[] processRefresh;
        public Form1()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
         processRefresh = Process.GetProcesses();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
           
            processRefresh = Process.GetProcesses();
            listView1.Refresh();//обновляем лист
        }

        private void button1_Click(object sender, EventArgs e)
        {
          

            Process[] processes = Process.GetProcesses();//получаем список процессов 
           
            ListViewItem lstViewItems = null;
        
            foreach (Process process in processes)
            {
              
                lstViewItems = new ListViewItem();
                lstViewItems.Text = process.ProcessName;//добавляем название процесса
                //получаем память занятую процессом
                lstViewItems.SubItems.Add(Process.GetProcessesByName(process.ProcessName)[0].VirtualMemorySize64.ToString());

              listView1.Items.Add(lstViewItems);
            }
             

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer.Start();
           
            ListViewItem lstViewItems = null;
            
            foreach (Process process in processRefresh)
            {
                lstViewItems = new ListViewItem();
                lstViewItems.Text = process.ProcessName;

                lstViewItems.SubItems.Add(Process.GetProcessesByName(process.ProcessName)[0].VirtualMemorySize64.ToString());


                listView1.Items.Add(lstViewItems);
            }


        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
         
            for (int i = 0; i < listView1.SelectedItems.Count; i++)
            {
                if (listView1.SelectedItems[i].Selected)
                {
                    try
                    {
                        string nameProcess = listView1.SelectedItems[i].Text;
                  Process process = Process.GetProcessesByName(nameProcess)[0];
                    this.listBox1.Items.Add($"Name Process : {process.ProcessName}");
                    this.listBox1.Items.Add($"Identity Process : {process.Id}");                   
                        this.listBox1.Items.Add($"Time Start Process : {process.StartTime}");
                        this.listBox1.Items.Add($"Counter of threads : {process.Threads.Count}");
                        this.listBox1.Items.Add($"Processor Time : {process.TotalProcessorTime}");
                        this.listBox1.Items.Add($"Counter process copy : {Process.GetProcessesByName(process.ProcessName).Count()}");
                    }
                    catch (Exception ex)
                    {

                        
                    }
                   
                 
                }
            }
           

        

        }
    }


    
}
