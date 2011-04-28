using System;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using System.Net;

namespace Utils.SysLogSender
{	
	public class Sender
	{
		public enum SeverityType
		{
			Emergency =0,
			Alert=1,
			Critical=2,
			Error=3,
			Warning=4,
			Notice=5,
			Informational=6,
			Debug=7
		}
	

		public enum Facility: int
		{
			/*
			 Notes from RFC: 
					ftp://ftp.rfc-editor.org/in-notes/rfc3164.txt
	
			 Note 1 - Various operating systems have been found to utilize
				   Facilities 4, 10, 13 and 14 for security/authorization,
				   audit, and alert messages which seem to be similar.
			Note 2 - Various operating systems have been found to utilize
				   both Facilities 9 and 15 for clock (cron/at) messages.
			*/
			Kern	=0 , // 0 kernel messages
			User	=1, // 1 generic user-level messages
			Mail	=2, // 2 mail subsystem
			Daemon	=3, // 3 other system daemons
			Auth	=4,	// 4 security/authorization messages (DEPRECATED Use LOG_AUTHPRIV instead)
			Syslog	=5, // 5 messages generated internally by syslogd
			LPR		=6, // 6 line printer subsystem
			News	=7, // 7 USENET news subsystem
			UUCP	=8, // 8 UUCP subsystem
			Cron	=9, // 9 clock daemon (cron and at) 9+15 do the same depending on OS
			AuthPriv=10, // 10 security/authorization messages (private)
			FTP		=11,// 11 FTP
			NTP		=12, // 12 Network Time Protocol
			Audit	=13, // 13 log audit		
			Audit2	=14, // 14 log audit		
			CRON2	=15, // 15 clock daemon (cron and at) 9+15 do the same depending on OS
			Local0	=16, // 16 reserved for local use
			Local1	=17, // 17 reserved for local use
			Local2	=18, // 18 reserved for local use
			Local3	=19, // 19 reserved for local use
			Local4	=20, // 20 reserved for local use
			Local5	=21, // 21 reserved for local use
			Local6	=22, // 22 reserved for local use
			Local7	=23 // 23 reserved for local use
	}
		
		private static UdpClient udp;
		private static ASCIIEncoding ascii= new ASCIIEncoding();
		private string machine= System.Net.Dns.GetHostName() + " ";
		private string sysLogLocalHostIpAddress ;

		private int facilitiyId;
		private string sysLogRemoteHostIpAddress;



		public  Sender(string syslogserver, Facility f)
		{
			facilitiyId = (int)f;
			sysLogLocalHostIpAddress = Dns.Resolve(Dns.GetHostName()).AddressList[0].ToString();
			sysLogRemoteHostIpAddress = Dns.Resolve(syslogserver).AddressList[0].ToString();
				
		}



	
		public  void Send(string ipAddress,  string body)
		{		
			if(ipAddress==null || (ipAddress.Length<5)) ipAddress=Dns.Resolve(Dns.GetHostName()).AddressList[0].ToString();
			 this.Send(Sender.SeverityType.Warning,DateTime.Now,body);
		}

		public void Send(SeverityType severity, DateTime time, string body)
		{	
			
			udp = new UdpClient(this.sysLogRemoteHostIpAddress, 514);
			byte[] rawMsg;

			// facility *8 
			// + Severity
			// == Priority Type as a number
			int prioritynumber=(this.facilitiyId*8)+(int)severity;
						
			string[] strParams = { string.Format("<{0}>",prioritynumber.ToString()),time.ToString("MMM dd HH:mm:ss "),this.machine, body };
			
			rawMsg = ascii.GetBytes(string.Concat(strParams));
			udp.Send(rawMsg, rawMsg.Length);
			udp.Close();
			udp=null;			 
		}
	}
}

