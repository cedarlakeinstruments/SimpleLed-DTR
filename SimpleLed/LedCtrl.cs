using System;
using System.IO.Ports;


namespace SimpleLed
{
    /// <summary>
    /// The LED control class
    /// </summary>
    /// <remarks>
    /// The LED is attached to the serial port's DTR (Data Terminal Ready) line
    /// and is switched on and off by changing the state of this control line from
    /// active to inactive
    /// </remarks>
    public class LedCtrl
    {
        private SerialPort _port;

        /// <summary>
        /// Constructor
        /// </summary>
        public LedCtrl()
        {
            IsConnected = false;
        }

        /// <summary>
        /// Indicates if port is open or not
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// Opens COM port specified
        /// </summary>
        /// <param name="port">COMn</param>
        /// <returns>true if opened successfully</returns>
        public bool connect(string port)
        {
            try
            {
                _port = new SerialPort(port);
                _port.Open();
                IsConnected = true;
            }
            catch
            {
                // Any exception returns false
                IsConnected = false;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~LedCtrl()
        {
            if (IsConnected)
            {
                off();
                _port.Close();
            }
        }

        /// <summary>
        /// Turns LED on
        /// </summary>
        /// <returns>true if successful</returns>
        public bool on()
        {
            try
            {
                _port.DtrEnable = true;
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Turns LED off
        /// </summary>
        /// <returns>true if successful</returns>
        public bool off()
        {
            try
            {
                _port.DtrEnable = false;
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
