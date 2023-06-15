using System;
using System.Collections.Generic;

namespace Attendance {
  internal class AttendanceMain {
    static void Main(string[] args) {
      AttendanceUtils utils = new AttendanceUtils();

      Console.WriteLine("Attendance Test");
      Console.WriteLine("===========================================");

      Console.WriteLine("Hint");
      Console.WriteLine("IP   : 27.72.244.199");
      Console.WriteLine("Port : 8080");

      Console.Write("IP Address: ");
      string ipAddress = Console.ReadLine();
      Console.WriteLine("You typed: " + ipAddress);

      Console.Write("Port: ");
      string port = Console.ReadLine();
      Console.WriteLine("You typed: " + port);

      // Connect
      bool isConnected = utils.connect(ipAddress, port);
      if (isConnected) {
        Console.WriteLine("Connected");
      } else {
        Console.WriteLine("Connection fail");
      }

      // Get Attendance data
      List<string> attendanceData = utils.getAllData();
      if (attendanceData.Count > 0) {
        Console.WriteLine("Enroll Number\t\tTMachineNumber\tEMachineNumber\tVerify Mode\tIn Out Mode\tDate Time");
        for (int i = 0; i < attendanceData.Count; i++) {
          Console.WriteLine(attendanceData[i]);
        } 
        Console.WriteLine("Total: " + attendanceData.Count);
      }

      Console.ReadKey();
    }
  }   
}