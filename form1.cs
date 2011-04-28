using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Utils.SysLogSender;

namespace SenderTester
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ComboBox cboPriority;
		private System.Windows.Forms.Label lblPriority;
		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.TextBox textBoxLogHost;
		private System.Windows.Forms.Label Loghost;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.cboPriority = new System.Windows.Forms.ComboBox();
			this.lblPriority = new System.Windows.Forms.Label();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.textBoxLogHost = new System.Windows.Forms.TextBox();
			this.Loghost = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(36, 300);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(100, 28);
			this.button1.TabIndex = 0;
			this.button1.Text = "Send Message";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// cboPriority
			// 
			this.cboPriority.Location = new System.Drawing.Point(140, 80);
			this.cboPriority.Name = "cboPriority";
			this.cboPriority.Size = new System.Drawing.Size(120, 21);
			this.cboPriority.TabIndex = 1;
			this.cboPriority.Text = "<<Select One>>";
			// 
			// lblPriority
			// 
			this.lblPriority.Location = new System.Drawing.Point(32, 84);
			this.lblPriority.Name = "lblPriority";
			this.lblPriority.Size = new System.Drawing.Size(100, 16);
			this.lblPriority.TabIndex = 2;
			this.lblPriority.Text = "Message Priority:";
			// 
			// txtMessage
			// 
			this.txtMessage.Location = new System.Drawing.Point(36, 116);
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.Size = new System.Drawing.Size(264, 172);
			this.txtMessage.TabIndex = 3;
			this.txtMessage.Text = "";
			// 
			// textBoxLogHost
			// 
			this.textBoxLogHost.Location = new System.Drawing.Point(140, 40);
			this.textBoxLogHost.Name = "textBoxLogHost";
			this.textBoxLogHost.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.textBoxLogHost.TabIndex = 4;
			this.textBoxLogHost.Text = "localhost";
			// 
			// Loghost
			// 
			this.Loghost.Location = new System.Drawing.Point(40, 36);
			this.Loghost.Name = "Loghost";
			this.Loghost.TabIndex = 5;
			this.Loghost.Text = "Syslog Server";
			
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(448, 437);
			this.Controls.Add(this.Loghost);
			this.Controls.Add(this.textBoxLogHost);
			this.Controls.Add(this.txtMessage);
			this.Controls.Add(this.lblPriority);
			this.Controls.Add(this.cboPriority);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
		
			  foreach(string s in Enum.GetNames(typeof(Sender.SeverityType)))
				this.cboPriority.Items.Add(s);
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if(this.cboPriority.SelectedIndex==-1)
				
			{
				MessageBox.Show("please choose a messagepriority, Captain!");
				this.cboPriority.Focus();
			}

			// create a new instance of the sender class
			// send the syslog server and the facility to log to
			// use the Facility Enum for the facility
			Sender s = new Sender("localhost",Sender.Facility.Local0);

			// send the log message
			// use the enum for severity type
			s.Send((Sender.SeverityType)this.cboPriority.SelectedIndex,DateTime.Now,this.txtMessage.Text.ToString());
			
			this.txtMessage.Text="";
		}
	}
}
