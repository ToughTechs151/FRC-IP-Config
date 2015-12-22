using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace FRC_IP_Config {
    public class Program {
        public static void Main( string[] args ) {
            string ipArgs = "interface ipv4 set address ";
            List<NetworkInterface> interfaces = new List<NetworkInterface>();
            int c = 1;
            foreach ( NetworkInterface inter in NetworkInterface.GetAllNetworkInterfaces() ) {
                if ( inter.Supports( NetworkInterfaceComponent.IPv4 ) ) {
                    interfaces.Add( inter );
                    Console.WriteLine( ( c++ ) + ". " + inter.Name );
                }
            }
            c = 0;
            do {
                Console.Write( "Choose network: " );
                try {
                    c = int.Parse( Console.ReadLine() );
                } catch (FormatException e) {}
            } while ( c < 1 || c > interfaces.Count );
            ipArgs += "\"" + interfaces[c - 1].Name + "\" ";

            Console.WriteLine( "\n1. DHCP\n2. Wired\n3. Wireless" );
            c = 0;
            do {
                Console.Write( "Choose setting: " );
                try {
                    c = int.Parse( Console.ReadLine() );
                } catch (FormatException e) {}
            } while ( c < 1 || c > 3 );
            string teamIp;
            switch ( c ) {
                case 1:
                    ipArgs += "dhcp";
                    break;
                case 2:
                    teamIp = teamNumberIP();
                    ipArgs += "static " + teamIp + "5 255.0.0.0 " + teamIp + "1 1";
                    break;
                case 3:
                    teamIp = teamNumberIP();
                    ipArgs += "static " + teamIp + "9 255.0.0.0 " + teamIp + "1 1";
                    break;
            }
            Process p = new Process();
            p.StartInfo.FileName = "netsh.exe";
            p.StartInfo.Arguments = ipArgs;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            Console.WriteLine( p.StandardOutput.ReadToEnd() );
            Console.WriteLine( "Press any key to continue..." );
            Console.ReadKey();
        }

        private static String teamNumberIP() {
            Console.WriteLine();
            int ? numIn = null;
            do {
                Console.Write( "Enter team number: " );
                try {
                    numIn = int.Parse( Console.ReadLine() );
                } catch (FormatException e) {}
            } while ( numIn == null );
            string num = numIn.ToString();
            if ( num.Length > 4 ) {
                num = num.Substring( 0, 4 );
            }
            if ( num.Length < 4 ) {
                for ( int i = 0, needed = 4 - num.Length; i < needed; i++ ) {
                    num = "0" + num;
                }
            }
            num = num.Insert( num.Length - 2, "." );
            if ( num[3] == '0' ) {
                num = num.Substring( 0, 3 ) + num[4];
            }
            if ( num[0] == '0' ) {
                num = num.Substring( 1 );
            }
            return "10." + num + ".";
        }
    }
}
