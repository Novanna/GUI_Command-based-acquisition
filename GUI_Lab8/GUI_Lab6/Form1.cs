using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUI_Lab6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //baudrate
            comboBox2.Items.Add("9600");
            comboBox2.Items.Add("14400");
            comboBox2.Items.Add("19200");
            comboBox2.Items.Add("38400");
            comboBox2.Items.Add("56000");
            comboBox2.Items.Add("57600");
            comboBox2.Items.Add("76800");
            comboBox2.Items.Add("115200");

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String[] portList = System.IO.Ports.SerialPort.GetPortNames();
            foreach (String portName in portList)
                comboBox1.Items.Add(portName);
            comboBox1.Text = comboBox1.Items[comboBox1.Items.Count - 1].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "Connect";
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
                toolStripStatusLabel1.Text = serialPort1.PortName + " is closed.";
                button1.Text = "Connect";
                
            }
            else
            {
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = Int32.Parse(comboBox2.Text);
                serialPort1.NewLine = "\r\n";
                serialPort1.Open();
                toolStripStatusLabel1.Text = serialPort1.PortName + " is connected.";
                button1.Text = "Disconnect";
            }  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure to close the app?", "Close Application", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.No) e.Cancel = true;
        }


        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            String receivedMsg = serialPort1.ReadLine();
            Tampilkan(receivedMsg);
        }
        
        private delegate void TampilkanDelegate(object item);
        private void Tampilkan(object item)
        {
            if (InvokeRequired)
                listBox1.Invoke(new TampilkanDelegate(Tampilkan), item);
            else
            {
                //This is the UI thread so perform the task.
                listBox1.Items.Add(item);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                splitData(item);
            }
        }
        
        private void splitData(object item)
        {
            if (timer1.Enabled == true && timer2.Enabled == true)
            {
                String[] data = item.ToString().Split(',');
                if (checkBox1.Checked == true)
                {
                    string temp = data[1];
                    label13.Text = temp + "°C";

                }
                else if (checkBox2.Checked == true)
                {
                    string hum = data[1];
                    label14.Text = hum + "%RH";
                }
                else if (checkBox3.Checked == true)
                {
                    string pres = data[1];
                    label15.Text = pres + "mb";
                }
                else if (checkBox4.Checked == true)
                {
                    string uv = data[1];
                    label16.Text = uv;
                }
                else if (checkBox5.Checked == true)
                {
                    string temp = data[1];
                    label13.Text = temp + "°C";

                    string hum = data[2];
                    label14.Text = hum + "%RH";

                    string pres = data[3];
                    label15.Text = pres + "mb";

                    string uv = data[4];
                    label16.Text = uv;
                }
                
            }
            else
                toolStripStatusLabel1.Text = "Start your application!";
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                timer1.Enabled = !(timer1.Enabled);
                if (timer1.Enabled == true)
                {
                    button3.Text = "Stop";
                }
                else
                {
                    button3.Text = "Start";
                }
            }
            else
            {
                toolStripStatusLabel1.Text =  "Connect your serial port first!";
            }

        }

        private void chartControl1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            label13.ResetText();
            label14.ResetText();
            label15.ResetText();
            label16.ResetText();
            button4.Text = "Set";
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            checkBox4.Enabled = true;
            checkBox5.Enabled = true;
            toolStripStatusLabel1.Text = "Set your request data!";

        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                if (checkBox1.Checked == true || checkBox2.Checked == true || checkBox3.Checked == true || checkBox4.Checked == true || checkBox5.Checked == true)
                {
                    timer1.Enabled = !(timer2.Enabled);
                    timer2.Enabled = !(timer2.Enabled);
                    if (timer1.Enabled == true && timer2.Enabled == true)
                    {
                        button5.Text = "Stop Request";
                        toolStripStatusLabel1.Text = "Application was send request data";
                    }
                    else
                    {
                        button5.Text = "Send Request";
                    }
                }
                else
                    toolStripStatusLabel1.Text = "Check one of checkbox in request data!";
            }
            else
            {
                toolStripStatusLabel1.Text = "Start your application!";
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                if (checkBox5.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false)
                {
                    serialPort1.Write("T" + "\r\n");
                    toolStripStatusLabel1.Text = "Request Data : Suhu";
                }
                else
                {
                    toolStripStatusLabel1.Text = "You just can checked one checkbox!";
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                    checkBox4.Checked = false;
                    checkBox5.Checked = false;
                }
            }
            else if (checkBox2.Checked == true)
            {
                if (checkBox5.Checked == false && checkBox1.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false)
                {
                    serialPort1.Write("H" + "\r\n");
                    toolStripStatusLabel1.Text = "Request Data : Kelembaban";
                }
                else
                {
                    toolStripStatusLabel1.Text = "You just can checked one checkbox!";
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                    checkBox4.Checked = false;
                    checkBox5.Checked = false;
                }
            }
            else if (checkBox3.Checked == true)
            {
                if (checkBox5.Checked == false && checkBox1.Checked == false && checkBox2.Checked == false && checkBox4.Checked == false)
                {
                    serialPort1.Write("P" + "\r\n");
                    toolStripStatusLabel1.Text = "Request Data : Tekanan";
                }
                else
                {
                    toolStripStatusLabel1.Text = "You just can checked one checkbox!";
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                    checkBox4.Checked = false;
                    checkBox5.Checked = false;
                }
            }
            else if (checkBox4.Checked == true)
            {
                if (checkBox5.Checked == false && checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false)
                {
                    serialPort1.Write("U"+ "\r\n");
                    toolStripStatusLabel1.Text = "Request Data : Indeks UV";
                }
                else
                {
                    toolStripStatusLabel1.Text = "You just can checked one checkbox!";
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                    checkBox4.Checked = false;
                    checkBox5.Checked = false;
                }
            }
            else if (checkBox5.Checked == true)
            {
                if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false)
                {
                    serialPort1.Write("A" + "\r\n");
                    toolStripStatusLabel1.Text = "Request All Data";
                }
                else
                {
                    toolStripStatusLabel1.Text = "Just checked All Sensor to get all data, unchecked another!";
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                    checkBox4.Checked = false;
                    checkBox5.Checked = false;
                }
            }
        }

   }
}

