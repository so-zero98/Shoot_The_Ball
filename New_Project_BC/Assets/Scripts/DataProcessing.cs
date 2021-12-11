using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Text.RegularExpressions;
using System;

public class DataProcessing : MonoBehaviour
{
    private int right = 0;

    public float pressure_val;
    public float roll_val;
    public float pitch_val;
    public float yaw_val;

    public string[] data_list;
    public string data;
    private static string DeviceName = "HoneyJam";
    private string subscribedService = "0000FFE0-0000-1000-8000-00805F9B34FB";
    private string subscribedCharacteristic = "0000FFE1-0000-1000-8000-00805F9B34FB";
    private string _deviceAddress;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        BluetoothLEHardwareInterface.Initialize(true, false, () =>
        {
            BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) =>
            {
                if (name.Contains(DeviceName))
                {
                    BluetoothLEHardwareInterface.StopScan();
                    _deviceAddress = address;
                    BluetoothLEHardwareInterface.ConnectToPeripheral(_deviceAddress, (address) =>
                    {
                    }, null, (address, service, characteristic) =>
                    {
                        BluetoothLEHardwareInterface.SubscribeCharacteristic(_deviceAddress, subscribedService, subscribedCharacteristic, null, (characteristic, bytes) =>
                        {
                            string str1 = Encoding.Default.GetString(bytes);
                            if (str1.Contains("s"))
                            {
                                if (right == 1)
                                {
                                    data_list = data.Split(',');
                                    data = "";

                                    pressure_val = float.Parse(data_list[1]);

                                    if (pressure_val <= 3)
                                        pressure_val = 0;

                                    yaw_val = float.Parse(data_list[2]);

                                    roll_val = float.Parse(data_list[3]);

                                    pitch_val = float.Parse(data_list[4]);
                                }
                                else
                                {
                                    right = 1;
                                }
                            }
                            if (right == 1)
                            {
                                data += str1;
                            }
                        });
                    }, null);
                }
            }, null);
        }, (error) =>
        {
            BluetoothLEHardwareInterface.Log("BLE Error: " + error);
        });
    }
}
