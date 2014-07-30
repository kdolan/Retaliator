using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.DirectInput;
using System.Threading;

namespace Retaliator_Client
{
    public partial class frm_Primary : Form
    {
        private int X_MAX_VAL = 65535; //Full left is 0
        private int Y_MAX_VAL = 65535; //Full forward is 0


        private bool X_INVERT = false;
        private bool Y_INVERT = true;
        private bool THROTTLE_INVERT = true;

        private double Y_Axis = 0;
        private double X_Axis = 0;
        private double throttle = 0;
        private bool fire = false;

        private double DEAD_ZONE = 0.01;

        private Joystick joystick;

        public frm_Primary()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread joystickManagerThread = new Thread(new ThreadStart(joystickManagerThreadStart));
            joystickManagerThread.Start();
            updateTImer.Start();           
        }

        private void joystickManagerThreadStart()
        {
            // Initialize DirectInput
            var directInput = new DirectInput();

            // Find a Joystick Guid
            var joystickGuid = Guid.Empty;

            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad,
                        DeviceEnumerationFlags.AllDevices))
                joystickGuid = deviceInstance.InstanceGuid;

            foreach (var deviceInstance in directInput.GetDevices())
            {
                if (deviceInstance.InstanceName.Contains("X52 Pro"))
                {
                    Console.WriteLine(deviceInstance.InstanceName+", "+deviceInstance.InstanceGuid);
                    joystickGuid = deviceInstance.InstanceGuid;
                }
            }


            // If Joystick not found, throws an error
            if (joystickGuid == Guid.Empty)
            {
                Console.WriteLine("No joystick/Gamepad found.");
                Console.ReadKey();
                Environment.Exit(1);
            }
            // Instantiate the joystick
            joystick = new Joystick(directInput, joystickGuid);

            Console.WriteLine("Found Joystick/Gamepad with GUID: {0}", joystickGuid);

            // Query all suported ForceFeedback effects
            var allEffects = joystick.GetEffects();
            foreach (var effectInfo in allEffects)
                Console.WriteLine("Effect available {0}", effectInfo.Name);

            // Set BufferSize in order to use buffered data.
            joystick.Properties.BufferSize = 128;

            // Acquire the joystick
            joystick.Acquire();
            // Poll events from joystick
            while (true)
            {
                joystick.Poll();
                var datas = joystick.GetBufferedData();
                foreach (var state in datas)
                {
                    if (state.Offset == JoystickOffset.Y)
                    {
                        Y_Axis = (state.Value - (Y_MAX_VAL / 2.0)) / (Y_MAX_VAL / 2.0);
                        if (Math.Abs(Y_Axis) <= 0.025)
                            Y_Axis = 0;
                        if (Y_INVERT)
                            Y_Axis *= -1;
                    }

                    if (state.Offset == JoystickOffset.X)
                    {
                        X_Axis = (state.Value - (X_MAX_VAL / 2.0)) / (X_MAX_VAL / 2.0);
                        if (Math.Abs(X_Axis) <= DEAD_ZONE)
                            X_Axis = 0;
                        if (X_INVERT)
                            X_Axis *= -1;
                    }

                    if(state.Offset == JoystickOffset.Z)
                    {
                        throttle = (state.Value) / ((double)X_MAX_VAL);
                        if (THROTTLE_INVERT)
                            throttle = (1 - throttle);
                    }

                    if(state.Offset == JoystickOffset.Buttons0)
                    {
                        if (state.Value == 128)
                            fire = true;
                        else
                            fire = false;
                    }
                }
            }
            
        }

        private void updateTImer_Tick(object sender, EventArgs e)
        {
            lbl_X.Text = "X: " + (X_Axis * 100.0).ToString("F") + "%";
            lbl_Y.Text = "Y: " + (Y_Axis * 100.0).ToString("F") + "%";
            lbl_Throttle.Text = "Throttle: " + (throttle * 100.0).ToString("F") + "%";
            ckbx_fire.Checked = fire;
        }
    }
}
