using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace SimpleLed
{
    /// <summary>
    /// This is the UI for the LED control
    /// </summary>
    public partial class SimpleLedMain : Form
    {
        private LedCtrl _led;
        private bool _ledState;

        public SimpleLedMain()
        {
            InitializeComponent();
            comboBoxPorts.SelectedIndex = 0;
            _led = new LedCtrl();
            _ledState = false;
        }

        /// <summary>
        /// Turn LED on
        /// </summary>
        private void buttonOn_Click(object sender, EventArgs e)
        {
            if (_led.IsConnected == true)
            {
                // Turn on LED
                _led.on();
                _ledState = true;
                
                // Are we flashing?
                if (checkBox1.Checked == true)
                {
                    // Get flash rate
                    int rateNum = 0;
                    int.TryParse(textBoxRate.Text, out rateNum);
                    int intervalMs = 100;
                    if (rateNum <= 10 && rateNum >= 0)
                    {
                        intervalMs = 1000 / rateNum;
                    }
                    // Set flash interval
                    timer1.Interval = intervalMs;
                    timer1.Enabled = true;
                }
            }
        }
        
        /// <summary>
        /// Turn LED off
        /// </summary>
        private void buttonOff_Click(object sender, EventArgs e)
        {
            if (_led.IsConnected == true)
            {
                _led.off();
                _ledState = false;
            }
            timer1.Enabled = false;
        }

        /// <summary>
        /// Open COM port
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string portName = (string)comboBoxPorts.SelectedItem;
            if (_led.connect(portName) == true)
            {
                buttonOff.Enabled = true;
                buttonOn.Enabled = true;
            }
            else
            {
                string errMsg = string.Format("Can't open port {0}", portName);
                MessageBox.Show(errMsg);
            }
        }

        /// <summary>
        /// Periodic timer for flashing LED
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_ledState == true)
            {
                _led.off();
                _ledState = false;
            }
            else
            {
                _led.on();
                _ledState = true;
            }
        }

    }
}
