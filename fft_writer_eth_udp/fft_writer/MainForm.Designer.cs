/*
 * Created by SharpDevelop.
 * User: Lmx2315
 * Date: 08/08/2018
 * Time: 13:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace fft_writer
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TextBox my_port_box;
		private System.Windows.Forms.Button Btn_start;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label Test_l1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Button save_botton;
		private System.Windows.Forms.TextBox Console1;
		private System.Windows.Forms.TextBox text_fsemple;
		private System.Windows.Forms.Label Lab_Ft;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox text_N_fft;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cmbWindow;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.my_port_box = new System.Windows.Forms.TextBox();
            this.Btn_start = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Test_l1 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.save_botton = new System.Windows.Forms.Button();
            this.Console1 = new System.Windows.Forms.TextBox();
            this.text_fsemple = new System.Windows.Forms.TextBox();
            this.Lab_Ft = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.text_N_fft = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbWindow = new System.Windows.Forms.ComboBox();
            this.my_ip_box = new System.Windows.Forms.TextBox();
            this.dut_ip = new System.Windows.Forms.TextBox();
            this.dut_port = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.channal_box = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.open_file_BTN = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox_sch = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_ip_generator = new System.Windows.Forms.TextBox();
            this.textBox_port_generator = new System.Windows.Forms.TextBox();
            this.btn_telnet_gen = new System.Windows.Forms.Button();
            this.textBox_freq_gen = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox_level_gen = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox_com_port = new System.Windows.Forms.TextBox();
            this.btn_com_open = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox_att_m54 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textBox_freq_m54 = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox_freq_start = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.textBox_freq_step = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.textBox_freq_stop = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.textBox_freq_delay = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btn_corr_spectr = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.btn_com_open_2 = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.btn_calibrovka = new System.Windows.Forms.Button();
            this.btn_load_ach = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label_test = new System.Windows.Forms.Label();
            this.textBox_error_ach = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.textBox_freq_com2 = new System.Windows.Forms.TextBox();
            this.button_send_freq_com2 = new System.Windows.Forms.Button();
            this.label37 = new System.Windows.Forms.Label();
            this.textBox_com2 = new System.Windows.Forms.TextBox();
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.textBox_Level_in = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.textBox_Ku = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.textBox_Pin = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.textBox_din_diapazone = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.button_AM = new System.Windows.Forms.Button();
            this.button_CHIRP = new System.Windows.Forms.Button();
            this.button_PM = new System.Windows.Forms.Button();
            this.label49 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.textBox_CHIRP_DELTA_F = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.textBox_min_dindiapaz = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.генераторОсновногоСигналаToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mXGToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mXGToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.sMA100AToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.sMA100AToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mXGToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.sMA100AToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.label59 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.btn_cal_ch = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // my_port_box
            // 
            this.my_port_box.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.my_port_box.Location = new System.Drawing.Point(965, 269);
            this.my_port_box.Margin = new System.Windows.Forms.Padding(4);
            this.my_port_box.Multiline = true;
            this.my_port_box.Name = "my_port_box";
            this.my_port_box.Size = new System.Drawing.Size(76, 30);
            this.my_port_box.TabIndex = 0;
            this.my_port_box.Text = "8888";
            this.my_port_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Btn_start
            // 
            this.Btn_start.Location = new System.Drawing.Point(1048, 268);
            this.Btn_start.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_start.Name = "Btn_start";
            this.Btn_start.Size = new System.Drawing.Size(100, 30);
            this.Btn_start.TabIndex = 1;
            this.Btn_start.Text = "ВКЛ";
            this.Btn_start.UseVisualStyleBackColor = true;
            this.Btn_start.Click += new System.EventHandler(this.Button1Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 25;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Test_l1
            // 
            this.Test_l1.Location = new System.Drawing.Point(8, 136);
            this.Test_l1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Test_l1.Name = "Test_l1";
            this.Test_l1.Size = new System.Drawing.Size(133, 28);
            this.Test_l1.TabIndex = 5;
            this.Test_l1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // save_botton
            // 
            this.save_botton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.save_botton.Location = new System.Drawing.Point(468, 258);
            this.save_botton.Margin = new System.Windows.Forms.Padding(4);
            this.save_botton.Name = "save_botton";
            this.save_botton.Size = new System.Drawing.Size(99, 35);
            this.save_botton.TabIndex = 6;
            this.save_botton.Text = "SAVE";
            this.save_botton.UseVisualStyleBackColor = true;
            this.save_botton.Click += new System.EventHandler(this.Save_bottonClick);
            // 
            // Console1
            // 
            this.Console1.Location = new System.Drawing.Point(273, 614);
            this.Console1.Margin = new System.Windows.Forms.Padding(4);
            this.Console1.Multiline = true;
            this.Console1.Name = "Console1";
            this.Console1.Size = new System.Drawing.Size(335, 312);
            this.Console1.TabIndex = 7;
            // 
            // text_fsemple
            // 
            this.text_fsemple.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.text_fsemple.Location = new System.Drawing.Point(21, 127);
            this.text_fsemple.Margin = new System.Windows.Forms.Padding(4);
            this.text_fsemple.Name = "text_fsemple";
            this.text_fsemple.Size = new System.Drawing.Size(99, 26);
            this.text_fsemple.TabIndex = 8;
            this.text_fsemple.Text = "6250";
            this.text_fsemple.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.text_fsemple.TextChanged += new System.EventHandler(this.TextBox1TextChanged);
            // 
            // Lab_Ft
            // 
            this.Lab_Ft.Location = new System.Drawing.Point(128, 130);
            this.Lab_Ft.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lab_Ft.Name = "Lab_Ft";
            this.Lab_Ft.Size = new System.Drawing.Size(79, 28);
            this.Lab_Ft.TabIndex = 9;
            this.Lab_Ft.Text = "Ft(Mhz)";
            this.Lab_Ft.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(128, 159);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 28);
            this.label1.TabIndex = 12;
            this.label1.Text = "N (FFT)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // text_N_fft
            // 
            this.text_N_fft.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.text_N_fft.Location = new System.Drawing.Point(21, 161);
            this.text_N_fft.Margin = new System.Windows.Forms.Padding(4);
            this.text_N_fft.Name = "text_N_fft";
            this.text_N_fft.Size = new System.Drawing.Size(99, 26);
            this.text_N_fft.TabIndex = 11;
            this.text_N_fft.Text = "4096";
            this.text_N_fft.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.text_N_fft.TextChanged += new System.EventHandler(this.N_fftTextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 184);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 28);
            this.label2.TabIndex = 10;
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmbWindow
            // 
            this.cmbWindow.FormattingEnabled = true;
            this.cmbWindow.Location = new System.Drawing.Point(20, 195);
            this.cmbWindow.Margin = new System.Windows.Forms.Padding(4);
            this.cmbWindow.Name = "cmbWindow";
            this.cmbWindow.Size = new System.Drawing.Size(116, 24);
            this.cmbWindow.TabIndex = 14;
            this.cmbWindow.SelectedIndexChanged += new System.EventHandler(this.cmbWindow_SelectedIndexChanged);
            this.cmbWindow.ValueMemberChanged += new System.EventHandler(this.cmbWindow_ValueMemberChanged);
            // 
            // my_ip_box
            // 
            this.my_ip_box.Location = new System.Drawing.Point(1349, 37);
            this.my_ip_box.Margin = new System.Windows.Forms.Padding(4);
            this.my_ip_box.Name = "my_ip_box";
            this.my_ip_box.Size = new System.Drawing.Size(99, 22);
            this.my_ip_box.TabIndex = 15;
            this.my_ip_box.Text = "127.0.0.1";
            this.my_ip_box.Visible = false;
            // 
            // dut_ip
            // 
            this.dut_ip.Location = new System.Drawing.Point(1349, 131);
            this.dut_ip.Margin = new System.Windows.Forms.Padding(4);
            this.dut_ip.Name = "dut_ip";
            this.dut_ip.Size = new System.Drawing.Size(99, 22);
            this.dut_ip.TabIndex = 16;
            this.dut_ip.Text = "127.0.0.1";
            this.dut_ip.Visible = false;
            // 
            // dut_port
            // 
            this.dut_port.Location = new System.Drawing.Point(1349, 178);
            this.dut_port.Margin = new System.Windows.Forms.Padding(4);
            this.dut_port.Name = "dut_port";
            this.dut_port.Size = new System.Drawing.Size(99, 22);
            this.dut_port.TabIndex = 17;
            this.dut_port.Text = "666";
            this.dut_port.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1457, 41);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "my_IP";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1457, 131);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 16);
            this.label4.TabIndex = 19;
            this.label4.Text = "dut_IP";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(814, 275);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 16);
            this.label5.TabIndex = 20;
            this.label5.Text = "ПОРТ (8888 или 8889)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1457, 180);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 21;
            this.label6.Text = "dut_port";
            this.label6.Visible = false;
            // 
            // channal_box
            // 
            this.channal_box.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.channal_box.Location = new System.Drawing.Point(21, 97);
            this.channal_box.Margin = new System.Windows.Forms.Padding(4);
            this.channal_box.Name = "channal_box";
            this.channal_box.Size = new System.Drawing.Size(99, 26);
            this.channal_box.TabIndex = 22;
            this.channal_box.Text = "1";
            this.channal_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.channal_box.TextChanged += new System.EventHandler(this.channal_box_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 77);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 16);
            this.label7.TabIndex = 23;
            this.label7.Text = "№ канала (1 или 2)";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // open_file_BTN
            // 
            this.open_file_BTN.Location = new System.Drawing.Point(1349, 208);
            this.open_file_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.open_file_BTN.Name = "open_file_BTN";
            this.open_file_BTN.Size = new System.Drawing.Size(100, 28);
            this.open_file_BTN.TabIndex = 0;
            this.open_file_BTN.Text = "open_file";
            this.open_file_BTN.UseVisualStyleBackColor = true;
            this.open_file_BTN.Visible = false;
            this.open_file_BTN.Click += new System.EventHandler(this.Open_file_BTN_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.Timer2_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(20, 250);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 24;
            this.button1.Text = "AVG1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(20, 286);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 28);
            this.button2.TabIndex = 25;
            this.button2.Text = "AVG2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // textBox_sch
            // 
            this.textBox_sch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_sch.Location = new System.Drawing.Point(1048, 306);
            this.textBox_sch.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_sch.Multiline = true;
            this.textBox_sch.Name = "textBox_sch";
            this.textBox_sch.Size = new System.Drawing.Size(52, 30);
            this.textBox_sch.TabIndex = 26;
            this.textBox_sch.Text = "0";
            this.textBox_sch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 230);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 16);
            this.label8.TabIndex = 27;
            this.label8.Text = "сглаживание";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(187, 112);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 16);
            this.label9.TabIndex = 31;
            this.label9.Text = "PORT";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(283, 2);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 16);
            this.label10.TabIndex = 30;
            this.label10.Text = "генератор";
            // 
            // textBox_ip_generator
            // 
            this.textBox_ip_generator.Location = new System.Drawing.Point(232, 76);
            this.textBox_ip_generator.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_ip_generator.Name = "textBox_ip_generator";
            this.textBox_ip_generator.Size = new System.Drawing.Size(99, 22);
            this.textBox_ip_generator.TabIndex = 29;
            this.textBox_ip_generator.Text = "192.168.10.2";
            this.textBox_ip_generator.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox_port_generator
            // 
            this.textBox_port_generator.Location = new System.Drawing.Point(232, 108);
            this.textBox_port_generator.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_port_generator.Name = "textBox_port_generator";
            this.textBox_port_generator.Size = new System.Drawing.Size(99, 22);
            this.textBox_port_generator.TabIndex = 28;
            this.textBox_port_generator.Text = "5024";
            this.textBox_port_generator.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_telnet_gen
            // 
            this.btn_telnet_gen.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_telnet_gen.Location = new System.Drawing.Point(233, 200);
            this.btn_telnet_gen.Margin = new System.Windows.Forms.Padding(4);
            this.btn_telnet_gen.Name = "btn_telnet_gen";
            this.btn_telnet_gen.Size = new System.Drawing.Size(100, 32);
            this.btn_telnet_gen.TabIndex = 32;
            this.btn_telnet_gen.Text = "connect";
            this.btn_telnet_gen.UseVisualStyleBackColor = true;
            this.btn_telnet_gen.Click += new System.EventHandler(this.btn_telnet_gen_Click);
            // 
            // textBox_freq_gen
            // 
            this.textBox_freq_gen.Location = new System.Drawing.Point(232, 140);
            this.textBox_freq_gen.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_freq_gen.Name = "textBox_freq_gen";
            this.textBox_freq_gen.Size = new System.Drawing.Size(99, 22);
            this.textBox_freq_gen.TabIndex = 33;
            this.textBox_freq_gen.Text = "435000000";
            this.textBox_freq_gen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(187, 144);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 16);
            this.label11.TabIndex = 34;
            this.label11.Text = "Freq";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(340, 144);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(23, 16);
            this.label12.TabIndex = 35;
            this.label12.Text = "Гц";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(340, 176);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 16);
            this.label13.TabIndex = 38;
            this.label13.Text = "Дбм";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(187, 176);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 16);
            this.label14.TabIndex = 37;
            this.label14.Text = "Level";
            // 
            // textBox_level_gen
            // 
            this.textBox_level_gen.Location = new System.Drawing.Point(232, 172);
            this.textBox_level_gen.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_level_gen.Name = "textBox_level_gen";
            this.textBox_level_gen.Size = new System.Drawing.Size(99, 22);
            this.textBox_level_gen.TabIndex = 36;
            this.textBox_level_gen.Text = "2";
            this.textBox_level_gen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.Location = new System.Drawing.Point(1048, 46);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(100, 20);
            this.label15.TabIndex = 40;
            this.label15.Text = "COM PORT";
            // 
            // textBox_com_port
            // 
            this.textBox_com_port.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_com_port.Location = new System.Drawing.Point(1048, 73);
            this.textBox_com_port.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_com_port.Multiline = true;
            this.textBox_com_port.Name = "textBox_com_port";
            this.textBox_com_port.Size = new System.Drawing.Size(99, 44);
            this.textBox_com_port.TabIndex = 39;
            this.textBox_com_port.Text = "COM5";
            this.textBox_com_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_com_port.TextChanged += new System.EventHandler(this.textBox_com_port_TextChanged);
            // 
            // btn_com_open
            // 
            this.btn_com_open.Location = new System.Drawing.Point(1156, 131);
            this.btn_com_open.Margin = new System.Windows.Forms.Padding(4);
            this.btn_com_open.Name = "btn_com_open";
            this.btn_com_open.Size = new System.Drawing.Size(52, 45);
            this.btn_com_open.TabIndex = 41;
            this.btn_com_open.Text = "send";
            this.btn_com_open.UseVisualStyleBackColor = true;
            this.btn_com_open.Click += new System.EventHandler(this.btn_com_open_Click);
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.PortName = "COM3";
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(1003, 209);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(25, 16);
            this.label16.TabIndex = 47;
            this.label16.Text = "Дб";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(1003, 192);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(38, 16);
            this.label17.TabIndex = 46;
            this.label17.Text = "ATT:";
            // 
            // textBox_att_m54
            // 
            this.textBox_att_m54.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_att_m54.Location = new System.Drawing.Point(1048, 191);
            this.textBox_att_m54.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_att_m54.Multiline = true;
            this.textBox_att_m54.Name = "textBox_att_m54";
            this.textBox_att_m54.Size = new System.Drawing.Size(99, 44);
            this.textBox_att_m54.TabIndex = 45;
            this.textBox_att_m54.Text = "31,5";
            this.textBox_att_m54.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(1004, 160);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(23, 16);
            this.label18.TabIndex = 44;
            this.label18.Text = "Гц";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(1004, 140);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(36, 16);
            this.label19.TabIndex = 43;
            this.label19.Text = "Freq";
            // 
            // textBox_freq_m54
            // 
            this.textBox_freq_m54.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_freq_m54.Location = new System.Drawing.Point(1048, 131);
            this.textBox_freq_m54.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_freq_m54.Multiline = true;
            this.textBox_freq_m54.Name = "textBox_freq_m54";
            this.textBox_freq_m54.Size = new System.Drawing.Size(99, 44);
            this.textBox_freq_m54.TabIndex = 42;
            this.textBox_freq_m54.Text = "435000000";
            this.textBox_freq_m54.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1521, 25);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(198, 293);
            this.richTextBox1.TabIndex = 48;
            this.richTextBox1.Text = "";
            this.richTextBox1.Visible = false;
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(576, 83);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(23, 16);
            this.label20.TabIndex = 51;
            this.label20.Text = "Гц";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(388, 83);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(68, 16);
            this.label21.TabIndex = 50;
            this.label21.Text = "Freq_start";
            // 
            // textBox_freq_start
            // 
            this.textBox_freq_start.Location = new System.Drawing.Point(468, 79);
            this.textBox_freq_start.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_freq_start.Name = "textBox_freq_start";
            this.textBox_freq_start.Size = new System.Drawing.Size(99, 22);
            this.textBox_freq_start.TabIndex = 49;
            this.textBox_freq_start.Text = "429500000";
            this.textBox_freq_start.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(576, 115);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(23, 16);
            this.label22.TabIndex = 54;
            this.label22.Text = "Гц";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(388, 115);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(69, 16);
            this.label23.TabIndex = 53;
            this.label23.Text = "Freq_step";
            // 
            // textBox_freq_step
            // 
            this.textBox_freq_step.Location = new System.Drawing.Point(468, 111);
            this.textBox_freq_step.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_freq_step.Name = "textBox_freq_step";
            this.textBox_freq_step.Size = new System.Drawing.Size(99, 22);
            this.textBox_freq_step.TabIndex = 52;
            this.textBox_freq_step.Text = "250000";
            this.textBox_freq_step.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(576, 147);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(23, 16);
            this.label24.TabIndex = 57;
            this.label24.Text = "Гц";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(388, 147);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(69, 16);
            this.label25.TabIndex = 56;
            this.label25.Text = "Freq_stop";
            // 
            // textBox_freq_stop
            // 
            this.textBox_freq_stop.Location = new System.Drawing.Point(468, 143);
            this.textBox_freq_stop.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_freq_stop.Name = "textBox_freq_stop";
            this.textBox_freq_stop.Size = new System.Drawing.Size(99, 22);
            this.textBox_freq_stop.TabIndex = 55;
            this.textBox_freq_stop.Text = "440500000";
            this.textBox_freq_stop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(576, 179);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(26, 16);
            this.label26.TabIndex = 60;
            this.label26.Text = "мС";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(388, 179);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(79, 16);
            this.label27.TabIndex = 59;
            this.label27.Text = "Freq_Delay";
            // 
            // textBox_freq_delay
            // 
            this.textBox_freq_delay.Location = new System.Drawing.Point(468, 175);
            this.textBox_freq_delay.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_freq_delay.Name = "textBox_freq_delay";
            this.textBox_freq_delay.Size = new System.Drawing.Size(99, 22);
            this.textBox_freq_delay.TabIndex = 58;
            this.textBox_freq_delay.Text = "1000";
            this.textBox_freq_delay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.Location = new System.Drawing.Point(468, 205);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(99, 38);
            this.button3.TabIndex = 61;
            this.button3.Text = "перебор";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(37, 521);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(723, 28);
            this.progressBar1.TabIndex = 62;
            this.progressBar1.Visible = false;
            // 
            // btn_corr_spectr
            // 
            this.btn_corr_spectr.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_corr_spectr.Location = new System.Drawing.Point(1788, 197);
            this.btn_corr_spectr.Margin = new System.Windows.Forms.Padding(4);
            this.btn_corr_spectr.Name = "btn_corr_spectr";
            this.btn_corr_spectr.Size = new System.Drawing.Size(100, 35);
            this.btn_corr_spectr.TabIndex = 63;
            this.btn_corr_spectr.Text = "ON";
            this.btn_corr_spectr.UseVisualStyleBackColor = true;
            this.btn_corr_spectr.Visible = false;
            this.btn_corr_spectr.Click += new System.EventHandler(this.btn_corr_spectr_Click);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(1782, 178);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(107, 16);
            this.label28.TabIndex = 64;
            this.label28.Text = "Коррекция АЧХ";
            this.label28.Visible = false;
            // 
            // btn_com_open_2
            // 
            this.btn_com_open_2.Location = new System.Drawing.Point(1155, 192);
            this.btn_com_open_2.Margin = new System.Windows.Forms.Padding(4);
            this.btn_com_open_2.Name = "btn_com_open_2";
            this.btn_com_open_2.Size = new System.Drawing.Size(52, 43);
            this.btn_com_open_2.TabIndex = 65;
            this.btn_com_open_2.Text = "send";
            this.btn_com_open_2.UseVisualStyleBackColor = true;
            this.btn_com_open_2.Click += new System.EventHandler(this.btn_com_open_2_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(519, 2);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(77, 16);
            this.label29.TabIndex = 66;
            this.label29.Text = "генератор";
            // 
            // btn_calibrovka
            // 
            this.btn_calibrovka.Location = new System.Drawing.Point(1755, 28);
            this.btn_calibrovka.Margin = new System.Windows.Forms.Padding(4);
            this.btn_calibrovka.Name = "btn_calibrovka";
            this.btn_calibrovka.Size = new System.Drawing.Size(133, 28);
            this.btn_calibrovka.TabIndex = 67;
            this.btn_calibrovka.Text = "калибровка";
            this.btn_calibrovka.UseVisualStyleBackColor = true;
            this.btn_calibrovka.Visible = false;
            this.btn_calibrovka.Click += new System.EventHandler(this.btn_calibrovka_Click);
            // 
            // btn_load_ach
            // 
            this.btn_load_ach.Location = new System.Drawing.Point(1755, 64);
            this.btn_load_ach.Margin = new System.Windows.Forms.Padding(4);
            this.btn_load_ach.Name = "btn_load_ach";
            this.btn_load_ach.Size = new System.Drawing.Size(100, 28);
            this.btn_load_ach.TabIndex = 68;
            this.btn_load_ach.Text = "load кор ИХ";
            this.btn_load_ach.UseVisualStyleBackColor = true;
            this.btn_load_ach.Visible = false;
            this.btn_load_ach.Click += new System.EventHandler(this.btn_load_ach_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(1863, 69);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(88, 20);
            this.checkBox1.TabIndex = 69;
            this.checkBox1.Text = "показать";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label_test
            // 
            this.label_test.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_test.Location = new System.Drawing.Point(650, 562);
            this.label_test.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_test.Name = "label_test";
            this.label_test.Size = new System.Drawing.Size(249, 57);
            this.label_test.TabIndex = 70;
            this.label_test.Text = "режим ТЕСТ";
            this.label_test.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_test.Visible = false;
            this.label_test.Click += new System.EventHandler(this.label_test_Click);
            // 
            // textBox_error_ach
            // 
            this.textBox_error_ach.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_error_ach.Location = new System.Drawing.Point(232, 256);
            this.textBox_error_ach.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_error_ach.Multiline = true;
            this.textBox_error_ach.Name = "textBox_error_ach";
            this.textBox_error_ach.Size = new System.Drawing.Size(99, 33);
            this.textBox_error_ach.TabIndex = 72;
            this.textBox_error_ach.Text = "0";
            this.textBox_error_ach.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label30.Location = new System.Drawing.Point(168, 236);
            this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(219, 16);
            this.label30.TabIndex = 73;
            this.label30.Text = "нерав. коэффициента усиления";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(340, 267);
            this.label31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(25, 16);
            this.label31.TabIndex = 74;
            this.label31.Text = "Дб";
            // 
            // label32
            // 
            this.label32.Location = new System.Drawing.Point(144, 192);
            this.label32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(46, 28);
            this.label32.TabIndex = 75;
            this.label32.Text = "окно";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(1529, 397);
            this.label35.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(23, 16);
            this.label35.TabIndex = 81;
            this.label35.Text = "Гц";
            this.label35.Visible = false;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(1529, 377);
            this.label36.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(36, 16);
            this.label36.TabIndex = 80;
            this.label36.Text = "Freq";
            this.label36.Visible = false;
            // 
            // textBox_freq_com2
            // 
            this.textBox_freq_com2.Location = new System.Drawing.Point(1574, 388);
            this.textBox_freq_com2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_freq_com2.Name = "textBox_freq_com2";
            this.textBox_freq_com2.Size = new System.Drawing.Size(99, 22);
            this.textBox_freq_com2.TabIndex = 79;
            this.textBox_freq_com2.Text = "435000000";
            this.textBox_freq_com2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_freq_com2.Visible = false;
            // 
            // button_send_freq_com2
            // 
            this.button_send_freq_com2.Location = new System.Drawing.Point(1682, 386);
            this.button_send_freq_com2.Margin = new System.Windows.Forms.Padding(4);
            this.button_send_freq_com2.Name = "button_send_freq_com2";
            this.button_send_freq_com2.Size = new System.Drawing.Size(52, 28);
            this.button_send_freq_com2.TabIndex = 78;
            this.button_send_freq_com2.Text = "send";
            this.button_send_freq_com2.UseVisualStyleBackColor = true;
            this.button_send_freq_com2.Visible = false;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(1589, 331);
            this.label37.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(63, 16);
            this.label37.TabIndex = 77;
            this.label37.Text = "com port:";
            this.label37.Visible = false;
            // 
            // textBox_com2
            // 
            this.textBox_com2.Location = new System.Drawing.Point(1574, 352);
            this.textBox_com2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_com2.Name = "textBox_com2";
            this.textBox_com2.Size = new System.Drawing.Size(99, 22);
            this.textBox_com2.TabIndex = 76;
            this.textBox_com2.Text = "COM4";
            this.textBox_com2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_com2.Visible = false;
            // 
            // textBox_Level_in
            // 
            this.textBox_Level_in.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_Level_in.Location = new System.Drawing.Point(606, 398);
            this.textBox_Level_in.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_Level_in.Multiline = true;
            this.textBox_Level_in.Name = "textBox_Level_in";
            this.textBox_Level_in.Size = new System.Drawing.Size(90, 34);
            this.textBox_Level_in.TabIndex = 82;
            this.textBox_Level_in.Text = "-5";
            this.textBox_Level_in.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_Level_in.TextChanged += new System.EventHandler(this.textBox_Level_in_TextChanged);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(555, 378);
            this.label33.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(183, 16);
            this.label33.TabIndex = 83;
            this.label33.Text = "Уровень входного сигнала";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(704, 405);
            this.label34.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(34, 16);
            this.label34.TabIndex = 84;
            this.label34.Text = "Дбм";
            // 
            // textBox_Ku
            // 
            this.textBox_Ku.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_Ku.Location = new System.Drawing.Point(606, 461);
            this.textBox_Ku.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_Ku.Multiline = true;
            this.textBox_Ku.Name = "textBox_Ku";
            this.textBox_Ku.Size = new System.Drawing.Size(90, 31);
            this.textBox_Ku.TabIndex = 85;
            this.textBox_Ku.Text = "0";
            this.textBox_Ku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(554, 436);
            this.label38.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(197, 16);
            this.label38.TabIndex = 86;
            this.label38.Text = "Измеренный коэф. усиления";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(704, 466);
            this.label39.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(25, 16);
            this.label39.TabIndex = 87;
            this.label39.Text = "Дб";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(198, 453);
            this.label40.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(40, 16);
            this.label40.TabIndex = 90;
            this.label40.Text = "мкВт";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(48, 416);
            this.label41.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(187, 16);
            this.label41.TabIndex = 89;
            this.label41.Text = "Измерение мощности шума";
            // 
            // textBox_Pin
            // 
            this.textBox_Pin.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_Pin.Location = new System.Drawing.Point(93, 443);
            this.textBox_Pin.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_Pin.Multiline = true;
            this.textBox_Pin.Name = "textBox_Pin";
            this.textBox_Pin.Size = new System.Drawing.Size(97, 31);
            this.textBox_Pin.TabIndex = 88;
            this.textBox_Pin.Text = "0";
            this.textBox_Pin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(90, 478);
            this.label42.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(109, 16);
            this.label42.TabIndex = 91;
            this.label42.Text = "(полоса = 5МГц)";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1826, 397);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 28);
            this.button4.TabIndex = 92;
            this.button4.Text = "калиб. пост.";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(20, 324);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(100, 31);
            this.button5.TabIndex = 93;
            this.button5.Text = "SAVE DATA";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            this.button5.MouseHover += new System.EventHandler(this.button5_MouseHover);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(474, 466);
            this.label45.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(25, 16);
            this.label45.TabIndex = 97;
            this.label45.Text = "Дб";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(295, 436);
            this.label46.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(257, 16);
            this.label46.TabIndex = 96;
            this.label46.Text = "Измеренный динамический диапазон";
            // 
            // textBox_din_diapazone
            // 
            this.textBox_din_diapazone.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_din_diapazone.Location = new System.Drawing.Point(358, 461);
            this.textBox_din_diapazone.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_din_diapazone.Multiline = true;
            this.textBox_din_diapazone.Name = "textBox_din_diapazone";
            this.textBox_din_diapazone.Size = new System.Drawing.Size(108, 31);
            this.textBox_din_diapazone.TabIndex = 95;
            this.textBox_din_diapazone.Text = "0";
            this.textBox_din_diapazone.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(191, 79);
            this.label47.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(20, 16);
            this.label47.TabIndex = 99;
            this.label47.Text = "IP";
            // 
            // label44
            // 
            this.label44.Location = new System.Drawing.Point(1020, 340);
            this.label44.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(125, 28);
            this.label44.TabIndex = 100;
            this.label44.Text = "принятые пакеты";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label48
            // 
            this.label48.Location = new System.Drawing.Point(1053, 238);
            this.label48.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(92, 28);
            this.label48.TabIndex = 101;
            this.label48.Text = "Обмен с М54";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button_AM
            // 
            this.button_AM.BackColor = System.Drawing.SystemColors.Control;
            this.button_AM.Location = new System.Drawing.Point(616, 83);
            this.button_AM.Margin = new System.Windows.Forms.Padding(4);
            this.button_AM.Name = "button_AM";
            this.button_AM.Size = new System.Drawing.Size(52, 45);
            this.button_AM.TabIndex = 102;
            this.button_AM.Text = "AM";
            this.button_AM.UseVisualStyleBackColor = false;
            this.button_AM.Click += new System.EventHandler(this.button_AM_Click);
            // 
            // button_CHIRP
            // 
            this.button_CHIRP.BackColor = System.Drawing.SystemColors.Control;
            this.button_CHIRP.Location = new System.Drawing.Point(616, 143);
            this.button_CHIRP.Margin = new System.Windows.Forms.Padding(4);
            this.button_CHIRP.Name = "button_CHIRP";
            this.button_CHIRP.Size = new System.Drawing.Size(52, 45);
            this.button_CHIRP.TabIndex = 103;
            this.button_CHIRP.Text = "ЛЧМ";
            this.button_CHIRP.UseVisualStyleBackColor = false;
            this.button_CHIRP.Click += new System.EventHandler(this.button_CHIRP_Click);
            // 
            // button_PM
            // 
            this.button_PM.Location = new System.Drawing.Point(616, 203);
            this.button_PM.Margin = new System.Windows.Forms.Padding(4);
            this.button_PM.Name = "button_PM";
            this.button_PM.Size = new System.Drawing.Size(52, 45);
            this.button_PM.TabIndex = 104;
            this.button_PM.Text = "ФМ";
            this.button_PM.UseVisualStyleBackColor = true;
            this.button_PM.Click += new System.EventHandler(this.button_PM_Click);
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label49.Location = new System.Drawing.Point(745, 157);
            this.label49.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(34, 16);
            this.label49.TabIndex = 107;
            this.label49.Text = "КГц";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(676, 125);
            this.label50.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(87, 16);
            this.label50.TabIndex = 106;
            this.label50.Text = "полоса ЛЧМ";
            // 
            // textBox_CHIRP_DELTA_F
            // 
            this.textBox_CHIRP_DELTA_F.Enabled = false;
            this.textBox_CHIRP_DELTA_F.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_CHIRP_DELTA_F.Location = new System.Drawing.Point(676, 147);
            this.textBox_CHIRP_DELTA_F.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_CHIRP_DELTA_F.Multiline = true;
            this.textBox_CHIRP_DELTA_F.Name = "textBox_CHIRP_DELTA_F";
            this.textBox_CHIRP_DELTA_F.Size = new System.Drawing.Size(61, 37);
            this.textBox_CHIRP_DELTA_F.TabIndex = 105;
            this.textBox_CHIRP_DELTA_F.Text = "5000";
            this.textBox_CHIRP_DELTA_F.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_CHIRP_DELTA_F.TextChanged += new System.EventHandler(this.textBox_CHIRP_DELTA_F_TextChanged);
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(340, 329);
            this.label43.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(25, 16);
            this.label43.TabIndex = 110;
            this.label43.Text = "Дб";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label51.Location = new System.Drawing.Point(168, 298);
            this.label51.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(204, 16);
            this.label51.TabIndex = 109;
            this.label51.Text = "мин. динамический диапазон ";
            // 
            // textBox_min_dindiapaz
            // 
            this.textBox_min_dindiapaz.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_min_dindiapaz.Location = new System.Drawing.Point(232, 318);
            this.textBox_min_dindiapaz.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_min_dindiapaz.Multiline = true;
            this.textBox_min_dindiapaz.Name = "textBox_min_dindiapaz";
            this.textBox_min_dindiapaz.Size = new System.Drawing.Size(99, 33);
            this.textBox_min_dindiapaz.TabIndex = 108;
            this.textBox_min_dindiapaz.Text = "0";
            this.textBox_min_dindiapaz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(16, 445);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(70, 29);
            this.button6.TabIndex = 111;
            this.button6.Text = "ЗАМЕР";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.генераторОсновногоСигналаToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1218, 29);
            this.menuStrip1.TabIndex = 113;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // генераторОсновногоСигналаToolStripMenuItem1
            // 
            this.генераторОсновногоСигналаToolStripMenuItem1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.генераторОсновногоСигналаToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mXGToolStripMenuItem2,
            this.sMA100AToolStripMenuItem1});
            this.генераторОсновногоСигналаToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.генераторОсновногоСигналаToolStripMenuItem1.Name = "генераторОсновногоСигналаToolStripMenuItem1";
            this.генераторОсновногоСигналаToolStripMenuItem1.Size = new System.Drawing.Size(144, 25);
            this.генераторОсновногоСигналаToolStripMenuItem1.Text = "Выбор приборов";
            // 
            // mXGToolStripMenuItem2
            // 
            this.mXGToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mXGToolStripMenuItem3,
            this.sMA100AToolStripMenuItem2});
            this.mXGToolStripMenuItem2.Name = "mXGToolStripMenuItem2";
            this.mXGToolStripMenuItem2.Size = new System.Drawing.Size(293, 26);
            this.mXGToolStripMenuItem2.Text = "Генератор основного сигнала";
            // 
            // mXGToolStripMenuItem3
            // 
            this.mXGToolStripMenuItem3.Name = "mXGToolStripMenuItem3";
            this.mXGToolStripMenuItem3.Size = new System.Drawing.Size(158, 26);
            this.mXGToolStripMenuItem3.Text = "MXG";
            this.mXGToolStripMenuItem3.Click += new System.EventHandler(this.mXGToolStripMenuItem3_Click);
            // 
            // sMA100AToolStripMenuItem2
            // 
            this.sMA100AToolStripMenuItem2.Name = "sMA100AToolStripMenuItem2";
            this.sMA100AToolStripMenuItem2.Size = new System.Drawing.Size(158, 26);
            this.sMA100AToolStripMenuItem2.Text = "SMA 100 A";
            this.sMA100AToolStripMenuItem2.Click += new System.EventHandler(this.sMA100AToolStripMenuItem2_Click);
            // 
            // sMA100AToolStripMenuItem1
            // 
            this.sMA100AToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mXGToolStripMenuItem4,
            this.sMA100AToolStripMenuItem3});
            this.sMA100AToolStripMenuItem1.Name = "sMA100AToolStripMenuItem1";
            this.sMA100AToolStripMenuItem1.Size = new System.Drawing.Size(293, 26);
            this.sMA100AToolStripMenuItem1.Text = "Генератор сигнала помехи";
            // 
            // mXGToolStripMenuItem4
            // 
            this.mXGToolStripMenuItem4.Name = "mXGToolStripMenuItem4";
            this.mXGToolStripMenuItem4.Size = new System.Drawing.Size(158, 26);
            this.mXGToolStripMenuItem4.Text = "MXG";
            this.mXGToolStripMenuItem4.Click += new System.EventHandler(this.mXGToolStripMenuItem4_Click);
            // 
            // sMA100AToolStripMenuItem3
            // 
            this.sMA100AToolStripMenuItem3.Name = "sMA100AToolStripMenuItem3";
            this.sMA100AToolStripMenuItem3.Size = new System.Drawing.Size(158, 26);
            this.sMA100AToolStripMenuItem3.Text = "SMA 100 A";
            this.sMA100AToolStripMenuItem3.Click += new System.EventHandler(this.sMA100AToolStripMenuItem3_Click);
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label52.Location = new System.Drawing.Point(321, 39);
            this.label52.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(175, 20);
            this.label52.TabIndex = 114;
            this.label52.Text = "Генератор Сигнала";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label53.Location = new System.Drawing.Point(790, 39);
            this.label53.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(167, 20);
            this.label53.TabIndex = 115;
            this.label53.Text = "Генератор Помехи";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(787, 84);
            this.label54.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(20, 16);
            this.label54.TabIndex = 126;
            this.label54.Text = "IP";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(936, 181);
            this.label55.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(34, 16);
            this.label55.TabIndex = 125;
            this.label55.Text = "Дбм";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(783, 181);
            this.label56.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(41, 16);
            this.label56.TabIndex = 124;
            this.label56.Text = "Level";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(828, 177);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(99, 22);
            this.textBox1.TabIndex = 123;
            this.textBox1.Text = "2";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(936, 149);
            this.label57.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(23, 16);
            this.label57.TabIndex = 122;
            this.label57.Text = "Гц";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(783, 149);
            this.label58.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(36, 16);
            this.label58.TabIndex = 121;
            this.label58.Text = "Freq";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(828, 145);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(99, 22);
            this.textBox2.TabIndex = 120;
            this.textBox2.Text = "435000000";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button7.Location = new System.Drawing.Point(829, 205);
            this.button7.Margin = new System.Windows.Forms.Padding(4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(100, 32);
            this.button7.TabIndex = 119;
            this.button7.Text = "connect";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(783, 117);
            this.label59.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(46, 16);
            this.label59.TabIndex = 118;
            this.label59.Text = "PORT";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(828, 81);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(99, 22);
            this.textBox3.TabIndex = 117;
            this.textBox3.Text = "192.168.10.4";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(828, 113);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(99, 22);
            this.textBox4.TabIndex = 116;
            this.textBox4.Text = "5024";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_cal_ch
            // 
            this.btn_cal_ch.Location = new System.Drawing.Point(20, 362);
            this.btn_cal_ch.Margin = new System.Windows.Forms.Padding(4);
            this.btn_cal_ch.Name = "btn_cal_ch";
            this.btn_cal_ch.Size = new System.Drawing.Size(100, 30);
            this.btn_cal_ch.TabIndex = 127;
            this.btn_cal_ch.Text = "калибровка";
            this.btn_cal_ch.UseVisualStyleBackColor = true;
            this.btn_cal_ch.Click += new System.EventHandler(this.btn_cal_ch_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1218, 569);
            this.Controls.Add(this.btn_cal_ch);
            this.Controls.Add(this.label54);
            this.Controls.Add(this.label55);
            this.Controls.Add(this.label56);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label57);
            this.Controls.Add(this.label58);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label59);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label53);
            this.Controls.Add(this.label52);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label43);
            this.Controls.Add(this.label51);
            this.Controls.Add(this.textBox_min_dindiapaz);
            this.Controls.Add(this.label49);
            this.Controls.Add(this.label50);
            this.Controls.Add(this.textBox_CHIRP_DELTA_F);
            this.Controls.Add(this.button_PM);
            this.Controls.Add(this.button_CHIRP);
            this.Controls.Add(this.button_AM);
            this.Controls.Add(this.label48);
            this.Controls.Add(this.label44);
            this.Controls.Add(this.label47);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.label46);
            this.Controls.Add(this.textBox_din_diapazone);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label42);
            this.Controls.Add(this.label40);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.textBox_Pin);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.textBox_Ku);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.textBox_Level_in);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.textBox_freq_com2);
            this.Controls.Add(this.button_send_freq_com2);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.textBox_com2);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.textBox_error_ach);
            this.Controls.Add(this.label_test);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btn_load_ach);
            this.Controls.Add(this.btn_calibrovka);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.btn_com_open_2);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.btn_corr_spectr);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.textBox_freq_delay);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.textBox_freq_stop);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.textBox_freq_step);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.textBox_freq_start);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.textBox_att_m54);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.textBox_freq_m54);
            this.Controls.Add(this.btn_com_open);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.textBox_com_port);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.textBox_level_gen);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBox_freq_gen);
            this.Controls.Add(this.btn_telnet_gen);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox_ip_generator);
            this.Controls.Add(this.textBox_port_generator);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox_sch);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.open_file_BTN);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.channal_box);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dut_port);
            this.Controls.Add(this.dut_ip);
            this.Controls.Add(this.my_ip_box);
            this.Controls.Add(this.cmbWindow);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text_N_fft);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Lab_Ft);
            this.Controls.Add(this.text_fsemple);
            this.Controls.Add(this.Console1);
            this.Controls.Add(this.save_botton);
            this.Controls.Add(this.Test_l1);
            this.Controls.Add(this.Btn_start);
            this.Controls.Add(this.my_port_box);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(1234, 607);
            this.MinimumSize = new System.Drawing.Size(1234, 607);
            this.Name = "MainForm";
            this.Text = "fft_writer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox my_ip_box;
        private System.Windows.Forms.TextBox dut_ip;
        private System.Windows.Forms.TextBox dut_port;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox channal_box;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button open_file_BTN;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox_sch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_ip_generator;
        private System.Windows.Forms.TextBox textBox_port_generator;
        private System.Windows.Forms.Button btn_telnet_gen;
        private System.Windows.Forms.TextBox textBox_freq_gen;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox_level_gen;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox_com_port;
        private System.Windows.Forms.Button btn_com_open;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox_att_m54;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBox_freq_m54;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBox_freq_start;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBox_freq_step;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox textBox_freq_stop;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox textBox_freq_delay;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btn_corr_spectr;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button btn_com_open_2;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Button btn_calibrovka;
        private System.Windows.Forms.Button btn_load_ach;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label_test;
        private System.Windows.Forms.TextBox textBox_error_ach;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox textBox_freq_com2;
        private System.Windows.Forms.Button button_send_freq_com2;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox textBox_com2;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.TextBox textBox_Level_in;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox textBox_Ku;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox textBox_Pin;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.TextBox textBox_din_diapazone;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Button button_AM;
        private System.Windows.Forms.Button button_CHIRP;
        private System.Windows.Forms.Button button_PM;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.TextBox textBox_CHIRP_DELTA_F;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox textBox_min_dindiapaz;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem генераторОсновногоСигналаToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mXGToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem sMA100AToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mXGToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem sMA100AToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mXGToolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem sMA100AToolStripMenuItem3;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button btn_cal_ch;
    }
}
