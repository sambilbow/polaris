using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using Freya;
using UnityEngine.Events;

public class analogPinValues : MonoBehaviour
{
    //Create a new "SerialPort" connection called arduinoNanoStream
    private SerialPort arduinoNanoStream;


    // A couple of variables that help us parse the heart rate data over serial into three separate variables (Rate, Interval, Pulse). We use these variables in Update()
    private string arduinoNanoString;
    private string[] arduinoNanoList;

    // Variables for our heart rate data. "public" means they are displayed on this component in the Unity inspector 
    [Header("Heart Rate Sensor (A0)")]
    public int heartRate;
    public int heartInterval;
    public float heartPulse;
    public bool pulseState;
    public UnityEvent _onPulse;
    public UnityEvent _offPulse;
    private bool updatePulse = true;

    // Display the variables holding our analog pin (1 - 7) data. Uncomment as needed 
    [Header("Analog Pins (A1, A2...)")]
    public float a1Value;
    public float a2Value;
    //public float a3Value;
    //public float a4Value;
    //public float a5Value;
    //public float a6Value;
    //public float a7Value;


    // Start is called before the first frame update
    void Start()
    {
        /*
         * Define the serial port name and speed (Baud rate) to read the data stream at.
         * To find the port name, open Terminal (Mac) or Command Prompt (Windows) 
         * On Mac, use the command "ls /dev/tty.*", and find the port that includes "usbserial-xxxx"
         * On Windows you can access Device Manager via right-clicking the Windows icon on the Start 
         * Menu, and find the name under "Ports (COM & LPT)"
         * 
         * Mac serial port names: "/dev/tty.usbserial-xxxx"
         * Windows serial port names: "COMx"
         * 
         * Where 'x' is a number like "14310" (Mac) or "7" (Windows)
         * 
         * Place this name in its entirety into the below quotation marks.
         */

        // Configure our serial port variable with a port name and speed.
        arduinoNanoStream = new SerialPort("COM7", 250000);

        // Amount of time to wait after no data comes before timing out and telling us
        arduinoNanoStream.ReadTimeout = 100;

        // Open up the serial port for incoming data stream
        arduinoNanoStream.Open();
        Debug.Log("Opened Serial Port");
    }


    // Update is called once per frame
    void Update()
    {
        try
        {
            // Set this variable equal to the latest line of data from the Serial port stream (e.g. "/a0/0,600,0")
            arduinoNanoString = arduinoNanoStream.ReadLine();

            // Split the line into a list of strings separated by commas
            arduinoNanoList = arduinoNanoString.Split(',');
            heartRate = int.Parse(arduinoNanoList[0]);

            // Set our Heart Interval variable from line 35 equal to the second variable in this list
            heartInterval = int.Parse(arduinoNanoList[1]);

            // Set our Heart Pulse variable from line 36 equal to a scaled version of the third variable in this list
            heartPulse = Mathf.Round(Mathfs.RemapClamped(0f, 600f, 0f, 1f, float.Parse(arduinoNanoList[2])));

            // Set our Pulse state variable on line 37 true if the Pulse = 1. Check if updatePulse is true so that this doesn't run every frame, only when Pulse crosses the 0-1 threshold.
            if (updatePulse && heartPulse == 1f)
            {
                updatePulse = false;
                pulseState = true;
                _onPulse.Invoke();
            }

            if (!updatePulse && heartPulse == 0f)
            {
                pulseState = false;
                _offPulse.Invoke();
                updatePulse = true;
            }


            a1Value = int.Parse(arduinoNanoList[3]);
            a2Value = int.Parse(arduinoNanoList[4]);
            //a3Value = int.Parse(arduinoNanoList[5]);
            //a4Value = int.Parse(arduinoNanoList[6]);
            //a5Value = int.Parse(arduinoNanoList[7]);
            //a6Value = int.Parse(arduinoNanoList[8]);
            //a7Value = int.Parse(arduinoNanoList[9]);

        }

        // If there is an error, catch it here and display it in the Console as a message.
        catch (System.Exception error)
        {
            Debug.Log(error.Message);
        }
    }

    public void SetPulseState()
    {
        if (pulseState)
        {
            Debug.Log("pre invoke on");
            _onPulse.Invoke();
        }
        else
        {
            Debug.Log("pre invoke off");
            _offPulse.Invoke();
        }
    }

    // Close the serial port when coming out of Game Mode
    private void OnApplicationQuit()
    {
        arduinoNanoStream.Close();
        Debug.Log("Closed Serial Port");
    }
}
