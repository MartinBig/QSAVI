﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;



public class Rotate_Finger : MonoBehaviour{   
    // ------------------------- SERIAL PORT -------------------------

    //SerialPort sp = new SerialPort(portName,baudRate); // fill in com port
    SerialPort serialPort;
    // ------------------------- TIMING -------------------------

    //float serialPortRefreshPeriod;
    //float serialPortRequestDelayPeriod;
    public float fingMin = 0f;
    public float fingMax = 0f;
    public int potMin = 0;
    public int potMax = 676;
    public int baudRate = 0;
    public string portName = "";
    public int Datapacket = 0;
    
    

    // ------------------------- START -------------------------

    // Use this for initialization
    void Start()    {
        StartCoroutine("ProcessRotation");
    }

    // ------------------------- GET SERIAL PORT DATA -------------------------

    IEnumerator ProcessFingerRotation()
    {
        yield return new WaitForSeconds(ArduinoInterface.SERIAL_PORT_REFRESH_PERIOD);

        // Assume data will be valid
        bool isValidData = true;

        // Get the data
        string currentArduinoDataPacket = ArduinoInterface.currentArduinoDataPacket;
        // Split the line at each comma (this is how the code currently formats the output)
        string[] vec3 = currentArduinoDataPacket.Split(',');

        // Check that there are all 4 parts
        if (vec3.Length < 8)     {
            isValidData = false;
        }

        // If the data is valid, process it
            if (isValidData)  {

            float potval = float.Parse(vec3[Datapacket]);
            float fingerRotation = ((potval-potMin) / potMax)*(fingMax - fingMin) +fingMin; //relate potentiometer reading to rotation, potentiometer goes from 0 - 676
            transform.eulerAngles = new Vector3(0, fingerRotation, 0);
                // figure out how to make fingerRotation to actually move the finger
        }

        StartCoroutine("ProcessFingerRotation");
    }
}

