using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace LocalLister
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        internal static List< string> GetAllIPv4Addresses()
        {
            List<string> l = new List<string>();
            foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
            {

                foreach (var ua in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ua.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        l.Add( ua.Address.ToString());
                    }
                }
            }
            return l;
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text += "      ("+Environment.UserName+")";
            comboBox2.SelectedIndex = 0;
            this.MinimumSize = this.MaximumSize = this.Size;

           var  _Prefixs_ = GetAllIPv4Addresses();
            foreach (var ip in _Prefixs_)
            {
                string x =(edit(ip));
                if(x.StartsWith("127.0"))
                    continue;
                threeparts.Add(x);
                comboBox1.Items.Add(x+"0");
            }
            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;



        }

        List<string> threeparts = new List<string>();
        private string  edit(string ip)
        {
            try
            {
                if (ip.Contains(".") == false)
                    return ip;
                // 1.5.2.3
                string[] pc = ip.Split('.');
                // 1 5 2 3 

                string u = "";
                for (int i = 0; i < pc.Length - 1; i++)
                {
                    u += pc[i] + ".";
                    
                }

                return u ;

            }
            catch { }
            return ip;



        }
        public static bool IsAlive(string ip)
        {
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingReply rep = p.Send(ip);
            return  (rep.Status == System.Net.NetworkInformation.IPStatus.Success) ;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            var x = new Form2();
            x.Show();
            Scan();
            this.Visible = true;
            try
            {
                x.Close();
            }catch{ }
            label3.Visible = comboBox2.Visible = true;


        }

        private void Scan()
        {
            this.Visible = false;
            richTextBox1.Text = this.currentresult = onlines = deads = "";
            string ThreeParts = this.threeparts[comboBox1.SelectedIndex];

            for (int i = ((int)numericUpDown1.Value); i <= ((int)numericUpDown5.Value); i++)
            {
                string x = ThreeParts + i;
                string state = this.offstate;

                string n = "";
                bool alive = IsAlive(x);
                if (alive)
                    state = this.onstate; 
                n = x + state + " \r\n";

                currentresult += n;
                if (alive)
                    onlines += n;
                else
                    deads += n;
                System.Threading.Thread.Sleep(10);
            }
            viewData();
        }

        private void viewData()
        {

            string d = "";
            switch (comboBox2.SelectedIndex)
            {
                case 0: d = currentresult;break ;
                case 1: d = onlines.Replace(this.onstate, ""); break;
                case 2: d = deads.Replace(this.offstate, ""); ; break;

            }
            richTextBox1.Text = d;
        } 
        private int ToInte(decimal p)
        {
            return (int)p;
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
        public string currentresult = ""; 
        public string deads ="";  
        public string onlines ="";

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewData();
        }

        public string offstate = " is OFFline-";

        public string onstate =" is ______Online______";
    }
}
