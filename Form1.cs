using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace process_list
{
    public partial class Form1 : Form
    {
        private Process[] T_process;
        public Form1()
        {
            InitializeComponent();
            dgv_process.Columns.Add("Prop", "Properties");
            dgv_process.Columns.Add("Val", "Values");
            dgv_process.Hide();
            get_process();
        }

        private void get_process(){
            List<Process> l_process = new List<Process>();
            Process[] p_list = Process.GetProcesses();
            foreach (var item in p_list)
            {
                list_process.Items.Add(item.ProcessName);
                l_process.Add(item);
            }
            T_process = l_process.ToArray();
            foreach (var prop in T_process[0].GetType().GetProperties())
            {
                dgv_process.Rows.Add(prop.Name, "");
            }
            dgv_process.Show();
        }

        private void select_process(object sender, EventArgs e)
        {
            Process cur_proc = T_process[list_process.SelectedIndex];
            int i = 0;
            foreach (var prop in cur_proc.GetType().GetProperties())
            {
                dgv_process.Rows[i].Cells[0].Value = prop.Name;
                try
                {
                    dgv_process.Rows[i].Cells[1].Value = prop.GetValue(cur_proc);
                }
                catch (Exception)
                {
                    dgv_process.Rows[i].Cells[1].Value = "Unavailable";
                }
                i++;
            }
        }

    }
}
