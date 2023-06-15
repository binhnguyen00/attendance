using System;
using System.Collections.Generic;

/* Function usages
  1. ReadGeneralLogData: Read Attendance records and Write them into the internal buffer of the PC. return true | false
  2. SSR_GetGeneralLogData: Read Attendance records one by one from the internal buffer.
*/

namespace Attendance {
  public class AttendanceUtils {
    public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();

    public bool checkValidUserInput(string ipAddress, string port) {
      if ("".Equals(ipAddress) || "".Equals(port)) {
        Console.WriteLine("IP or Port must be filled");
        return false; 
      } 
      return true;
    }

    public bool connect(string ipAddress, string port) {
      Console.Write("Connecting...");
      bool isValidInPut = checkValidUserInput(ipAddress, port);
      bool isConnected = false;
      if (isValidInPut) {
        isConnected = axCZKEM1.Connect_Net(ipAddress, Convert.ToInt32(port));
        if (isConnected) {
          /* 
            In fact,when you are using the tcp/ip communication,this parameter will be ignored, 
            that is any integer will all right.Here we use 1.
          */
          int iMachineNumber = 1; 
          axCZKEM1.RegEvent(iMachineNumber, 65535);
        }
      }
      return isConnected;
    }

    public List<string> getAllData() {
      Console.WriteLine("Reading...");
      string sdwEnrollNumber="";
      int idwTMachineNumber=0;
      int idwEMachineNumber=0;
      int idwVerifyMode=0;
      int idwInOutMode=0;
      int idwYear=0;
      int idwMonth=0;
      int idwDay=0;
      int idwHour=0;
      int idwMinute=0;
      int idwSecond=0;
      int idwWorkcode=0;
      
      int idwErrorCode=0;
      int iGLCount=0;
      int iIndex=0;

      List<string> records = new List<string>();
      int iMachineNumber = 1;
      // axCZKEM1.EnableDevice(iMachineNumber, false); // Disable the device

      bool readAble = axCZKEM1.ReadGeneralLogData(iMachineNumber);
      if (readAble) {
        bool isReadingData = axCZKEM1.SSR_GetGeneralLogData ( 
          iMachineNumber, out sdwEnrollNumber, out idwVerifyMode,
          out idwInOutMode, out idwYear, out idwMonth, out idwDay, 
          out idwHour, out idwMinute, out idwSecond, ref idwWorkcode
        );
        while (axCZKEM1.SSR_GetGeneralLogData ( 
          iMachineNumber, out sdwEnrollNumber, out idwVerifyMode,
          out idwInOutMode, out idwYear, out idwMonth, out idwDay, 
          out idwHour, out idwMinute, out idwSecond, ref idwWorkcode
        )) {
          records.Add(
            sdwEnrollNumber + "\t\t" + idwTMachineNumber + "\t\t" + idwEMachineNumber + "\t\t" + idwVerifyMode + "\t\t" + idwInOutMode +
            "\t\t" + idwYear + "/" + idwMonth + "/" + idwDay + " " + idwHour + ":" + idwMinute + ":" + idwSecond 
          );
        } 
      }
      
      // axCZKEM1.EnableDevice(iMachineNumber, true); // Enable the device
      return records;
    }


  }
}