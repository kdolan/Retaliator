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

namespace Retaliator_Client
{
    public partial class frm_Primary : Form
    {

        public frm_Primary()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {



            //DirectOutputClass.DirectOutput_Initialize("test");
            //DirectOutputClass.DirectOutput_GetDeviceInstance(,);

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
            var joystick = new Joystick(directInput, joystickGuid);

            Console.WriteLine("Found Joystick/Gamepad with GUID: {0}", joystickGuid);

            // Query all suported ForceFeedback effects
            var allEffects = joystick.GetEffects();
            foreach (var effectInfo in allEffects)
                Console.WriteLine("Effect available {0}", effectInfo.Name);

            // Set BufferSize in order to use buffered data.
            joystick.Properties.BufferSize = 128;

            // Acquire the joystick
            joystick.Acquire();
            joystick.RunControlPanel();
            // Poll events from joystick
            while (true)
            {
                joystick.Poll();
                var datas = joystick.GetBufferedData();
                foreach (var state in datas)
                {
                    if (!(state.ToString().Contains("Rotation") || state.ToString().Contains("Slider")))
                        Console.WriteLine(state);
                }
                   
            }
        }
    }
}
