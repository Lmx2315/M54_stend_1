/*
 * Created by SharpDevelop.
 * User: Lmx2315
 * Date: 08/08/2018
 * Time: 13:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
//  #define TEST
#define WORK

using System;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Ports;
using System.IO;
using PlotWrapper;
using DSPLib;
using System.Numerics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using MinimalisticTelnet;
using System.Linq;
using System.Security.Policy;
using Accord;
using System.Xml.Serialization;
//using DiscreteFourierTransform;
//using FFTLibrary;

namespace fft_writer
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
        delegate void ShowMessageMethod(string msg);

        public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			Debug.WriteLine("Debug Information-Product Starting ");
			Debug.WriteLine("------------------------ ");            
        }
        public struct Config
        {
            public string Koeff_measure0;//
            public string Koeff_measure1;//
            public string Koeff_measure2;//
            public string Koeff_measure3;//
            public string N_filtr_usr0;  //коэффициент усреднения
            public string N_filtr_usr1;
            public string SPUR0_REMOVE_N;//удаление спур при измерении коэфф. шума
            public string SPUR1_REMOVE_N;
            public string FLAG_filtr;  //сглаживающий фильтр по умолчанию
            public string CORRECT_FREQ;//поправка на частоту цифрового гетеродина в поделке
            public string N_usredneniy_noise;//количество измерений шума
        }

        Config cfg = new Config();
        //----------ETH------------
        UdpClient _server      = null;
        IPEndPoint _client     = null;
        Thread _listenThread   = null;
        Thread _copyThread     = null;
        Thread _fftThread      = null;
        Thread _PcontrolThread = null;
        private bool _isServerStarted = false;

        Class1 rcv_func=new Class1();
		Form1 fft_form     = new Form1();
		
		static int  BUF_N= 64000;
		static int Fsample=12;

		Byte  [] RCV           = new byte[64000];
        int   [] packet_data_i = new int [64000];
        int   [] packet_data_q = new int [64000];

        int[] tst_data_i = new int[64000];
        int[] tst_data_q = new int[64000];

        double B_win = 1;
  
		int flag_NEW_FFT;
        byte[] buf = new byte[64];

        double filtr   = 0;
        int sch_packet = 0;
        int FLAG_filtr = 1; //по умлочанию стоит AVG1

        string fileName;
        string text_from_file;
        int Flag_comport_rcv = 0;
        string selectedWindowName;
        int FLAG_CALIBROVKA = 0;
        string DATA0_IQ_TEXT = ""; //в этой переменной храним записываемые данные, принятые из поделки
        string DATA1_IQ_TEXT = ""; //в этой переменной храним записываемые данные, принятые из поделки
        double Din_DIAP_min = 0;
        double Podav_DIAP_min = 0;
        double LEVEL_TEST_SIGNAL = 0;//измеренный уровень входного тестового сигнала в ДБм, применяется при тесте на подавление помехи за полосой (сначала измеряем уровень помехи в полосе сигнала и запоминаем его, затем помещаем помеху за полосу и сравниваем)
        double LVL_Pin_DBm=0;//входная измеренная мощность сигнала (для контроля коэфф. передачи и коэфф. шума)
        int CORRECT_FREQ = -1_000_000; //поправочная частота связаная с гетеродином -1_000_000
        double Koeff_measure0 = 1.0;
        double Koeff_measure1 = 5.0;
        double Koeff_measure2 = 5.0;
        double Koeff_measure3 = 5.0;
        int N_filtr_usr0 = 10;
        int N_filtr_usr1 = 33;
        int SPUR0_REMOVE_N = 30;
        int SPUR1_REMOVE_N = 1;
        double Koeff_usileniya_median=0;
        int N_usredneniy_noise=100;

         Plot fig1 = new Plot(15,"I Input", "Sample", "Вольт","","","","","");
	     Plot fig2 = new Plot(15,"Q Input", "Sample", "Вольт","","","","","");
		 Plot fig3 = new Plot(15,"FFT (dBm)", "кГц", "Mag (dBm)","","","","","");

        string  GEN_SIGN_HOST = "192.168.10.2";  //адреса генератора сигналов и генератора помехи
        int     GEN_SIGN_PORT = 5024;
        string  GEN_POMEH_HOST = "192.168.10.4";
        int     GEN_POMEH_PORT = 5024;

        IZM_Generator GEN_SIGN  = null;
        IZM_Generator GEN_POMEH = null;

        int FLAG_CORRECT = 0;
        string NAME_BLOCK;//тут храним номер блока в виде "160001"
        bool FLAG_CALIBR_CH=false;
        bool FLAG_SYNC_GEN_SIGN_POMEH=false;
        int  _FREQ_DELTA = 0; //разница между частотой сигнального генератора и помехового
        bool FLAG_TEST2 = false; //флаг того что запущен тест 2 (проверка при наличии помехи в полосе сигнала)
        bool FLAG_TEST3 = false; //флаг того что запущен тест 3 (проверка подавления помехи поставленной за полосой)
        bool FLAG_TEST5 = false; /*флаг что запущен тест 5 (проверка основных параметров при воздействии помехи за полосой )*/
        bool FLAG_KORR = false;
        private void MainForm_FormClosing(Object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show(
               "Вы действительно хотите выйти из программы?",
               "Завершение программы",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Warning
              );
            if (dialog == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
            if (_listenThread!=null) _listenThread.Abort();
            if (_copyThread!=null) _copyThread.Abort();
            if (_fftThread!=null) _fftThread.Abort();
            if (_isServerStarted)   _server.Close();
            if (_PcontrolThread!=null) _PcontrolThread.Abort();
            //Changet state to indicate the server stops.
            _isServerStarted = false;

           // this.Dispose();
           // Application.Exit();
        }
        //-------------------eth-------------------------
        string FLAG_THREAD="start";
        
        private void Start()
        {
            IPAddress my_ip;
            UInt16 my_port;

            my_ip = IPAddress.Parse(my_ip_box.Text);
            my_port = UInt16.Parse(my_port_box.Text);

            fig3.Show();//показываем окно спектра 
            // IPEndPoint serverEnd = new IPEndPoint(IPAddress.Any, 1234);
            //Create the server.
            IPEndPoint serverEnd = new IPEndPoint(my_ip, my_port);
            _server = new UdpClient(serverEnd);
            _server.Client.ReceiveBufferSize = 8192 * 300;//увеличиваем размер приёмного буфера!!!
            //Debug.WriteLine("Waiting for a client...");
            //Create the client end.
            //_client = new IPEndPoint(IPAddress.Any, 0);
           
            //Start listening.
            Thread _listenThread = new Thread(new ThreadStart(Listening));//тред приёма данных по UDP
                   _listenThread.Start();
            _listenThread.IsBackground = true;//делает поток фоновым который завершается по закрытию основного приложения

            //Start copy-ing.
            Thread _copyThread = new Thread(new ThreadStart(DATA_COPY));//тред копирования данных в буфер обработки
                   _copyThread.Start();
            _copyThread.IsBackground = true;

            //Start fft-ing.
              Thread _fftThread = new Thread(new ThreadStart(fft_out));//тред расчёта fft
              _fftThread.Start();
              _fftThread.IsBackground = true;
            
            Thread _PcontrolThread = new Thread(new ThreadStart(Pout_control));
            _PcontrolThread.Start();
            _PcontrolThread.IsBackground=true;            

            //Change state to indicate the server starts.
            _isServerStarted = true;

            //-----шлём приветствие программсе на си--------------
            UdpClient client = new UdpClient();
            client.Connect("127.0.0.1", 666);

            string msg = "Hello bro!!!\n";
            byte[] data = Encoding.UTF8.GetBytes(msg);
            int number_bytes = client.Send(data, data.Length);
            client.Close();

             selectedWindowName = cmbWindow.SelectedValue.ToString();
        }

        private void Stop()
        {
            try
            {
                //Stop listening.
                 FLAG_THREAD = "close";
                _listenThread.Join();
                _server.Close();
                _isServerStarted = false;
                _copyThread.Join();
                _fftThread.Join();
            }
            catch (Exception excp)
            { 
                Console.WriteLine(excp.Message);               
            }
        }

        Object DATAIQ = new Object();

        int FLAG_UDP_RCV = 0;
        string COMMAND_FOR_SERVER="HOMEWORLD!!!\n";
        int DATA_size = 0;
        byte[] DATA_SW0;//буфер0 приёма данных с шины SW
        byte[] DATA_SW1;//буфер1 приёма данных с шины SW
        UInt16 FLAG_BUF_SW = 0;
        private void Listening()
        {           
            //Listening loop.
            while ((true)&&(FLAG_THREAD=="start"))
            {
                if (this.Disposing) return;
                try
                { //receieve a message form a client.
                    lock (DATAIQ)
                    {
                        if (FLAG_BUF_SW == 1)  //тут висим пока не приходят данные
                        {
                            DATA_SW0 = _server.Receive(ref _client);
                            FLAG_BUF_SW = 0;
                        }
                        else
                        {
                            DATA_SW1 = _server.Receive(ref _client);
                            FLAG_BUF_SW = 1;
                        }
                    }
                    
                }
                catch (Exception excp)
                {
                    Console.WriteLine(excp.Message);
                }
                FLAG_UDP_RCV = 1;//принят пакет
                sch_packet++;

                UdpClient client = new UdpClient();
                client.Connect("127.0.0.1", 666);

                byte[] data;
                string       msg = COMMAND_FOR_SERVER;
                            data = Encoding.UTF8.GetBytes(msg);
                int number_bytes = client.Send(data, data.Length);
                client.Close();
            }
        }

        string FLAG_DATA_NEW;//флаг показывает в каком массиве текущие данные
        void DATA_COPY ()
        {
           while ((true) && (FLAG_THREAD == "start"))
            {
                if (FLAG_UDP_RCV == 1)
                {
                    lock (DATAIQ)
                      {
                        if (FLAG_BUF_SW == 0)
                        {
                            Array.Copy(DATA_SW0, RCV, DATA_SW0.Length);//копируем массив отсчётов в форму обработки 
                            FLAG_DATA_NEW = "0";
                            DATA_size = DATA_SW0.Length;
                        }
                        if (FLAG_BUF_SW == 1)
                        {
                            Array.Copy(DATA_SW1, RCV, DATA_SW1.Length);//копируем массив отсчётов в форму обработки
                            FLAG_DATA_NEW = "1";
                            DATA_size = DATA_SW1.Length;
                        }
                        FLAG_UDP_RCV = 0;
                    }
                }
                Thread.Sleep(0);
            }
        }

        byte[] BUFFER_1 = new byte[64000];
        byte[] BUFFER_2 = new byte[64000];

        void MSG_collector()
        {
           if (RCV[0] == 1) Array.Copy(RCV, BUFFER_1, BUF_N*4);//копируем массив отсчётов в форму обработки 
           if (RCV[0] == 2) Array.Copy(RCV, BUFFER_2, BUF_N*4);//

           if (Convert.ToByte(channal_box.Text) == 1) { BUF_convert(BUFFER_1, DATA_size); }
           if (Convert.ToByte(channal_box.Text) == 2) { BUF_convert(BUFFER_2, DATA_size); }
 
           if (flag_NEW_FFT == 0)
            {
                Array.Copy(data_0_i, packet_data_q, BUF_N);//копируем массив отсчётов в форму обработки	
                Array.Copy(data_0_q, packet_data_i, BUF_N);//копируем массив отсчётов в форму обработки	
               
                flag_NEW_FFT = 1;//сообщаем форме что пришёл новый массив fft
           //     fft_out();
            }
        }

        private void ShowMsg(string msg)
        {
            this.Console1.Text += msg + "\r\n";
        }
        //-----------------------------------------------

        Byte sch =0;

        Byte[] cos_gen ()
        {
            Byte[] data = new byte[1446];

            int i = 0;
            int n = 0;
            double z = 0;
            int x = 0;

            if (sch < 254) sch++; else sch = 0;

            i = 6;

            data[0] = 0;
            data[1] = 1;
            data[2] = 0;
            data[3] = 0;
            data[4] = 0;
            data[5] = sch;

            for (n=0;n<360;n++)
            {
                z = 30000 * Math.Cos(2 * Math.PI * i / 20);
                x = Convert.ToInt16(z);
             //   Debug.WriteLine("x:" + x);
                data[i  ] = Convert.ToByte((x >> 8) & 0xff);
                data[i+1] = Convert.ToByte((x >> 0) & 0xff);
        
                z = 30000 * Math.Cos(2 * Math.PI * i / 20);
                x = Convert.ToInt16(z);

                data[i+2] = Convert.ToByte((x >> 8) & 0xff);
                data[i+3] = Convert.ToByte((x >> 0) & 0xff);
    
                i = i + 4;
            }
            return data;
        }

        int N_usred = 20;
        int N_sch_timer1=0;
   
        private void timer1_Tick(object sender, EventArgs e)
        {
            double[] z = new double[1];
            
                if ((FLAG_DATA_NEW == "0") || (FLAG_DATA_NEW == "1"))
                {
                    selectedWindowName = cmbWindow.SelectedValue.ToString();//выбираем тип окна для БПФ
                    MSG_collector();
                    FLAG_DATA_NEW = "";
                }
                Ku_control();
                DISPLAY();
                z[0] = LVL_Pin_DBm;

            if (N_sch_timer1 > 0)
            {
                N_sch_timer1--;
                filtr_usr_rst(z, MeM2, N_usred,false);//
                var y = 1_000_000 * Math.Pow(10, (z[0] / 10));//чтобы получить нВт
                var x = Math.Round(y, 2);                
                textBox_Pin.Text = Convert.ToString(x); //выводим усреднённое значение входной мощности сигнала
            } else
            {
                 filtr_usr_rst(z, MeM2, N_usred, true);
            }
                

                textBox_sch.Text = Convert.ToString(sch_packet);
                sch_packet = 0;
                filtr = 12000 / BUF_N * B_win;
                label8.Text = "полоса фильтра:" + Convert.ToString(filtr) + "кГц";           
        }

        double func1(double lvl, double x)
        {
            double solv=0;
            double koeff = 0;

            if ((x > -2450) && (x < 2450)) koeff =   -2 ; else
            if ((x > -2550) && (x < 2550)) koeff = -4.5 ; else
            if ((x > -2650) && (x < 2650)) koeff = -5.23; else
            if ((x > -2750) && (x < 2750)) koeff = -10.0 ; else
            if ((x > -2850) && (x < 2850)) koeff = -14.0 ; else
            if ((x > -2950) && (x < 2950)) koeff = -21.2; else
            if ((x > -3050) && (x < 3050)) koeff = -37.2; else
            if ((x > -3150) && (x < 3150)) koeff = -64.2; else koeff = -70.0;

            solv = lvl - koeff;
            solv = (0.001) * Math.Pow(10, (solv / 10));
            solv = Math.Pow((solv * 50), 0.5);//в вольтах

            return solv;
        }

        void Ku_control ()
        {
            //32000 - 1Вольт

            var Lvl_pom=func1(B_out, B_xout);
            var Level_in = Convert.ToDouble(textBox_Level_in.Text);
            var Level_in_mW = (0.001)*Math.Pow(10, (Level_in / 10));
            var Level_in_V = Math.Pow((Level_in_mW * 50), 0.5);
            var Level_out = A_out/32767*1;//выходной сигнал в вольтах
            var Ku = Math.Round((20 * Math.Log10(Level_out / Level_in_V)), 2);

            //-------------------------
            var poteri_sig   = Convert.ToDouble(toolStripTextBox1.Text);
            var poteri_pomeh = Convert.ToDouble(toolStripTextBox2.Text);
            if ((checkBox2.Checked)&&(FLAG_TEST2))
            {
                try 
                {
                    var z1 = Math.Pow(Lvl_pom, 2);
                    var z2 = Math.Pow(Level_out, 2);
                    var z3 = Math.Pow(z1 + z2, 0.5);//считаем геометрическую сумму
                    var z4 = Math.Pow(z3, 2) / 50 * 1000;//переводим в мВт
                    var Lvl = Math.Round((10 * Math.Log10(z4)), 2);
                    var a = Math.Pow(10, (Convert.ToDouble(textBox1.Text) - poteri_pomeh) / 10);      //это милливаты
                    var b = Math.Pow(10, ((Convert.ToDouble(textBox_level_gen.Text) - poteri_sig) / 10));
                    var c = a + b;
                    var d = Math.Round((Math.Log10(c) * 10), 2);
                    Ku = Lvl - d;
                    textBox_Ku.Text = Convert.ToString(Ku);
                    //     textBox_Ku.BackColor = Color.Blue;
                    textBox_Level_in.Text = d.ToString();

                } catch
                { }


            } else
            {
          
                textBox_Ku.Text = Convert.ToString(Ku);
         //     textBox_Ku.BackColor = Color.White;
                try
                {
                    textBox_Level_in.Text = (Convert.ToDouble(textBox_level_gen.Text) - poteri_sig).ToString();
                } catch
                {

                }

                
            }
            
        }

        double[][] MeM2 = new double[300][];
        void Pout_control ()
        {
            int i;
            for (i=0;i< N_usred; i++) MeM2[i] = new double[1];//создаём зубчатый массив один раз , потом дальше поток ввходит в while и не возвращается сюда.

            while (true)
            {
                double A = A_out;           //распаковываем переменную
                var Lvl_U = A / 32767 * 1;  //выходной сигнал в вольтах
                var Lvl_P = Math.Pow(Lvl_U, 2) / 50 * 1000;    //мощность в мВт
                if (Lvl_P == 0) Lvl_P = 0.0000000001;
                var Lvl_Pdbm = 10 * System.Math.Log10(Lvl_P); //мощность в ДБм
                var x = Math.Round(Convert.ToDouble(Lvl_Pdbm), 2);                                
                LVL_Pin_DBm = x;
                Thread.Sleep(0);
            }          
        }

        void MainFormLoad(object sender, EventArgs e)
		{
            CFG_load();//загружаем предустановки
            IH_load();//загружаем реальную АЧХ для блока из неё быдут высчитаны поправочные коэффициенты
            
            BUF_N =Convert.ToInt16(text_N_fft.Text);
			// Load window combo box with the Window Names (from ENUMS)
            cmbWindow.DataSource = Enum.GetNames(typeof(DSPLib.DSP.Window.Type));
            cmbWindow.SelectedIndex = 5;//Hann
            //Start();

            //выводим всплывающую подсказку!
            ToolTip t = new ToolTip(); 
            t.SetToolTip(button5, "Сохранить принятые данные в файл");
            t.SetToolTip(save_botton, "Сохранить измеренные значения перебора частот в файл");
            t.SetToolTip(textBox_port_generator, "MXG - 5024 , SMA 100A - 5025");
            t.SetToolTip(textBox_din_diapazone, "в режиме синхронизации генератора сигналов и помехи показывает динамический диапазон между несущими и спурами \r\n в режиме без синхронизации - динамический диапазон между основной несущей и спурами!");
            t.SetToolTip(channal_box, "Номер отображаемого канала 1 или 2");
            t.SetToolTip(text_fsemple, "Тактовая частота по выходу приёмника, не меняется.");
            t.SetToolTip(text_N_fft, "Размер окна обработки дискретного преобразования Фурье (128 - 8192)");
            t.SetToolTip(cmbWindow, "Вид сглаживающего окна для ДПФ");
            t.SetToolTip(button1, "Сглаживающий видеофильтр 1");
            t.SetToolTip(button2, "Сглаживающий видеофильтр 2");
            t.SetToolTip(btn_cal_ch, "Запуск калибровки канала приёмника перед проводимыми измерениями на динамический диапазон");
            t.SetToolTip(button6, "Проведение замера мощности шума в полной полосе приёмника, при измерении коэффициента шума");
            t.SetToolTip(textBox_Pin, "Отображение мощности шума в полной полосе пропускания приёмника, измерение производится по нажатию кнопки <Замер>");
            t.SetToolTip(textBox_min_dindiapaz, "Измеренное значение минимального динамического диапазона");
            t.SetToolTip(textBox_error_ach, "Измеренное значение неравномерности коэффициента усиления");
            t.SetToolTip(btn_telnet_gen, "Отправка команды управления в Генератор сигналов");
            t.SetToolTip(textBox_level_gen, "Выходной уровень сигнала генератора");
            t.SetToolTip(textBox_freq_gen, "Частота сигнала генератора");
            t.SetToolTip(textBox_port_generator, "Рабочий порт управления генератора по сети ethernet");
            t.SetToolTip(textBox_ip_generator, "Рабочий адрес генератора по сети ethernet");
            t.SetToolTip(checkBox_sync, "Синхронизация измерительный генераторов при тесте на динамический диапазон при подаче помехи");
            t.SetToolTip(textBox_Level_in, "Заданный уровень сигнала на входе Приёмника, необходим для измерения реального коэффициента передачи.");
            t.SetToolTip(textBox_Ku, "Расчитанный коэффициент усиления Приёмника, исходя из размаха сигнала на АЦП канала и уровня входного сигнала Приёмника.");
            t.SetToolTip(save_botton, "Сохранение данных неравномерности коэффициента усиления в частотном диапазоне, необходимо предварительное сканирование.");
            t.SetToolTip(button3, "Запуск режима сканирования в диапазоне частот");
            t.SetToolTip(textBox_freq_delay, "Задержка переключения частот, 2000 мс - минимальная величена с запасом обеспечивающая время установления сигнала после одновременной перестройки генераторов и приёмника");
            t.SetToolTip(textBox_freq_stop, "Конечная частота перестройки генератора");
            t.SetToolTip(textBox_freq_start, "Начальная частота перестройки генератора");
            t.SetToolTip(textBox_freq_step, "Шаг перестройки частоты генератора");
            t.SetToolTip(textBox_CHIRP_DELTA_F, "Полоса частот занимаемая сигналом ЛЧМ");
            t.SetToolTip(button_AM, "Включение АМ модуляции с предустановленными параметрами, для демонстрации");
            t.SetToolTip(button_CHIRP, "Включение ЛЧМ модуляции");
            t.SetToolTip(button_PM, "Включение ФМ модуляции");
            t.SetToolTip(textBox_com_port, "Адрес COM порта для управления приёмником (используется младший компорт и двух)");
            t.SetToolTip(textBox_freq_m54, "Частота настройки приёмника");
            t.SetToolTip(textBox_att_m54, "Уровень ослабления входного аттенюатора, соответствует номеру канала");
            t.SetToolTip(Btn_start, "Включает приём данных для отображения спектра, от приёмного модуля по сети ethernet");
            t.SetToolTip(my_port_box, "Номер приёмного порта сети ethernet, если производится запуск двух копий программы (8888 и 8889) для одновременного отображения двух каналов");
            t.SetToolTip(textBox_sch, "Число принятых программой пакетов в интервале обработки");    
	        t.SetToolTip(button9,  "тест №2(A) проверка динамического диапазона и неравномерности АЧХ, при воздействии помехи в полосе обработки");
            t.SetToolTip(button16, "тест №2(Б) проверка коэффициента усиления, при воздействии помехи в полосе обработки");
            t.SetToolTip(button8,  "тест №1 проверка динамического диапазона и неравномерности АЧХ");
            t.SetToolTip(button10, "тест №3 проверка уровня подавления сигнала помехи в диапазоне 395 - 419,5 МГц");
            t.SetToolTip(button11, "тест №4 проверка уровня подавления сигнала помехи в диапазоне 450,5 - 475 МГц");
            t.SetToolTip(button12, "тест №5 проверка динамического диапазона при наличии входного сигнала и сигнала помехи в диапазоне 395 - 419,5 МГц");
            t.SetToolTip(button13, "тест №6 проверка динамического диапазона при наличии входного сигнала и сигнала помехи в диапазоне 450,5 - 475 МГц");
            t.SetToolTip(button14, "тест №7 проверка динамического диапазона и неравномерности АЧХ, при наличии сигнала помехи за диапазоном приёма <419.5 МГц");
            t.SetToolTip(button15, "тест №8 проверка динамического диапазона и неравномерности АЧХ, при наличии сигнала помехи за диапазоном приёма >450.5 МГц");
            t.SetToolTip(textBox_Ku, "Для измерение коэффициента услиления изделия, при наличии сложного сигнала в полосе пропускания - необходимо провести калибровку!");
            t.SetToolTip(button17, "тест №9 демонстрация полосы прокускания, на экране показываются две моночастоты с краёв спектра обработки, между ними 5 МГц");

            /*
            t.SetToolTip(button_AM,"Генератор сигнала должен быть SMA 100A!");
            t.SetToolTip(button_CHIRP, "Генератор сигнала должен быть SMA 100A!");
            t.SetToolTip(button_PM, "Генератор сигнала должен быть SMA 100A!");
            */

            IZM_Generator GEN_MXG1 = new IZM_Generator("MXG");       //это управление генератором
            IZM_Generator GEN_MXG2 = new IZM_Generator("MXG");       //это управление генератором
            IZM_Generator GEN_SMA = new IZM_Generator("SMA 100 A");

            GEN_SIGN  = GEN_MXG1;
            GEN_POMEH = GEN_MXG2;

            GEN_SIGN.host = textBox_ip_generator.Text;
            GEN_SIGN.port = Convert.ToInt32(textBox_port_generator.Text);

            GEN_POMEH.host = textBox3.Text;
            GEN_POMEH.port = Convert.ToInt32(textBox4.Text);

            mXGToolStripMenuItem3.BackColor = Color.Aqua;
            mXGToolStripMenuItem4.BackColor = Color.Aquamarine;

            button_AM.Enabled = false;
            button_CHIRP.Enabled = false;
            button_PM.Enabled = false;
            textBox_error_ach.Text="0";
            MessageBox.Show("Перед работой:\r\nУстановите связь по COM порту с изделием.\r\nУбедитесь в правильности IP адресов измерительных приборов.\r\nВыберите тип измерительных генераторов.");

            //    MainForm
#if TEST
            label_test.Visible = true;
#endif
        }

        // Calculate log(number) in the indicated log base.
        private double LogBase(double number, double log_base)
        {
            return Math.Log(number) / Math.Log(log_base);
        }

        private System.Numerics.Complex[] POST_REMOVE (System.Numerics.Complex[] A) //функция удаляет постоянную составляющую из вектора
        {
            int i = 0;
            System.Numerics.Complex z=0;
            for (i = 0; i < A.Length; i++) z = (z + A[i]);
            z = z / A.Length;
            for (i = 0; i < A.Length; i++) A[i] = A[i] - z;
            return A;
        }

        int [] tst_signal (int a,uint n)
        {
            int[] array = new int[n];
            int i;
            double z;
            double w1 = 0.1;
            double w2 = 0.2;

            for (i=0;i<n;i++)
            {
                if (a==0) z = 30000 * (Math.Cos(2 * Math.PI * w1 * i)) + 10 * (Math.Cos(2 * Math.PI * w2 * i));
                else      z = 30000 * (Math.Sin(2 * Math.PI * w1 * i)) + 10 * (Math.Sin(2 * Math.PI * w2 * i));
                array[i] = (int)z;
            }
            return array;
        }

        private int [] SPUR_REMOVE (double [] a,int k) //a-входной вектор, k - количество спур для удаления
        {
            int j=0;
            int l = 0;
            int index=0;
            double A_max=0;
            int[] idx = new int[k];
       //     Debug.WriteLine("----------");
            for (j=0;j<k;j++)
            {
                A_max=a.Max();                      //ищем максимум в массиве
                index = Array.IndexOf(a, A_max);    //определяем индекс найденого максимума
                a[index] = 0;                       //обнуляем максимум
                idx[l]= index;                      //запоминаем индекс
                l++;
                //         Debug.WriteLine("A_max:"+ A_max);
                //         Debug.WriteLine("index:" + index);
            }
            return idx;
        }

        int km = 0;
        double A_out = 0;
        double B_out = 0;//амплитуда второй гармоники (обычно условная помеха)
        double B_xout = 0;/*координата второй гармоники*/
        double M_out = 0; //амплитуда главной гармоники

        string FLAG_DISPAY = "";
        double  AMAX, BMAX, CMAX, M1X, M1Y, M2X, M2Y, M3X, M3Y;
        double [] TSAMPL  = new double[4096];
        double [] MAG_LOG = new double[4096];

        double H_a = 0;//максимальный уровень сигнала в спектре
        double H_b = 0;//вторая максимальная гармоника в спектре
        double H_delta = 0;

        double[] H_q = { -0.220477, -1.76878, -0.602164, -0.913822, -0.886835, -1.7772, 1.36022, -3.63074, -0.2505, 2.80075, -8.44291, 6.48476, -12.8247, 8.38382, -6.26534, -13.1824, 22.5553, -41.5086, 38.8532, -28.0017, -8.75528, 41.6745, -81.1482, 85.8237, -85.7525, 26.9482, 68.9365, -144.272, 144.511, -54.7266, 2.45574, 6.45885, -66.79, 81.7377, -42.6371, 17.8659, -0.196799, 28.4292, -85.838, 180.982, -165.842, 148.284, 141.946, -357.965, 644.414, -735.075, 570.723, -282.066, -589.997, 1513.71, -1982.31, 1906.68, -1144.98, -451.565, 2478.35, -4305.22, 4758.25, -3720.61, 741.302, 4073.04, -9872.3, 14931.1, -14753.3, -3525.99, 89801.4, -3505.4, -14581.2, 14669.7, -9641.46, 3953.61, 715.074, -3566.38, 4531.05, -4071.96, 2327.42, -421.002, -1059.04, 1748.65, -1801.89, 1362.35, -525.45, -248.261, 495.093, -627.854, 539.694, -293.048, 112.423, 112.575, -118.77, 115.748, -43.9126, 3.39483, -4.10936, 37.7368, -60.0501, 100.96, -76.1108, 7.10599, 2.62531, -55.8521, 144.318, -141.434, 66.439, 25.556, -80.4433, 79.3497, -74.2671, 37.5466, -7.92261, -24.8173, 33.7291, -35.8595, 19.0041, -11.2003, -5.3408, 6.60269, -10.3894, 4.80144, -6.59629, 1.74394, -0.489924, -2.62478, 0.195514, -1.19107, -1.14408, -0.808126, -0.474232, -1.83026 };
        double[] H_i = { -1, -1.31279, -0.308793, -1.28754, -1.18601, -0.533113, -0.735555, -3.90869, 4.33925, -7.06787, 1.31238, -2.09543, -6.03124, 9.89133, -21.2423, 25.3237, -14.0766, 2.30803, 21.7577, -45.2378, 63.857, -59.2126, 22.911, 9.38634, -78.8197, 123.07, -133.782, 56.52, 54.1236, -98.6309, 76.1462, -95.0234, 72.3418, -8.28296, -20.2758, 17.6597, 7.50105, -41.7184, 54.3167, 10.0364, -141.202, 331.982, -440.975, 368.96, -73.6581, -208.469, 735.649, -1179.37, 1336.58, -977.851, -31.3203, 1454.83, -2582.02, 3058.78, -2662.96, 1019.47, 1637.94, -4609.01, 7112.62, -7702.69, 5080.92, 2276.9, -16241.1, 36733.2, -1, -36520.6, 16049.6, -2239.01, -4964.08, 7474.9, -6863.27, 4415.95, -1561.7, -966.166, 2498.92, -2853.22, 2386.12, -1336.23, 26.5592, 878.238, -1192, 1035.65, -640.201, 176.166, 59.8692, -303.68, 348.105, -254.333, 99.1654, -8.08022, -28.9802, 5.08071, 31.9094, -39.3134, 26.3371, 7.97503, -84.7325, 101.181, -81.9315, 98.6762, -56.0505, -57.3808, 127.04, -118.889, 71.9448, -10.6119, -22.8581, 51.5818, -58.8934, 38.0207, -20.8316, -3.84671, 10.1052, -23.0406, 15.688, -9.82407, 2.99507, -0.150929, -2.73867, 3.38069, -4.63365, 0.796443, -1.13395, -1.11478, -1.23683, -0.359795, -1.91348, -0.662199 };

        double[][] MeM = new double[100][];//массив памяти для сглаживания вывода спектроанализатора
        
        void fft_out ()
        {
            int i;
            for (i=0;i<100;i++)
            {
               MeM[i] = new double[BUF_N];//формируем зубчатый массив памяти
            }

          while ((true) && (FLAG_THREAD == "start"))
            {
                if (flag_NEW_FFT == 1)
                {
        
                    UInt32 zeros = Convert.ToUInt32(0);
                    uint N_temp;
                    int N_complex;
                    uint z;
                    int step = 0;
                    int pstep = 0;
                    int N_correct = 128;

                    N_temp = Convert.ToUInt32(BUF_N);
                    N_complex = Convert.ToInt32(BUF_N);

                    double A_max;
                    double B_max;
                    double C_max;

                    double[] m_sort      = new double[BUF_N];
                    double[] fft_array   = new double[BUF_N];
                    double[] fft_array_x = new double[BUF_N];
                    double[] fft_array_y = new double[BUF_N];                   

                    double[] t = new double[BUF_N];
                    double[] A = new double[BUF_N];

               //     string selectedWindowName = cmbWindow.SelectedValue.ToString();

                    DSPLib.DSP.Window.Type windowToApply = (DSPLib.DSP.Window.Type)Enum.Parse(typeof(DSPLib.DSP.Window.Type), selectedWindowName);

                    int post_U_i = 0;
                    int post_U_q = 0;

                    for (i = 0; i < N_temp; i++)
                    {
                        if (packet_data_i[i] > 32767)
                        {
                            z = (uint)(packet_data_i[i]);
                            z = (~z) & 0xffff;
                            packet_data_i[i] = -1 * Convert.ToInt32(z + 1);
                        }
                        else packet_data_i[i] = Convert.ToInt32(packet_data_i[i]);

                        if (packet_data_q[i] > 32767)
                        {
                            z = (uint)(packet_data_q[i]);
                            z = (~z) & 0xffff;
                            packet_data_q[i] = -1 * Convert.ToInt32(z + 1);
                        }
                        else packet_data_q[i] = Convert.ToInt32(packet_data_q[i]);

                        post_U_i = (post_U_i + packet_data_i[i]) /2;
                        post_U_q = (post_U_q + packet_data_q[i]) /2;
                    }

                    //            Debug.WriteLine("post_U_i:" + post_U_i);
                    //            Debug.WriteLine("post_U_q:" + post_U_q);

                    tst_data_i = tst_signal(0, N_temp);
                    tst_data_q = tst_signal(1, N_temp);


                    for (i = 0; i < fft_array_x.Length; i++)
                        {
                            if (FLAG_CALIBROVKA == 1)
                            {
                                fft_array_x[i] = Convert.ToDouble(packet_data_i[i] - post_U_i);
                                fft_array_y[i] = Convert.ToDouble(packet_data_q[i] - post_U_q);
                            }
                            else
                                {
                                    fft_array_x[i] = (double)packet_data_i[i];//переводим данные из целочисленного в вид с плавующей точкой
                                    fft_array_y[i] = (double)packet_data_q[i];

                           //       fft_array_x[i] = (double)tst_data_i[i];//переводим данные из целочисленного в вид с плавующей точкой
                           //       fft_array_y[i] = (double)tst_data_q[i];

                                    tst_data_i[i] = packet_data_i[i];
                                    tst_data_q[i] = packet_data_q[i];
                            }
                        }

                    //------------------Считаем амплитуду входного сигнала-------------------------
                    System.Numerics.Complex[] cpxResult = new System.Numerics.Complex[N_complex];
                    
                    //---переводим входные вектора в комплексный вид
                    for (i = 0; i < N_complex; i++)
                    {
                        cpxResult[i] = new System.Numerics.Complex(fft_array_x[i], fft_array_y[i]);
                    }
                    
                    int k = Convert.ToInt16(LogBase(N_temp, 2));//порядок БПФ

                    FFTLibrary.Complex fft_z = new FFTLibrary.Complex();
                    FFTLibrary.Complex fft_h = new FFTLibrary.Complex();//fft для импульсной характеристики фильтра                  

                    if (btn_corr_spectr.Text == "ON") //включена коррекция АЧХ
                {                   
               
                    double[] fh_i = new double[N_temp * 2];
                    double[] fh_q = new double[N_temp * 2];

                    Array.Copy(H_i, fh_i, N_correct);//H_i  - ИХ корректирующего фильтра (действительная часть)
                    Array.Copy(H_q, fh_q, N_correct);//H_i  - ИХ корректирующего фильтра (мнимая часть)

                    double[] fx_i = new double[N_temp * 2];
                    double[] fx_q = new double[N_temp * 2];

                    Array.Copy(fft_array_x, fx_i, N_temp);
                    Array.Copy(fft_array_y, fx_q, N_temp);

                    //БПФ должно быть размером больше чем длинна ИХ фильтра + длинна вектора данных - 1 !!!
                    fft_h.FFT_filtr(1, k + 1, fh_i, fh_q); //расчитываем БПФ от импульсной характеристики фильтра
                    fft_h.FFT_filtr(1, k + 1, fx_i, fx_q); //расчитываем БПФ от входных данных

                    System.Numerics.Complex[] HxResult = new System.Numerics.Complex[N_complex * 2];//комплексный массив для комплексной ИХ
                    System.Numerics.Complex[] XxResult = new System.Numerics.Complex[N_complex * 2];//комплексный массив для обработанных БПФ данных
                    System.Numerics.Complex[] ZxResult = new System.Numerics.Complex[N_complex * 2];//комплексный массив для обработанных БПФ данных

                    for (i = 0; i < (N_complex * 2); i++)
                    {
                        HxResult[i] = new System.Numerics.Complex(fh_i[i], fh_q[i]);//конвертируем в комплексный вектор
                        XxResult[i] = new System.Numerics.Complex(fx_i[i], fx_q[i]);
                        ZxResult[i] = Complex.Multiply(HxResult[i], XxResult[i]);   //перемножаем комплексные числа обработанной ИХ и входных данных
                            
                      if (N_temp == 1024) ZxResult[i] = Complex.Divide(ZxResult[i], 1.34);//
                      if (N_temp == 512)  ZxResult[i] = Complex.Divide(ZxResult[i], 1.98);//
                      if (N_temp == 256)  ZxResult[i] = Complex.Divide(ZxResult[i], 2.92);//
                      if (N_temp == 128)  ZxResult[i] = Complex.Divide(ZxResult[i], 2.72);//
                    }

                    //---------Для коэфф шума-----------
                    int[] idx = new int[SPUR0_REMOVE_N];//тут храним список удаляемых спурр

                    if (N_sch_timer1>0) //проверяем что идёт интервал измерения шума
                        {
                            // Convert the complex result to a scalar magnitude 
                            double[] magAn = DSP.ConvertComplex.ToMagnitude(ZxResult);//высчитываем массив модулей (длинну векторов) комплексных отсчётов входного массива
                            double[] mag_Corrn = DSP.Math.Divide(magAn, 9.33);           //приводим показометр к такому же значению, как на спектре                    
               
                            idx=SPUR_REMOVE(mag_Corrn, SPUR0_REMOVE_N);   // ищем список индексов спур
                        }

                    //----------------------------------

                    for (i = 0; i < (N_complex * 2); i++)
                    {
                        fx_i[i] = ZxResult[i].Real;
                        fx_q[i] = ZxResult[i].Imaginary;

                        //действует только на интервале измерения шума!!!
                        if (N_sch_timer1 > 0)//для измерения шума
                            {
                                if (idx.Contains(i)) //обнуляем индексы спур
                                {
                                    fx_i[i] = 0;
                                    fx_q[i] = 0;
                                }
                            }
                    }

                    fft_h.FFT(-1, k + 1, fx_i, fx_q);//высчитываем обратное  БПФ 
                                           
                        Array.Copy(fx_i, fft_array_x, N_temp);
                        Array.Copy(fx_q, fft_array_y, N_temp);

                        fft_array_x = DSP.Math.Divide(fft_array_x, 12 -  k + 1);//компенсируем усиление возникшее при обработке
                        fft_array_y = DSP.Math.Divide(fft_array_y, 12 -  k + 1);
                    }

                    // Apply window to the time series data
                    double[] wc = DSP.Window.Coefficients(windowToApply, N_temp);
                    double windowScaleFactor = DSP.Window.ScaleFactor.Signal(wc);

                    double[] dat_I_scale = DSP.Math.Divide(fft_array_x, 1.11);//приводим к еденице
                    double[] dat_Q_scale = DSP.Math.Divide(fft_array_y, 1.11);

                    double[] windowedTimeSeries_i = DSP.Math.Multiply(dat_I_scale, wc);
                    double[] windowedTimeSeries_q = DSP.Math.Multiply(dat_Q_scale, wc);

                    windowedTimeSeries_i = DSP.Math.Multiply(windowedTimeSeries_i, windowScaleFactor);  //учитываем разный коэффициент передачи окна
                    windowedTimeSeries_q = DSP.Math.Multiply(windowedTimeSeries_q, windowScaleFactor);                   
                    
                    //------------------Расчитываем БПФ----------------------------------------      
                   
                    fft_z.FFT(1, k, windowedTimeSeries_i, windowedTimeSeries_q);  //внутри FFT происходит масштабирование расчитанных данных!                  
                    
                    for (i = 0; i < N_complex; i++)
                    {
                        cpxResult[i] = new System.Numerics.Complex(windowedTimeSeries_q[i], windowedTimeSeries_i[i]);
                    }

                    //-----------------Это мы расчитываем уровень сигнала на входе АЦП после коррекции искажений--------------------
                    System.Numerics.Complex[] PinResult  = new System.Numerics.Complex[N_complex];
                    System.Numerics.Complex[] CorrResult = new System.Numerics.Complex[N_complex];

                    for (i = 0; i < N_complex; i++)
                    {
                        PinResult[i] = new System.Numerics.Complex(dat_I_scale[i], dat_Q_scale[i]);
                    }

                    Array.Copy(PinResult, CorrResult, PinResult.Length);

                    // Convert the complex result to a scalar magnitude 
                    double[] magA = DSP.ConvertComplex.ToMagnitude(CorrResult);//высчитываем массив модулей (длинну векторов) комплексных отсчётов входного массива
                    double[] mag_Corr = DSP.Math.Divide(magA, 9.33);           //приводим показометр к такому же значению, как на спектре 
                    A_out = FILTR_MEDIANA(mag_Corr);

                    //--------------------------------------------------------------------------------------------------------------
                    //Accord.Math.FourierTransform.FFT(cpxResult, Accord.Math.FourierTransform.Direction.Forward); //этот метод статический
                    //mag = DSP.Math.Multiply(mag, 1);

                    double[] magLog_temp  = DSP.ConvertComplex.ToMagnitudeSquared(cpxResult);//получаем квадрат напряжения
                    double[] magLog_temp2 = DSP.Math.Divide  (magLog_temp,50);
                    double[] magLog       = DSP.Math.Log10   (magLog_temp2);
                             magLog       = DSP.Math.Multiply(magLog,10);
                             magLog       = DSP.Math.Subtract(magLog,79.7);
                    int j;
                         
                    for (j = 0; j < BUF_N; j++)
                    {
                        //	 A[j] = magLog[j];
                        if (j <  (BUF_N / 2))      A[j] = magLog[j + (BUF_N / 2)];
                        if (j > ((BUF_N / 2) - 1)) A[j] = magLog[j - (BUF_N / 2)];
                        t[j] = -3121 + (6250 * j / (BUF_N));//важно сначала умножить а потом поделить!!!! атоноль
                             // Debug.WriteLine("t[]:"+t[j]);
                    }

                    for (j = 0; j <BUF_N; j++)
                    {
                        magLog[j] = A[BUF_N - 1 - j];
                    //  if (magLog[j] < 0) magLog[j] = 0;
                    }

                    if (FLAG_CORRECT<1) FUNC_MAG_CORRECT(ref magLog, Koeff_measure0); 
                    else
                    if (FLAG_CORRECT < 3) FUNC_MAG_CORRECT(ref magLog, Koeff_measure1);
                    else
                    if (FLAG_CORRECT == 3) FUNC_MAG_CORRECT(ref magLog, Koeff_measure3);

                    if (FLAG_filtr == 1) filtr_usr2(magLog,MeM, N_filtr_usr0);
                    if (FLAG_filtr == 2) filtr_usr2(magLog,MeM, N_filtr_usr1);
                    
                    int k_max = 0;
                    double m1x, m1y;
                    double m2x, m2y;
                    double m3x, m3y;

                    Array.Copy(magLog, m_sort, BUF_N);                    

                    (k_max, A_max) = MAX_f(magLog, BUF_N);       //определяем Х координату первого максимума
                    m1x = t[k_max];
                    m1y = A_max;

                    if (BUF_N == 4096) step = 50; else
                    if (BUF_N == 2048) step = 40; else
                    if (BUF_N == 1024) step = 30; else
                    if (BUF_N ==  512) step = 25; else
                    if (BUF_N ==  256) step = 15; else
                    if (BUF_N ==  128) step = 10;
                    else               step = 60;

                    step  = (int)(step * B_win);
                    pstep = step / 2;

                    if (k_max > pstep && k_max < (BUF_N - step)) 
                        for (i = 0; i < step; i++) m_sort[k_max + i - pstep] = -90;//очищаем буфер поиска от уже ненужных величин

                    (k_max, B_max) = MAX_f(m_sort, BUF_N);      //определяем Х координату второго максимума
                    m2x = t[k_max];
                    m2y = B_max;                                //определяем второй максимум

                    if (k_max > pstep && k_max < (BUF_N - step))  for (i = 0; i < step; i++) m_sort[k_max + i - pstep] = -90; 
                    else //очищаем буфер поиска от уже ненужных величин
                        if (k_max > pstep)
                    {
                        step = (BUF_N - k_max) * 2;
                        for (i = 0; i < step; i++) m_sort[k_max + i - step/2] = -90;
                    }

                        (k_max, C_max) = MAX_f(m_sort, BUF_N);      //определяем Х координату третьего максимума

                    m3y = C_max;
                    m3x = t[k_max]; ;

                    H_a = m1y;
                    H_b = m2y;
                    H_delta = m1y - m2y;
                    //------------------------------------------------------------------------
                    
                    Array.Copy(magLog, MAG_LOG, BUF_N);
                    Array.Copy(t, TSAMPL, BUF_N);
                       

                    AMAX = A_max;
                    BMAX = B_max;
                    CMAX = C_max;
                    M1X = m1x;
                    M1Y = m1y; M_out = M1Y;
                    M2X = m2x; B_xout = M2X;
                    M2Y = m2y; B_out = M2Y;
                    M3X = m3x;
                    M3Y = m3y;
                    FLAG_DISPAY = "1";
                    flag_NEW_FFT = 0;
                }
                Thread.Sleep(0);
            }
        }

    void FUNC_MAG_CORRECT (ref double  [] m,double korr)
        {
      
           for (int i=0; i<m.Length;i++)
            {
                 if (m[i] < -40) m[i] = m[i] - korr; 
              //  m[i] = -10;
            }
        }

      double FILTR_MEDIANA (double [] M) //M - массив входных отсчётов
      {
        Array.Sort(M);
        int k = (M.Length) / 2; //средний элемент массива
        return M[k];    //возвращаем медианное значение
      }
        //фильтр - среднее
        double FILTR_MAT(double[] M) //M - массив входных отсчётов
        {
            double z = 0;
            for (var i = 0; i < M.Length; i++) z = z + M[i];
            z = z / M.Length;
            return z;    //возвращаем среднее значение
        }

        double MEM_ZNACH = 0;
        void DISPLAY () //выводит измерения на панель
        {
            double DATA_out = 0;
            int Nbuf = BUF_N;// размер БПФ
            double[] TSAMPL_tmp = new double[Nbuf];
            double[] MAG_LOG_tmp = new double[Nbuf];

            if (FLAG_DISPAY=="1")
            {
                double a = Math.Round(M1Y - M2Y,1);
                double b = Math.Round(M1Y - M3Y,1);
                double c = Math.Round(LEVEL_TEST_SIGNAL - M1Y); // отношение к уровню входного сигнала                

                if (MEM_ZNACH > a) DATA_out = MEM_ZNACH;
                else               DATA_out = a;

                MEM_ZNACH = a;
                
                //Start a Stopwatch
                //Stopwatch stopwatch = new Stopwatch();
                //stopwatch.Start();

                if (radioButton1.Checked)
                {
                    if (FLAG_SYNC_GEN_SIGN_POMEH==false) textBox_din_diapazone.Text = a.ToString();
                    else                                 textBox_din_diapazone.Text = b.ToString();
                } else
                {
                    textBox_din_diapazone.Text = c.ToString();
                }

                Array.Copy(TSAMPL, TSAMPL_tmp, Nbuf); //благодаря этому у нас нормальный вывод спектра без артефактов при переключении размерности  БПФ
                Array.Copy(MAG_LOG, MAG_LOG_tmp, Nbuf);

                fig3.PlotData(TSAMPL_tmp, MAG_LOG_tmp, AMAX, BMAX, CMAX, M1X, M1Y, M2X, M2Y, M3X, M3Y);
                //fig3.Show();
                FLAG_DISPAY = "";
            }            
        }

        void filtr_usr2(double[] data, double[][] a, int k)//входные данные, входной зубчатый массив памяти (в нулевом массиве текущий массив данных )и глубина усреднения
        {
            int i;
            int j;
            double[] o = new double[data.Length];

            for (j = 0; j < data.Length; j++)
            {
                for (i = 0; i < k; i++) if (i < (k - 1)) a[k - i - 1][j] = a[k - i - 2][j]; else a[k - i - 1][j] = data[j];

                for (i = 0; i < k; i++) o[j] = (o[j] + a[i][j]);
            }
            for (j = 0; j < data.Length; j++) o[j] = o[j] / k;
             
            Array.Copy(o, data, data.Length);
        }

        void filtr_usr_rst(double[] data, double[][] a, int k,bool rst)//входные данные, входной зубчатый массив памяти (в нулевом массиве текущий массив данных )и глубина усреднения
        {
            int i;
            int j;
            double[] o = new double[data.Length];

            if (rst == false)
            {
                for (j = 0; j < data.Length; j++)
                {
                    for (i = 0; i < k; i++) if (i < (k - 1)) a[k - i - 1][j] = a[k - i - 2][j]; else a[k - i - 1][j] = data[j];

                    for (i = 0; i < k; i++) o[j] = (o[j] + a[i][j]);
                }
                for (j = 0; j < data.Length; j++) o[j] = o[j] / k;

                Array.Copy(o, data, data.Length);
            }
            else
            {
                for (j = 0; j < data.Length; j++)
                {
                    for (i = 0; i < k; i++) a[i][j] = 0;
                }
            }
        }

        void filtr_max_rst(double[] data, double[][] a, int k, bool rst)//входные данные, входной зубчатый массив памяти (в нулевом массиве текущий массив данных )и глубина усреднения
        {
            int i;
            int j;
            double[] o = new double[data.Length];

            if (rst == false)
            {
                for (j = 0; j < data.Length; j++)
                {
                    for (i = 0; i < k; i++) if (i < (k - 1)) a[k - i - 1][j] = a[k - i - 2][j]; else a[k - i - 1][j] = data[j];

                    for (i = 0; i < k; i++) if (a[i][j]> o[j]) o[j] = a[i][j];
                }
         
                Array.Copy(o, data, data.Length);
            }
            else
            {
                for (j = 0; j < data.Length; j++)
                {
                    for (i = 0; i < k; i++) a[i][j] = 0;
                }
            }
        }
        void SerialPort1DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
		{
		
		}

        (int n ,double Amax ) MAX_f (double [] m,int N)
        {
            int i = 0;
            double max = -90;
            int k = 0;

            for (i=0;i<N;i++)
            {
                if (max<m[i])
                {
                    max = m[i];
                    k = i;
                }
            }
            return (k,max);
        }

		void Button1Click(object sender, EventArgs e)
		{
            if (Btn_start.Text== "ВЫКЛ")
            {
          //      Stop();
                Btn_start.Text = "ВКЛ";
                timer1.Enabled = false;
            }
            else
            {
                Start();
                Btn_start.Text = "ВЫКЛ";
                Btn_start.Enabled = false;
                timer1.Enabled = true;
            }
        }
		void Port_enClick(object sender, EventArgs e)
		{       
			
		}
		void Save_bottonClick(object sender, EventArgs e)
		{ 
   			saveFileDialog1.Filter = "text|*.txt|data|*.dat|fir coef|*.coff";  
   			saveFileDialog1.Title = "Save an File";  
   			
		 if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            return;
        	// получаем выбранный файл
        	string filename = saveFileDialog1.FileName;
        	// сохраняем текст в файл
        	System.IO.File.WriteAllText(filename, Console1.Text);
        	//MessageBox.Show("Файл сохранен");
		    
		}

		void array_save (int [] a,int N)
		{
			string text="";
		
			System.IO.StreamWriter textFile = new System.IO.StreamWriter(@"c:\temp\c_sharp_test.txt");
			Debug.WriteLine("сохраняем сформированый массив в файл");
			
			for (int i = 0; i < N; i++)
		      {
				text=text+Convert.ToString(a[i])+"\r\n";
		      }
			textFile.WriteLine(text);	          
			textFile.Close();
		}
		void N_fftTextChanged(object sender, EventArgs e)
		{
            int n = 0;            
            n= Convert.ToInt16(text_N_fft.Text);
            if ((n == 64) || (n == 128) || (n == 256) || (n == 512) || (n == 1024) || (n == 2048) || (n == 4096) || (n == 8192))
            {
                BUF_N = Convert.ToInt16(text_N_fft.Text);                
            }  
            //Debug.WriteLine("изменили BUF_N:"+BUF_N);
		}
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			Fsample=Convert.ToInt16(text_fsemple);
			//Debug.WriteLine("изменили BUF_N:"+BUF_N);
		}

        private void cmbWindow_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedWindowName = cmbWindow.SelectedValue.ToString();
            if (selectedWindowName == "Nutall3")    B_win = 2.02;
            if (selectedWindowName == "Hann")       B_win = 1.5;
            if (selectedWindowName == "None")       B_win = 1.0;
            if (selectedWindowName == "Hamming")    B_win = 1.37;
            if (selectedWindowName == "BH92")       B_win = 1.46;
            if (selectedWindowName == "Nutall4")    B_win = 2.02;
            if (selectedWindowName == "Nutall3A")   B_win = 2.02;
            if (selectedWindowName == "Nutall3B")   B_win = 2.02;
            if (selectedWindowName == "Nutall4A")   B_win = 2.02;
        }

        private void Open_file_BTN_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                text_from_file = File.ReadAllText(fileName);   
            }
        }

        int[] data_0_i = new int[BUF_N];
        int[] data_0_q = new int[BUF_N];

        int BUF_convert(byte [] m, int col)
        {
            int i = 0;
            int k = 0;
            int l = 0;
            int z = 0;

            z= (Convert.ToInt32(m[3])<<24)+ (Convert.ToInt32(m[2]) << 16)+ (Convert.ToInt32(m[1]) << 8)+ (Convert.ToInt32(m[0]) << 0);

            Array.Clear(data_0_i, 0, BUF_N);
            Array.Clear(data_0_q, 0, BUF_N);

       //     Debug.WriteLine("M[0]={0:X}",z);

            for (i = 4; i < col; i++)//
            {                      
                    if (k == 0) data_0_i[l] = Convert.ToInt32(m[i]);
                    if (k == 1) data_0_i[l] = data_0_i[l] + (Convert.ToInt32(m[i])<<8);
         
                    if (k == 2) data_0_q[l] = Convert.ToInt32(m[i]);
                    if (k == 3) data_0_q[l] = data_0_q[l] + (Convert.ToInt32(m[i])<<8);

                if (k != 3) k = k + 1;
                else
                {
         //           Debug.WriteLine("data_0_q={0:X}", data_0_q[l]);
         //           Debug.WriteLine("data_0_i={0:X}", data_0_i[l]);
                    k = 0;
                  l = l + 1;
                }
            }
            return 1;
        }
        //-----------------------------------
        private void Timer2_Tick(object sender, EventArgs e)
        {
            int size_rcv;
            String buf_strng = "";

            Encoding utf8 = Encoding.GetEncoding("UTF-8");
            Encoding win1251 = Encoding.GetEncoding("Windows-1251");

            byte[] utf8Bytes;
            byte[] win1251Bytes;

            if ((Flag_comport_rcv == 1) && (serialPort1.IsOpen))
            {
                Flag_comport_rcv = 0;
                size_rcv = serialPort1.BytesToRead;
                serialPort1.Read(RCV, 0, size_rcv);
                // buf_strng = serialPort1.ReadExisting();

                buf_strng = Encoding.UTF8.GetString(RCV, 0, size_rcv);
                richTextBox1.Text = buf_strng;

                btn_com_open.Text = "send";
                btn_com_open.ForeColor = Color.Black;
                serialPort1.Close();
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (FLAG_filtr == 1) FLAG_filtr = 0; else FLAG_filtr = 1;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (FLAG_filtr == 2) FLAG_filtr = 0; else FLAG_filtr = 2;
        }

        private void btn_telnet_gen_Click(object sender, EventArgs e)
        {
     
            GEN_SIGN.FREQ(Convert.ToInt32(textBox_freq_gen.Text ));
            GEN_SIGN.POW (textBox_level_gen.Text);
            if (checkBox3.Checked)
            {
                GEN_SIGN.OUT(1);
                GEN_SIGN.SEND();
            }
            GEN_SIGN.SEND();

            COMMAND_FOR_SERVER = Convert.ToString(Convert.ToDouble(textBox_freq_gen.Text) - Convert.ToDouble(textBox_freq_m54.Text));
        }

        private void freq_send(int freq)
        {
            string command1 = "__~0 freq:";//добавил 2-пробела перед командой         

                try
                {
                    if (serialPort1.IsOpen == false) serialPort1.PortName = textBox_com_port.Text;
                    command1 = command1 + Convert.ToString(freq+ CORRECT_FREQ) + ";__";//2-пробела и после команды , чтобы срабатывало.
                    textBox_freq_m54.Text = Convert.ToString(freq);
                if (serialPort1.IsOpen == false)
                    {
                        serialPort1.Open();
                    }
                    serialPort1.Write(command1);
                     // здесь может быть код еще...
                }
                catch (Exception ex)
                {
                    btn_com_open.Text = "send";
                    btn_com_open.ForeColor = Color.Black;
                    // что-то пошло не так и упало исключение... Выведем сообщение исключения
                    //Console.WriteLine(string.Format("Port:'{0}' Error:'{1}'", serialPort1.PortName, ex.Message));
                }

                try
                {
                  serialPort1.Close();  
                } catch (Exception ex)
                {}
                
        }

        bool FLAG_IH_load = false;

        private void btn_com_open_Click(object sender, EventArgs e)
        {
            string command1 = "  ~0 freq:";
            string command2 = "  ~0 upr_at";
            int freq = 0;

            freq = Convert.ToInt32(textBox_freq_m54.Text) + CORRECT_FREQ;

            checkBox1.Checked = false;
            if (serialPort1.IsOpen == false) serialPort1.PortName = textBox_com_port.Text;
                        
                try
                {
                    command1 = command1 + freq.ToString() + ";  ";
                    //   command2 = command2 + channal_box.Text + ":" + textBox_att_m54.Text + ";";
                    if (serialPort1.IsOpen == false)
                    {
                        serialPort1.Open();
                    }
                Debug.WriteLine("шлём:" + command1);
                //       btn_com_open.Text = "trnsf";
                btn_com_open.ForeColor = Color.Green;
                    serialPort1.Write(command1);
                //  serialPort1.Write(command2);
                 // здесь может быть код еще...
                }
                catch (Exception ex)
                {
                    // что-то пошло не так и упало исключение... Выведем сообщение исключения
                    Console.WriteLine(string.Format("Port:'{0}' Error:'{1}'", serialPort1.PortName, ex.Message));
                }
         
         //   serialPort1.Close();

            if (FLAG_IH_load == true) Kih_load();
            else MessageBox.Show("Загрузите корректирующую АЧХ характеристику");
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            Flag_comport_rcv = 1;
            var serialDevice = sender as SerialPort;
            var buffer = new byte[serialDevice.BytesToRead];
            serialDevice.Read(buffer, 0, buffer.Length);
            string z = Encoding.GetEncoding(1251).GetString(buffer);//чтобы видеть русский шрифт!!!
            Debug.WriteLine(":" + z);
            if (FLAG_COMPORT_ERROR==1)
            {
                FLAG_COMPORT_ERROR = 0;
                bool FLAG = z.Contains("help");
                if (FLAG)
                {
                    Debug.WriteLine("Найден ответ!");
                    button_com_port.BackColor = Color.Green;
                }
                else
                {
                    Debug.WriteLine("нет ответа!");
                    button_com_port.BackColor = Color.Gray;
                }
            }
            serialPort1.Close();
        }


        int freq_start   = 0;
        int freq_stop    = 0;
        int freq_step    = 0;
        int freq_current = 0;
        int freq_setup = 0;//переменна типа перебора - калибровка или просто просмотр частот (перебор)
        int freq_last = 0;
        double progress_freq = 0;
        double progress_sum = 0;
        int freq_delta = 0;
        int flag_progress = 0;

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text=="SCAN")
            {
                _FREQ_DELTA = Convert.ToInt32(textBox2.Text) - Convert.ToInt32(textBox_freq_gen.Text);
                Console.WriteLine("_FREQ_DELTA:"+_FREQ_DELTA);
                
                button3.Text = "STOP";
                Din_DIAP_min = 100;
                Podav_DIAP_min = 100;
                FLAG_filtr = 0;//2
                if (_isServerStarted == true) timer3.Enabled = true;
                timer3.Interval = Convert.ToInt32(textBox_freq_delay.Text);
                freq_start = Convert.ToInt32(textBox_freq_start.Text);
                freq_stop = Convert.ToInt32(textBox_freq_stop.Text);
                freq_step = Convert.ToInt32(textBox_freq_step.Text);
                freq_last = 0;
                freq_current = freq_start;
                freq_delta = freq_stop - freq_start;
                progress_freq = freq_step;
                if (freq_delta > 0) progress_freq = progress_freq * 100 / freq_delta;
                //      Debug.WriteLine("progress_freq: " + progress_freq);
                progressBar1.Value = 0;
                flag_progress = 1;
                Console1.Text = "";
                freq_setup = 0;//перебор
                VAR_IH_data_obzor.lenght = -2;//ато вываливаются ошибки - недостаточно данных
                checkBox1.Checked = false;
                progressBar1.Value = 0;
                
            }  
            else
            {
                FLAG_TEST5 = false;
                FLAG_TEST3 = false;
                button3.Text = "SCAN";
                timer3.Enabled = false;
                flag_progress = 0;
                FLAG_filtr = 1;
                progressBar1.Visible = false;
            }
            
        }

    
        struct Struct_buff        //тут лежат калибровачные характеристики !!!
        {
            public string z;
            public int lenght;
        }

        Struct_buff VAR_IH_data;
        Struct_buff VAR_IH_data_obzor;

        bool FLAG_TIMER3 = true;
        private void timer3_Tick(object sender, EventArgs e)
        {
            
            double a, b, c;
            string text = "";
            //create a new telnet connection to hostname "x.x.x.x" on port "5025 or 5024"
            string host = textBox_ip_generator.Text;
            int port = Convert.ToInt32(textBox_port_generator.Text);
            progressBar1.Visible = true;
            int freq_temp = Convert.ToInt32(textBox_freq_m54.Text);

            try
            {
               if (freq_last>0)
            {
                //тут происходят измерения! и сразу запись в стринг
            text = Convert.ToString(freq_last) + ";" + Convert.ToString(Math.Round(H_a+90, 2)) + ";" + Convert.ToString(Math.Round(H_b, 2)) + ";" + Convert.ToString(Math.Round(H_delta, 2));
            Console1.Text = Console1.Text + "\r\n" + text;

                    if (FLAG_TIMER3)
                    {
                        Koeff_usileniya_median = Convert.ToDouble(textBox_Ku.Text);
                        FLAG_TIMER3 = false;
                    }
                    else
                    {
                        Koeff_usileniya_median = (Koeff_usileniya_median + Convert.ToDouble(textBox_Ku.Text)) / 2;
                        Koeff_usileniya_median = Math.Round(Koeff_usileniya_median, 2);
                        textBox5.Text = Koeff_usileniya_median.ToString();
                        textBox6.Text = (Convert.ToDouble(textBox5.Text) + Convert.ToDouble(textBox_att_m54.Text)).ToString();
                    }           

                if (radioButton1.Checked)
                    {
                        if (Convert.ToDouble(textBox_din_diapazone.Text) < Din_DIAP_min) Din_DIAP_min = Convert.ToDouble(textBox_din_diapazone.Text);
                        textBox_min_dindiapaz.Text = Din_DIAP_min.ToString();
                    }
               else
                    {
                        if (Convert.ToDouble(textBox_din_diapazone.Text) < Podav_DIAP_min) Podav_DIAP_min = Convert.ToDouble(textBox_din_diapazone.Text);
                        textBox_min_dindiapaz.Text = Podav_DIAP_min.ToString();
                    }
            }

            if ((freq_setup == 1) || (freq_setup == 2))
            {
                VAR_IH_data.lenght++;
                freq_temp = freq_current;
                if ((FLAG_TEST3==false)&&(FLAG_TEST5==false)) freq_send(freq_temp);//отсылаем текущую частоту в поделку для перестройки поделки её в центр диапазона, если это не ТЕСТ3,4
            }
     //       else VAR_IH_data_obzor.lenght++;

            if (freq_setup == 0)
            {
                VAR_IH_data_obzor.lenght++;
                freq_temp = freq_current;
                if ((FLAG_TEST3==false)&&(FLAG_TEST5==false)) freq_send(freq_temp);//отсылаем текущую частоту в поделку для перестройки поделки её в центр диапазона
            }            

            if (FLAG_TEST5==false)
            {
                GEN_SIGN.FREQ(freq_temp);//отсылаем частоту в генератор сигнала
                GEN_SIGN.SEND();

                if (FLAG_SYNC_GEN_SIGN_POMEH)//если включена синхронизация сигнального и помехового генераторов!
                {
                var _freq=freq_temp+_FREQ_DELTA;
                textBox2.Text=_freq.ToString();
                GEN_POMEH.FREQ(_freq); 
                GEN_POMEH.SEND();
                }    
            } else
            {
                textBox2.Text=freq_temp.ToString();
                GEN_POMEH.FREQ(freq_temp); 
                GEN_POMEH.SEND();
            }
            

            COMMAND_FOR_SERVER = Convert.ToString(freq_current-freq_temp);//отсылаем частоту для ТЕСТ-а

            freq_last = freq_current;
            freq_current = freq_current + freq_step;
            a = freq_delta;
            b = freq_stop- freq_last;
            c = b / a * 100;

            progressBar1.Value = 100 - Convert.ToInt16(c);

            if (freq_current > freq_stop)
            {
                if (freq_setup == 0)  VAR_IH_data_obzor.z = Console1.Text;//режим перебора
                if (freq_setup == 1)  VAR_IH_data.z = Console1.Text;//режим калибровки
                if (freq_setup == 1)  FLAG_IH_load = true;//калибровочная характеристика используется только если прошла калибровка или если она загружена
                freq_current = freq_stop;
              //  progressBar1.Value = 100;
                timer3.Enabled = false;
                FLAG_TIMER3 = true;
                Koeff_usileniya_median = 0;//сбрасываем переменную среднего коэффициента усиления
                flag_progress = 0;
                FLAG_filtr = 1;
                ACH_error(freq_setup);//в режиме калибровки
                //--------------------------
                 checkBox2.Checked = false;
                //   FLAG_CORRECT = 0;
                //--------------------------
                    progressBar1.Visible = false;
                 button3.Text = "SCAN";
                 FLAG_TEST5=false;
               if (serialPort1.IsOpen == true)
                {
                    serialPort1.Close();
                }
            }

                if (FLAG_IH_load == true) Kih_load();     
            }
            catch
            {
                FLAG_TIMER3 = true;
                timer3.Enabled=false;
                MessageBox.Show("Что то не то с IP/порт адресами!");
            }
            
        }

        private void btn_corr_spectr_Click(object sender, EventArgs e)
        {
            if (btn_corr_spectr.Text=="OFF")
            {
                btn_corr_spectr.Text = "ON";
            } else
            {
                btn_corr_spectr.Text = "OFF";
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private int ATT_SEND ()
        {
            int error=0;
            string command1 = "  ~0 upr_at";
            string command2 = "  ~0 upr_at";
            string ch1 = "";
            string ch2 = "";

            if (serialPort1.IsOpen == false) serialPort1.PortName = textBox_com_port.Text;
            if (btn_com_open.Text == "send")
            {
                try
                {
                    var z = 63 - (Convert.ToDouble(textBox_att_m54.Text) * 2);
              //    if (channal_box.Text == "1") chanal = "1"; else chanal = "2";

                    command2 = command2 + "2" + ":" + Convert.ToString(z) + ";  ";
                    command1 = command1 + "1" + ":" + Convert.ToString(z) + ";  ";

                    if (serialPort1.IsOpen == false)
                    {
                        serialPort1.Open();
                    }
                    btn_com_open.ForeColor = Color.Green;
                    serialPort1.Write(command2);
                    serialPort1.Write(command1);
                    // здесь может быть код еще...
                }
                catch (Exception ex)
                {
                    btn_com_open.Text = "send";
                    btn_com_open.ForeColor = Color.Black;
                    // что-то пошло не так и упало исключение... Выведем сообщение исключения
                    Console.WriteLine(string.Format("Port:'{0}' Error:'{1}'", serialPort1.PortName, ex.Message));
                    
                    error=-1;
                }
                serialPort1.Close();
            }

            /*
            else if (serialPort1.IsOpen == true)
            {
                btn_com_open.Text = "open";
                btn_com_open.ForeColor = Color.Black;
                serialPort1.Close();
            }
            */
            return error;
        }

        private int ATT (string ch,string at)
        {
            int error=0;
            string command1 = "  ~0 freq:";
            string command2 = "  ~0 upr_at";
            string chanal = "";

            if (serialPort1.IsOpen == false) serialPort1.PortName = textBox_com_port.Text;
            if (btn_com_open.Text == "send")
            {
                try
                {
                    var z = 63 - (Convert.ToDouble(at) * 2);
                    if (ch == "1") chanal = "1"; else chanal = "2";
                    command2 = command2 + chanal + ":" + Convert.ToString(z) + ";  ";

                    if (serialPort1.IsOpen == false)
                    {
                        serialPort1.Open();
                    }
                    btn_com_open.ForeColor = Color.Green;
                    serialPort1.Write(command2);
                    // здесь может быть код еще...
                }
                catch (Exception ex)
                {
                    btn_com_open.Text = "send";
                    btn_com_open.ForeColor = Color.Black;
                    // что-то пошло не так и упало исключение... Выведем сообщение исключения
                    Console.WriteLine(string.Format("Port:'{0}' Error:'{1}'", serialPort1.PortName, ex.Message));
                    
                    error=-1;
                }
                serialPort1.Close();
            }

            /*
            else if (serialPort1.IsOpen == true)
            {
                btn_com_open.Text = "open";
                btn_com_open.ForeColor = Color.Black;
                serialPort1.Close();
            }
            */
            return error;
        }

        private void btn_com_open_2_Click(object sender, EventArgs e)
        {
            ATT_SEND();
        }

        private void btn_calibrovka_Click(object sender, EventArgs e)
        {
            if (_isServerStarted == true) timer3.Enabled = true;
            timer3.Interval = Convert.ToInt32(textBox_freq_delay.Text);
            freq_start = 428856000;//Hz 428500000
            freq_stop  = 441500000;
            freq_step  = 48828;//Hz
            freq_last  = 0;
            freq_current = freq_start;
            freq_delta = freq_stop - freq_start;
            progressBar1.Value = 0;
            flag_progress = 1;
            Console1.Text = "Режим калибровки:\r\n";
            checkBox1.Checked = false;
            freq_setup = 1;
            VAR_IH_data.lenght = 0;
            FLAG_IH_load = false;
            //btn_corr_spectr.Text = "OFF";
        }

        double[] M_ach        = new double[256];
        double[] F_ach        = new double[256];
        double[] temp_Ach     = new double[128];
        double Max_A = 0;
        double[] Etalon_ach   = new double[128];
        double[] k_ach_i      = new double[128];
        double[] k_ach_q      = new double[128];
        double[] k_ach_temp_i = new double[128];
        double[] k_ach_temp_q = new double[128];

        int Ach_length = 256;//общая длинна характеристики, должна быть больше чем локальная и идти с шагом 48.828 кгц!
        int Korre_IH   = 128;//длинна корректириующей характеристики


        double[] F_w = { 0.000905133, 0.000838113, 0.00063418, 0.000284835, -0.000223718, -0.000910019, -0.00179658, -0.00290892, -0.00427433,
                        -0.00592055, -0.00787417, -0.0101589, -0.0127937, -0.015791, -0.0191543, -0.0228764, -0.0269375, -0.0313029, -0.0359214,
                        -0.0407241, -0.0456226, -0.0505082, -0.0552518, -0.0597035, -0.0636935, -0.0670327, -0.0695151, -0.0709197, -0.0710139,
                        -0.0695567, -0.0663033, -0.0610101, -0.053439, -0.0433641, -0.0305767, -0.0148917, 0.00384677, 0.0257593, 0.0509259, 0.0793812,
                         0.11111, 0.146044, 0.184058, 0.22497, 0.268541, 0.314474, 0.362417, 0.411965, 0.462668, 0.514033, 0.565533, 0.616613, 0.666701,
                         0.715216, 0.761576, 0.805214, 0.845579, 0.882156, 0.914467, 0.942086, 0.964645, 0.981838, 0.993432, 0.999268, 0.999268, 0.993432,
                         0.981838, 0.964645, 0.942086, 0.914467, 0.882156, 0.845579, 0.805214, 0.761576, 0.715216, 0.666701, 0.616613, 0.565533, 0.514033,
                         0.462668, 0.411965, 0.362417, 0.314474, 0.268541, 0.22497, 0.184058, 0.146044, 0.11111, 0.0793812, 0.0509259, 0.0257593, 0.00384677,
                        -0.0148917, -0.0305767, -0.0433641, -0.053439, -0.0610101, -0.0663033, -0.0695567, -0.0710139, -0.0709197, -0.0695151, -0.0670327,
                        -0.0636935, -0.0597035, -0.0552518, -0.0505082, -0.0456226, -0.0407241, -0.0359214, -0.0313029, -0.0269375, -0.0228764, -0.0191543,
                        -0.015791, -0.0127937, -0.0101589, -0.00787417, -0.00592055, -0.00427433, -0.00290892, -0.00179658, -0.000910019, -0.000223718, 0.000284835,
                         0.00063418, 0.000838113, 0.000905133 };

        double [] sdvig(double [] m,int k,int n)
        {
            double[] temp = new double[n];
            int i = 0;

            for (i = 0; i < n; i++)
            {
                if (i < (n-k))
                {
                    temp[i] = m[i + k];
                }
                else
                {
                    temp[i] = m[i-(n-k)];
                }
            }
            return temp;
        }

        double[] Local_ACH   = new double[128];
        double[] DB_ACH_Korr = new double[128];

 void  ACH_error (int rejim)
        {
            int i = 0;
            int j = 0;
            int N_freq = 0;
            int N_freq_low = 0;
            int N_freq_high = 0;
			int N_range=0;
            int freq_centr = 0;
            int freq_low   = 429500000;
            int freq_high  = 440500000;
            int k = 0;

            double[] m = new double[256];
            
            if (FLAG_IH_load)
            {
                if (rejim==1)
                {
                    M_ach = BUF_getter(2, VAR_IH_data.z, 0, VAR_IH_data.lenght);//загружаем коэффициенты истиной АЧХ в массив
                    F_ach = BUF_getter(1, VAR_IH_data.z, 0, VAR_IH_data.lenght);//загружаем частоты истиной АЧХ в массив
                }

                if (rejim == 0)
                {//отключаем режим калибровки полосы обработки!!!
                  M_ach = BUF_getter(2, VAR_IH_data_obzor.z, 0, VAR_IH_data_obzor.lenght);//загружаем коэффициенты истиной АЧХ в массив
                  F_ach = BUF_getter(1, VAR_IH_data_obzor.z, 0, VAR_IH_data_obzor.lenght);//загружаем частоты истиной АЧХ в массив
          //          M_ach = BUF_getter(2, VAR_IH_data.z, 0, VAR_IH_data.lenght);//загружаем коэффициенты истиной АЧХ в массив
          //          F_ach = BUF_getter(1, VAR_IH_data.z, 0, VAR_IH_data.lenght);//загружаем частоты истиной АЧХ в массив
                }

                //определяем максимум в реальной АЧХ

                //определяем центральную частоту в номере отсчёта массива
                freq_centr = 435000000;
                N_freq      = find_number(freq_centr, F_ach);
                N_freq_low  = find_number(freq_low  , F_ach);
                N_freq_high = find_number(freq_high , F_ach);
                N_range = N_freq_high - N_freq_low;

                for (i = N_freq_low; i < (N_freq_high + 1); i++) m[i- N_freq_low] = M_ach[i];//копируем отсчёты АЧХ в промежуточный массив
     
                double MAX_A = m.Max();//ищем максимум а АЧХ
                double error = 0;
                double error_max = 0;

                for (i = 0; i < N_range; i++)
                {
                    error = MAX_A - m[i];
                    if (error_max < error) error_max = error;
                    //error = Math.Round(error, 2);
                }
                error_max = Math.Round(error_max, 2);
                if (error_max > 1.6) error_max = error_max - 0.2;
                textBox_error_ach.Text = Convert.ToString(error_max);
                
            }
            else MessageBox.Show("загрузите параметры АЧХ!");
           
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox_com_port_TextChanged(object sender, EventArgs e)
        {

        }

        private void label_test_Click(object sender, EventArgs e)
        {

        }

        private void textBox_Level_in_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (FLAG_CALIBROVKA == 0) FLAG_CALIBROVKA = 1; else FLAG_CALIBROVKA = 0; //убирает постояннуюу составляющую!!!???
        }

        void Kih_load ()
        {
            int i = 0;
            int j = 0;
            int N_freq = 0;
            int N_freq_low = 0;
            int N_freq_high = 0;
            int freq_centr = 0;
            int freq_low  = 428856000;
            int freq_high = 441000000;
            int k = 0;

            M_ach = BUF_getter(2, VAR_IH_data.z, 0, VAR_IH_data.lenght);//загружаем коэффициенты истиной АЧХ в массив
            F_ach = BUF_getter(1, VAR_IH_data.z, 0, VAR_IH_data.lenght);//загружаем частоты истиной АЧХ в массив

            (k, Max_A) = MAX_f(M_ach, Ach_length);//определяем максимум в реальной АЧХ

            //определяем центральную частоту в номере отсчёта массива
            freq_centr = Convert.ToInt32(textBox_freq_m54.Text);
            N_freq = find_number(freq_centr, F_ach);
            N_freq_low = find_number(freq_low, F_ach);
            N_freq_high = find_number(freq_high, F_ach);

            Array.Clear(temp_Ach, 0, 128);//очищаем массив
            Array.Clear(Etalon_ach, 0, 128);//очищаем массив            

            j = 13;//128/2-51 =13 это номер массива за полосой пропускания!!!
            i = 0;
            for (i = (N_freq - 51); i < (N_freq + 51); i++)//128 - это 6.25Мгц а 5 Мгц - 102 , по -51 и +51 остальное "0"
            {
                if ((i > 0) && (i < Ach_length)) temp_Ach[j] = M_ach[i];//переписываем часть массива во временный массив
                else temp_Ach[j] = 0;
                if ((i > N_freq_low) && (i < N_freq_high)) Etalon_ach[j] = Max_A; 
                j++;
            }

            Array.Copy(temp_Ach, Local_ACH, Korre_IH);

            if (checkBox1.Checked ==true)
            {
                Plot2 fig1 = new Plot2(15, "общая АЧХ", "N", "Дб");//(85.0, "общая АЧХ", "N", "Дб")
                fig1.PlotData(M_ach);
                fig1.Show();
                                
                Plot2 fig2 = new Plot2(15, "локальная АЧХ", "N", "Дб");//(85.0, "локальная АЧХ", "N", "Дб")
                fig2.PlotData(Local_ACH);
                fig2.Show();
            }           

            //-------------------------------------
            // переводим АЧХ в линейный масштаб

            for (i = 0; i < Korre_IH; i++)
            {
                temp_Ach[i] = Math.Pow(10, (temp_Ach[i] / 20));
                Etalon_ach[i] = Math.Pow(10, (Etalon_ach[i] / 20));
                k_ach_i[i] = Etalon_ach[i] / temp_Ach[i];//расчитываем компенсирующуюу АЧХ
   //             if (k_ach_i[i] > 2) k_ach_i[i] = 2;
            }
            //------------------------------------------------------------
           /* 
            Plot2 figX = new Plot2(85.0, "компенсирующуюу АЧХ", "N", "разы");
            figX.PlotData(k_ach_i);
            figX.Show();
           */ 
            //-------------------------------------------------------------
            Array.Clear(k_ach_q, 0, Korre_IH);//очищаем массив                       

            //сдвигаем  по кольцу в сторону - ставим ноль в центр
            k_ach_i = sdvig(k_ach_i, 64, 128);

            //       double[] k_ach_i = { 2.23872, 1.99526, 1.99526, 1.99526, 1.99526, 1.99526, 1.99526, 1.77828, 1.77828, 1.77828, 1.77828, 1.77828, 1.77828, 1.58489, 1.58489, 1.58489, 1.58489, 1.58489, 1.58489, 1.58489, 1.41254, 1.41254, 1.41254, 1.41254, 1.41254, 1.41254, 1.41254, 1.41254, 1.25893, 1.25893, 1.25893, 1.25893, 1.25893, 1.25893, 1.25893, 1.25893, 1.25893, 1.25893, 1.25893, 1.12202, 1.12202, 1.12202, 1.12202, 1.12202, 1.12202, 1.12202, 1.12202, 1.12202, 1.12202, 1.12202, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 10, 8.91251, 8.91251, 8.91251, 7.94328, 7.94328, 7.94328, 7.07946, 7.07946, 7.07946, 7.07946, 6.30957, 6.30957, 6.30957, 5.62341, 5.62341, 5.62341, 5.62341, 5.01187, 5.01187, 5.01187, 4.46684, 4.46684, 4.46684, 4.46684, 3.98107, 3.98107, 3.98107, 3.98107, 3.54813, 3.54813, 3.54813, 3.54813, 3.16228, 3.16228, 3.16228, 3.16228, 2.81838, 2.81838, 2.81838, 2.81838, 2.81838, 2.51189, 2.51189, 2.51189, 2.51189, 2.51189, 2.23872, 2.23872, 2.23872, 2.23872 };
            //----------------------------------------
            //расчёт ИХ от корректирующей характеристики
            k = Convert.ToInt16(LogBase(Korre_IH, 2));//порядок БПФ
            FFTLibrary.Complex fft_z = new FFTLibrary.Complex();
            fft_z.FFT(-1, k, k_ach_i, k_ach_q);//высчитываем обратное  БПФ 

            k_ach_i = DSP.Math.Divide(k_ach_i,51);//убираем усиление БПФ
            k_ach_q = DSP.Math.Divide(k_ach_q,51);

            //сдвигаем результат работы БПФ по кольцу в сторону - ставим ноль в центр
            k_ach_i = sdvig(k_ach_i, 64, 128);
            k_ach_q = sdvig(k_ach_q, 64, 128);

            if (checkBox1.Checked == true)
            {
                Plot2 fig3 = new Plot2(15, "импульсная х-ка", "N", "разы");//(85.0, "импульсная х-ка", "N", "разы")
                fig3.PlotData(k_ach_i);
                fig3.Show();
            }

            //накладываем окно FLATTOP для гладкости ИХ
            double[] windowedTimeSeries_i = DSP.Math.Multiply(k_ach_q, F_w);
            double[] windowedTimeSeries_q = DSP.Math.Multiply(k_ach_i, F_w);
            double[] t = new double[Korre_IH];

            Array.Copy(windowedTimeSeries_i, H_i, Korre_IH);
            Array.Copy(windowedTimeSeries_q, H_q, Korre_IH);

            //---------------вводим усиление вектора ИХ------------
            H_i = DSP.Math.Multiply(H_i, 32767);
            H_q = DSP.Math.Multiply(H_q, 32767);
            //-----------------------------------------------------           

            Array.Copy(H_i, windowedTimeSeries_i, Korre_IH);
            Array.Copy(H_q, windowedTimeSeries_q, Korre_IH);

            //сдвигаем  по кольцу в сторону - ставим ноль в центр
            fft_z.FFT(1, k, windowedTimeSeries_q, windowedTimeSeries_i);

            windowedTimeSeries_i = sdvig(windowedTimeSeries_i, 64, 128);
            windowedTimeSeries_q = sdvig(windowedTimeSeries_q, 64, 128);

            windowedTimeSeries_i = DSP.Math.Multiply(windowedTimeSeries_i, 1);
            windowedTimeSeries_q = DSP.Math.Multiply(windowedTimeSeries_q, 1);

            //------------проверяем полученную АЧХ корректирующего фильтра
            System.Numerics.Complex[] cpxResult = new System.Numerics.Complex[Korre_IH];

            for (i = 0; i < Korre_IH; i++)
            {
                cpxResult[i] = new System.Numerics.Complex(windowedTimeSeries_i[i], windowedTimeSeries_q[i]);
            }

            // Convert and Plot Log Magnitude
            double[] mag = DSP.ConvertComplex.ToMagnitude(cpxResult);
            mag = DSP.Math.Multiply(mag, 1);
            double[] magLog = DSP.ConvertMagnitude.ToMagnitudeDBV(mag);
            magLog = DSP.Math.Add(magLog, -30);

            Array.Copy(magLog, DB_ACH_Korr, Korre_IH);

            if (checkBox1.Checked == true)
            {
                Plot2 fig5 = new Plot2(10, "корректирующая АЧХ", "N", "Дб");
    //          fig5.PlotData(k_ach_i);//DB_ACH_Korr
                fig5.PlotData(DB_ACH_Korr);//
                fig5.Show();
            }
            //MessageBox.Show("Файл сохранен");
        }

        private void cmbWindow_ValueMemberChanged(object sender, EventArgs e)
        {

        }


        private void button5_Click(object sender, EventArgs e)
        {
            DATA_CONVERT_TO_TEXT();
            saveFileDialog2.Filter = "data|*.dat";
            saveFileDialog2.Title = "Сохряняем массив принятый данных";
            if (saveFileDialog2.ShowDialog() == DialogResult.Cancel)   return;
            // сохраняем текст в файл
            string filename =saveFileDialog2.FileName;
            try
            {
                 System.IO.File.WriteAllText(filename, DATA0_IQ_TEXT);
            }
            catch
            {

            }
        }

        void DATA_CONVERT_TO_TEXT () //тут готовим данные из буфера к сохранению в файл, сохраняется N+1 данных чтобы получился ровный смещёный в центр - спектр
        {
            uint z;
            int i = 0;
            int n = Convert.ToInt32(text_N_fft.Text)-1;        

            DATA0_IQ_TEXT = "";
            for (i=0;i<(n+1);i++)
            {
                if (i < n) DATA0_IQ_TEXT = DATA0_IQ_TEXT + tst_data_i[i].ToString() + "," + tst_data_q[i].ToString() + ";" + "\r\n";
                else       DATA0_IQ_TEXT = DATA0_IQ_TEXT + tst_data_i[i].ToString() + "," + tst_data_q[i].ToString() + ";";
            }
        }

        private void channal_box_TextChanged(object sender, EventArgs e)
        {
            string z;
            if (channal_box.Text != "1" && channal_box.Text != "2") channal_box.Text = "1";
            if (channal_box.Text == "1") ATT("2", "31,5");else //
            if (channal_box.Text == "2") ATT("1", "31,5");

            /*
            if (channal_box.Text == "1")
            {
                z = NAME_BLOCK + "_1.txt";//160008_1.txt - файл с формой АЧХ канала
                IH_load_name(z);//загружаю характеристику для номера 160008
            }
            if (channal_box.Text == "1")
            {
                z = NAME_BLOCK + "_2.txt";
                IH_load_name("x_0008_2.txt");
            }
            */
        }

        private void button_AM_Click(object sender, EventArgs e)
        {
            //create a new telnet connection to hostname "x.x.x.x" on port "5025"
            string host = textBox_ip_generator.Text;
            int port = Convert.ToInt32(textBox_port_generator.Text);
#if WORK
            TelnetConnection tc = new TelnetConnection(host, port);
            string prompt = "";
            // while connected
            while (tc.IsConnected && prompt.Trim() != "exit")
            {
                
                if (button_AM.BackColor==SystemColors.Control)
                {
                    button_PM.BackColor    = SystemColors.Control;
                    button_CHIRP.BackColor = SystemColors.Control;
                    button_AM.BackColor    = SystemColors.ControlDark;

                    GEN_SIGN.CMD_AM(true);
                    GEN_SIGN.SEND();
                    prompt = "exit";
                } else
                {
                    button_AM.BackColor = SystemColors.Control;
                    GEN_SIGN.CMD_AM(false);
                    GEN_SIGN.SEND();
                    prompt = "exit";
                }
                
            }
            Console.WriteLine("***DISCONNECTED");
            Console.ReadLine();
#endif
            COMMAND_FOR_SERVER = Convert.ToString(Convert.ToDouble(textBox_freq_gen.Text) - Convert.ToDouble(textBox_freq_m54.Text));
        }

        private void button_CHIRP_Click(object sender, EventArgs e)
        {
            //create a new telnet connection to hostname "x.x.x.x" on port "5025"
            string host = textBox_ip_generator.Text;
            int port = Convert.ToInt32(textBox_port_generator.Text);
#if WORK
            TelnetConnection tc = new TelnetConnection(host, port);
            string prompt = "";
            // while connected
            while (tc.IsConnected && prompt.Trim() != "exit")
            {
                try
                {
                    if (button_CHIRP.BackColor == SystemColors.Control)
                    {
                        textBox_CHIRP_DELTA_F.Enabled = true;
                        button_AM.BackColor = SystemColors.Control;
                        button_PM.BackColor = SystemColors.Control;
                        button_CHIRP.BackColor = SystemColors.ControlDark;

                        prompt = "freq " + textBox_freq_gen.Text + " Hz";
                        tc.WriteLine(prompt);
                        prompt = "pow " + textBox_level_gen.Text + " DBm";
                        tc.WriteLine(prompt);
                        prompt = "outp " + " 1";
                        tc.WriteLine(prompt);

                        int f = Convert.ToInt32(textBox_CHIRP_DELTA_F.Text);

                        prompt = "CHIR:BAND " + f.ToString() + "000";
                        tc.WriteLine(prompt);

                        prompt = "SOUR:CHIR:DIR UP";
                        tc.WriteLine(prompt);

                        prompt = "CHIR:PULS:PER 103 us";
                        tc.WriteLine(prompt);

                        prompt = "CHIR:PULS:WIDT 100 us";
                        tc.WriteLine(prompt);

                        prompt = "CHIR:STAT ON";
                        tc.WriteLine(prompt);
                        //    Console.Write(tc.Read());
                        prompt = "exit";
                    }
                    else
                    {
                        textBox_CHIRP_DELTA_F.Enabled = false;
                        button_CHIRP.BackColor = SystemColors.Control;
                        prompt = "CHIR:STAT" + " OFF";
                        tc.WriteLine(prompt);
                        //    Console.Write(tc.Read());
                        prompt = "exit";
                    }
                } catch
                {
                    prompt = "exit";
                }
               
            }
            Console.WriteLine("***DISCONNECTED");
            Console.ReadLine();
#endif
            COMMAND_FOR_SERVER = Convert.ToString(Convert.ToDouble(textBox_freq_gen.Text) - Convert.ToDouble(textBox_freq_m54.Text));
        }

        private void button_PM_Click(object sender, EventArgs e)
        {
            //create a new telnet connection to hostname "x.x.x.x" on port "5025"
            string host = textBox_ip_generator.Text;
            int port = Convert.ToInt32(textBox_port_generator.Text);
#if WORK
            TelnetConnection tc = new TelnetConnection(host, port);
            string prompt = "";
            // while connected
            while (tc.IsConnected && prompt.Trim() != "exit")
            {
                if (button_PM.BackColor == SystemColors.Control)
                {
                    button_AM.BackColor    = SystemColors.Control;
                    button_CHIRP.BackColor = SystemColors.Control;
                    button_PM.BackColor    = SystemColors.ControlDark;

                    prompt = "freq " + textBox_freq_gen.Text + " Hz";
                    tc.WriteLine(prompt);
                    prompt = "pow " + textBox_level_gen.Text + " DBm";
                    tc.WriteLine(prompt);
                    prompt = "outp " + " 1";
                    tc.WriteLine(prompt);

                    prompt = "LFO2:FREQ 100kHz";
                    tc.WriteLine(prompt);

                    prompt = "PM:INT:SOUR LF2";
                    tc.WriteLine(prompt);

                    prompt = "PM 6";
                    tc.WriteLine(prompt);

                    prompt = "AM:STAT" + " OFF";
                    tc.WriteLine(prompt);

                    prompt = "PM:INT:SOUR LF2N";
                    tc.WriteLine(prompt);

                    prompt = "PM:SOUR INT";
                    tc.WriteLine(prompt);

                    prompt = "PM:STAT ON";
                    tc.WriteLine(prompt);
                    //    Console.Write(tc.Read());
                    prompt = "exit";
                }
                else
                {
                    button_PM.BackColor = SystemColors.Control;
                    prompt = "PM:STAT" + " OFF";
                    tc.WriteLine(prompt);
                    //    Console.Write(tc.Read());
                    prompt = "exit";
                }
            }
            Console.WriteLine("***DISCONNECTED");
            Console.ReadLine();
#endif
            COMMAND_FOR_SERVER = Convert.ToString(Convert.ToDouble(textBox_freq_gen.Text) - Convert.ToDouble(textBox_freq_m54.Text));
        }

        private void textBox_CHIRP_DELTA_F_TextChanged(object sender, EventArgs e)
        {
            //create a new telnet connection to hostname "x.x.x.x" on port "5025"
            string host = textBox_ip_generator.Text;
            int port = Convert.ToInt32(textBox_port_generator.Text);
            TelnetConnection tc = new TelnetConnection(host, port);
            string prompt = "";
            // while connected
            try 
            {
                int z = Convert.ToInt32(textBox_CHIRP_DELTA_F.Text);
                if (z > 0)
                {
                    while (tc.IsConnected && prompt.Trim() != "exit")
                    {
                        if (button_CHIRP.BackColor == SystemColors.ControlDark)
                        {
                            int f = Convert.ToInt32(textBox_CHIRP_DELTA_F.Text);

                            prompt = "CHIR:BAND " + f.ToString() + "000";
                            tc.WriteLine(prompt);

                            prompt = "SOUR:CHIR:DIR UP";
                            tc.WriteLine(prompt);

                            prompt = "CHIR:PULS:PER 103 us";
                            tc.WriteLine(prompt);

                            prompt = "CHIR:PULS:WIDT 100 us";
                            tc.WriteLine(prompt);

                            prompt = "CHIR:STAT ON";
                            tc.WriteLine(prompt);
                            //    Console.Write(tc.Read());

                        }
                        prompt = "exit";
                    }
                }
            } catch
            {

            }           
           
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
          
        }

        private void button6_Click(object sender, EventArgs e)
        {
            N_sch_timer1 = N_usredneniy_noise;
            if (channal_box.Text == "1")
            {
                ATT("2", "31,5");
            //    ATT("2", "31,5");
            //    ATT("2", "31,5");
                ATT("2", "31,5");
            }
            else //
            if (channal_box.Text == "2")
            {
                ATT("1", "31,5");
            //    ATT("1", "31,5");
            //    ATT("1", "31,5");
                ATT("1", "31,5");
            }
        }

        private void mXGToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void генераторСигналаПомехиToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void mXGToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            IZM_Generator GEN_MXG = new IZM_Generator("MXG"); 
            GEN_SIGN = GEN_MXG;
            textBox_port_generator.Text = "5024";
            mXGToolStripMenuItem3.BackColor = Color.Aqua;
            sMA100AToolStripMenuItem2.BackColor= SystemColors.Control;
            GEN_SIGN.host = textBox_ip_generator.Text;
            GEN_SIGN.port = Convert.ToInt32(textBox_port_generator.Text);
            button_AM.Enabled=false;
            button_CHIRP.Enabled=false;
            button_PM.Enabled=false;
        }

        private void sMA100AToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            IZM_Generator GEN_SMA = new IZM_Generator("SMA 100 A");
            GEN_SIGN = GEN_SMA;
            textBox_port_generator.Text = "5025";
            sMA100AToolStripMenuItem2.BackColor = Color.Aqua;
            mXGToolStripMenuItem3.BackColor = SystemColors.Control;
            GEN_SIGN.host = textBox_ip_generator.Text;
            GEN_SIGN.port = Convert.ToInt32(textBox_port_generator.Text);
            button_AM.Enabled=true;
            button_CHIRP.Enabled=true;
            button_PM.Enabled=true;
        }

        private void mXGToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            IZM_Generator GEN_MXG = new IZM_Generator("MXG"); 
            GEN_POMEH= GEN_MXG;
            textBox4.Text = "5024";
            mXGToolStripMenuItem4.BackColor = Color.Aquamarine;
            sMA100AToolStripMenuItem3.BackColor= SystemColors.Control;
            GEN_POMEH.host = textBox3.Text;
            GEN_POMEH.port = Convert.ToInt32(textBox4.Text);
        }

        private void sMA100AToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            IZM_Generator GEN_SMA = new IZM_Generator("SMA 100 A");
            GEN_POMEH = GEN_SMA;
            textBox4.Text = "5025";
            mXGToolStripMenuItem4.BackColor = SystemColors.Control;
            sMA100AToolStripMenuItem3.BackColor = Color.Aquamarine;
            GEN_POMEH.host = textBox3.Text;
            GEN_POMEH.port = Convert.ToInt32(textBox4.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //create a new telnet connection to hostname "x.x.x.x" on port "5025 or 5024"
       
            try
            {
                GEN_POMEH.FREQ(Convert.ToInt32(textBox2.Text));
                GEN_POMEH.POW(textBox1.Text);
                //      GEN_POMEH.OUT (1);
                GEN_POMEH.SEND();
            }
            catch
            {
                MessageBox.Show("Проверте настройки сети!");
            }
            
        }

        private void btn_cal_ch_Click(object sender, EventArgs e)
        {
            textBox_att_m54.Text = "31,5";
            ATT_SEND();
            FLAG_CALIBR_CH = true;//запускаем стейт машину калибровки
            timer5.Start();
            btn_cal_ch.BackColor = SystemColors.ControlDarkDark;
        }

        int sch_line (string a)
        {
            int n = 0;
            n = a.Split('\n').Count();
            return n;
        }

        enum STATE {START,ST1,ST2,ST3,ST4,END};
        STATE stt;

        private void textBox_ip_generator_TextChanged(object sender, EventArgs e)
        {
            GEN_SIGN.host = textBox_ip_generator.Text;
        }

        private void textBox_port_generator_TextChanged(object sender, EventArgs e)
        {
            GEN_SIGN.port = Convert.ToInt32(textBox_port_generator.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            GEN_POMEH.host=textBox3.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            GEN_POMEH.port = Convert.ToInt32(textBox4.Text);
        }

        double LEVEL_Pin_old=0;

        private void timer5_Tick(object sender, EventArgs e)
        {
            if (FLAG_CALIBR_CH) CAL_CH_st();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int freq = Convert.ToInt32(textBox2.Text);
                if ((freq > 443000000) || (freq < 427000000)) FLAG_CORRECT = 3;
                else FLAG_CORRECT = 0;

                if (checkBox2.Checked)
                {
                    GEN_POMEH.OUT(1);
                    GEN_POMEH.SEND();
                }
                else
                {
                    GEN_POMEH.OUT(0);
                    GEN_POMEH.SEND();
                }
            }
            catch
            {
                MessageBox.Show("Что-то не то с IP адресом!");
            }
            
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox3.Checked)
                {                    
                    GEN_SIGN.OUT(1);
                    GEN_SIGN.SEND();
                }
                else
                {
                    GEN_SIGN.OUT(0);
                    GEN_SIGN.SEND();
                }
            } catch
            {
                MessageBox.Show("Что-то не то с IP адресом!");
            }
            
        }

        private void checkBox_sync_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_sync.Checked)
            {
                FLAG_SYNC_GEN_SIGN_POMEH = true;
            } else
            {
                FLAG_SYNC_GEN_SIGN_POMEH = false;
            }
            
        }

        int FLAG_COMPORT_ERROR = 0;
        private void button_com_port_Click(object sender, EventArgs e)
        {
            string command1 = "  ~0 help;";
            if (serialPort1.IsOpen == false)
            {
                // Allow the user to set the appropriate properties.
                serialPort1.PortName = textBox_com_port.Text;
                serialPort1.BaudRate = 115200;

                // Set the read/write timeouts
                serialPort1.ReadTimeout = 500;
                serialPort1.WriteTimeout = 500;

                try
                {
                    if (serialPort1.IsOpen == false)
                    {
                        FLAG_COMPORT_ERROR = 1;
                        serialPort1.Open();                        
                    }
                    Debug.WriteLine("open");
                    serialPort1.Write(command1);
                    // здесь может быть код еще...
                }
                catch (Exception ex)
                {
                    // что-то пошло не так и упало исключение... Выведем сообщение исключения
                    Console.WriteLine(string.Format("Port:'{0}' Error:'{1}'", serialPort1.PortName, ex.Message));
                    MessageBox.Show("Проблема с портом!");
                }
            }
            else
            {
                serialPort1.Close();
                button_com_port.Text = "open";
            }

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void button8_Click(object sender, EventArgs e)
        {
            GEN_POMEH.OUT(0);/*выключаем выход сигнала помехи*/
            GEN_POMEH.SEND();
            checkBox2.Checked = false;
            FLAG_CORRECT = 1;
            checkBox2.Checked = false;
            checkBox3.Checked = true;
            radioButton1.Checked = true;
            label61.Text = "РЕЖИМ СКАНИРОВАНИЯ:ТЕСТ1";
            FLAG_TEST3 = false;
            textBox_att_m54.Text = "31,5";
            ATT_SEND();
            textBox_freq_gen.Text = "435000000";
            textBox_freq_start.Text = "429500000";
            textBox_freq_step.Text = "250000";
            textBox_freq_stop.Text = "440500000";
            textBox_freq_delay.Text = "500";
            textBox_level_gen.Text = "25";
            textBox2.Text = (Convert.ToInt32(textBox_freq_gen.Text) + 3000000).ToString();
            textBox_freq_m54.Text = textBox_freq_gen.Text;
            checkBox_sync.Checked = false;
            btn_com_open_Click(null, null);
            button7_Click(null, null);
            btn_telnet_gen_Click(null, null);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label51.Text = "мин. динамический диапазон";
                label46.Text = "Измеренный динамический диапазон";
            }
            else
            {
                label51.Text = "мин. подавление помеховых сигналов";
                label46.Text = "Измеренный уровень подавления";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FLAG_CORRECT = 1;
            radioButton1.Checked = true;
            label61.Text = "РЕЖИМ СКАНИРОВАНИЯ:ТЕСТ2";
            FLAG_TEST3 = false;
            FLAG_TEST2 = true;
            textBox_att_m54.Text = "31,5";
            ATT_SEND();
            textBox_freq_gen.Text = "435000000";
	        textBox_freq_start.Text="429500000";
            textBox_freq_step.Text ="250000";
            textBox_freq_stop.Text ="440500000";
            textBox_freq_delay.Text="500";
	        textBox_level_gen.Text ="25";
            textBox1.Text="10";
            textBox2.Text = (Convert.ToInt32(textBox_freq_gen.Text) + 3000000).ToString();
            textBox_freq_m54.Text = textBox_freq_gen.Text;
            btn_com_open_Click(null, null);
            button7_Click(null, null);
            btn_telnet_gen_Click(null, null);
            checkBox_sync.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FLAG_CORRECT = 0;
            GEN_POMEH.OUT(0);/*выключаем выход сигнала помехи*/
            GEN_POMEH.SEND();
            checkBox2.Checked = false;
            label61.Text = "РЕЖИМ СКАНИРОВАНИЯ:ТЕСТ3";
            textBox_att_m54.Text = "31,5";
            ATT_SEND();
            textBox_freq_gen.Text   = "435000000";
            textBox_freq_start.Text = "395000000";
            textBox_freq_step.Text  = "500000";
            textBox_freq_stop.Text  = "419500000";
            textBox_freq_delay.Text = "500";
            textBox_level_gen.Text  = "25";
            textBox_freq_m54.Text = textBox_freq_gen.Text;
            checkBox_sync.Checked = false;
            btn_com_open_Click(null, null);
            btn_telnet_gen_Click(null, null);
            //запускаем калибровку
            FLAG_TEST3 = true;
            btn_cal_ch_Click(null, null);
            radioButton2.Checked = true;
            FLAG_TEST2 = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FLAG_CORRECT = 0;
            GEN_POMEH.OUT(0);/*выключаем выход сигнала помехи*/
            GEN_POMEH.SEND();
            checkBox2.Checked = false;
            label61.Text = "РЕЖИМ СКАНИРОВАНИЯ:ТЕСТ4";
            textBox_att_m54.Text = "31,5";
            ATT_SEND();
            textBox_freq_gen.Text = "435000000";
            textBox_freq_start.Text = "450500000";
            textBox_freq_step.Text = "500000";
            textBox_freq_stop.Text = "475000000";
            textBox_freq_delay.Text = "500";
            textBox_level_gen.Text = "25";
            textBox_freq_m54.Text = textBox_freq_gen.Text;
            checkBox_sync.Checked = false;
            btn_com_open_Click(null, null);
            btn_telnet_gen_Click(null, null);
            //запускаем калибровку
            FLAG_TEST3 = true;
            btn_cal_ch_Click(null, null);
            radioButton2.Checked = true;
            FLAG_TEST2 = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            FLAG_CORRECT = 1;
            radioButton1.Checked = true;
            checkBox2.Checked = false;
            label61.Text = "РЕЖИМ СКАНИРОВАНИЯ:ТЕСТ5";
            textBox_att_m54.Text = "31,5";
            ATT_SEND();
            textBox_freq_gen.Text = "435000000";
            textBox_freq_start.Text = "395000000";
            textBox_freq_step.Text = "450000";
            textBox_freq_stop.Text = "419500000";
            textBox_freq_delay.Text = "500";
            textBox_level_gen.Text = "25";
            textBox1.Text         = "10";/*уровень сигнала генератора помехи*/
            textBox_freq_m54.Text = textBox_freq_gen.Text;
            checkBox_sync.Checked = false;
            btn_com_open_Click(null, null);
            btn_telnet_gen_Click(null, null);
            button7_Click(null, null);
            //запускаем калибровку
            FLAG_TEST3 = false;
            FLAG_TEST5 = true;
            btn_cal_ch_Click(null, null);
            FLAG_TEST2 = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            FLAG_CORRECT = 1;
            radioButton1.Checked = true;
            checkBox2.Checked = false;
            label61.Text = "РЕЖИМ СКАНИРОВАНИЯ:ТЕСТ6";
            textBox_att_m54.Text = "31,5";
            ATT_SEND();
            textBox_freq_gen.Text = "435000000";
            textBox_freq_start.Text = "450500000";
            textBox_freq_step.Text = "450000";
            textBox_freq_stop.Text = "475000000";
            textBox_freq_delay.Text = "500";
            textBox_level_gen.Text = "25";
            textBox1.Text = "10";/*уровень сигнала генератора помехи*/
            textBox_freq_m54.Text = textBox_freq_gen.Text;
            checkBox_sync.Checked = false;
            btn_com_open_Click(null, null);
            btn_telnet_gen_Click(null, null);
            button7_Click(null, null);
            //запускаем калибровку
            FLAG_TEST3 = false;
            FLAG_TEST5 = true;
            btn_cal_ch_Click(null, null);
            FLAG_TEST2 = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            FLAG_CORRECT = 2;
            radioButton1.Checked = true;
            label61.Text = "РЕЖИМ СКАНИРОВАНИЯ:ТЕСТ7";
            FLAG_TEST3 = false;
            FLAG_TEST5 = false;
            textBox_att_m54.Text = "31,5";
            ATT_SEND();
            textBox_freq_gen.Text = "435000000";
            textBox_freq_start.Text = "429500000";
            textBox_freq_step.Text = "500000";
            textBox_freq_stop.Text = "440500000";
            textBox_freq_delay.Text = "500";
            textBox_level_gen.Text = "25";
            textBox1.Text = "10";
            textBox2.Text = "419500000";//генератор помехи
            textBox_freq_m54.Text = textBox_freq_gen.Text;
            btn_com_open_Click(null, null);
            button7_Click(null, null);
            btn_telnet_gen_Click(null, null);
            checkBox_sync.Checked = false;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            FLAG_TEST2 = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            FLAG_CORRECT = 3;
            radioButton1.Checked = true;
            label61.Text = "РЕЖИМ СКАНИРОВАНИЯ:ТЕСТ8";
            FLAG_TEST3 = false;
            FLAG_TEST5 = false;
            textBox_att_m54.Text = "31,5";
            ATT_SEND();
            textBox_freq_gen.Text = "435000000";
            textBox_freq_start.Text = "429500000";
            textBox_freq_step.Text = "500000";
            textBox_freq_stop.Text = "440500000";
            textBox_freq_delay.Text = "500";
            textBox_level_gen.Text = "25";
            textBox1.Text = "10";
            textBox2.Text = "450500000";//генератор помехи
            textBox_freq_m54.Text = textBox_freq_gen.Text;
            btn_com_open_Click(null, null);
            button7_Click(null, null);
            btn_telnet_gen_Click(null, null);
            checkBox_sync.Checked = false;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            FLAG_TEST2 = false;
        }

        private void textBox_level_gen_TextChanged(object sender, EventArgs e)
        {
  
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            label61.Text = "РЕЖИМ СКАНИРОВАНИЯ:ТЕСТ2(Б)";
            FLAG_TEST3 = false;
            FLAG_TEST2 = true;
            textBox_att_m54.Text = "13";
            ATT_SEND();
            textBox_freq_gen.Text = "435000000";
            textBox_freq_start.Text = "429500000";
            textBox_freq_step.Text = "250000";
            textBox_freq_stop.Text = "440500000";
            textBox_freq_delay.Text = "2000";
            textBox_level_gen.Text = "10";
            textBox1.Text = "0";
            textBox2.Text = (Convert.ToInt32(textBox_freq_gen.Text) + 3000000).ToString();
            textBox_freq_m54.Text = textBox_freq_gen.Text;
            btn_com_open_Click(null, null);
            button7_Click(null, null);
            btn_telnet_gen_Click(null, null);
            checkBox_sync.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
        }

        private void CAL_CH_st ()
        {
            int error=0;
            double z =0;
            double delta_old=0;
            double delta_new=0;
            double delta=0;
            double ATT=0;
            int sign=0;
            double LVL_ref = 10_300_000;

            string host = textBox_ip_generator.Text;
            int port = Convert.ToInt32 (textBox_port_generator.Text);
            ATT      = Convert.ToDouble(textBox_att_m54.Text);
            Console.WriteLine("ATT:"+ATT);
            //-------------------
            GEN_POMEH.host = textBox3.Text;
            GEN_POMEH.port = Convert.ToInt32(textBox4.Text);

            GEN_SIGN.host = textBox_ip_generator.Text;
            GEN_SIGN.port = Convert.ToInt32(textBox_port_generator.Text);

            //-------------------

            if (stt==STATE.START)            //устанавливаем частоту центра рабочего диапазона
            {
                FLAG_filtr = 0;
                Console.WriteLine("STATE.START");
                stt = STATE.ST1;
                GEN_SIGN.FREQ (435000000); //устанавливаем генератор сигнала
                GEN_SIGN.SEND ();
                textBox_freq_m54.Text = "435000000";

                GEN_POMEH.OUT (0); checkBox2.Checked = false;
                GEN_POMEH.SEND();

                textBox_att_m54.Text="31,5";//выставляем аттенюатор на максимум
                error=ATT_SEND ();                //устанавливаем аттенюатор М54
                freq_send(435000000);       //устанавливаем частоту М54

               if (error==-1) {stt = STATE.END;button_com_port.BackColor=Color.Red;MessageBox.Show("не работает компорт!");}//если ошибка в процессе работы - останавливаемся!

            } else
            if (stt == STATE.ST1)
            {
                Console.WriteLine("STATE.ST1");
                z =LVL_Pin_DBm;
                var att_diff=Math.Floor(10-z);//считаем предварительную разницу между ожидаемым сигналом и реальным
                var y = 1_000_000 * Math.Pow(10, (z / 10));//чтобы получить мкВт
                var x = Math.Round(y, 2);

                if ((ATT==31.5)&&(x> LVL_ref)) 
                {
                    stt = STATE.END;
                    MessageBox.Show("Слишком высокий уровень входного сигнала!");                    
                } else
                {
                   ATT=ATT-att_diff; 
                   if (ATT<0) ATT=0; 
                   textBox_att_m54.Text=ATT.ToString();
                   error=ATT_SEND (); 
                   stt = STATE.ST2;
                }
                Console.WriteLine("LVL_Pin_DBm:"+z); 
                if (error==-1) {stt = STATE.END;button_com_port.BackColor=Color.Red;MessageBox.Show("не работает компорт!");}//если ошибка в процессе работы - останавливаемся!

            }else
            if (stt == STATE.ST2) //измеряем уровень сигнала в полосе 5 МГц
            {
         //     Console.WriteLine("STATE.ST2");
                z =LVL_Pin_DBm;
                var y = 1_000_000 * Math.Pow(10, (z / 10));//чтобы получить мкВт
                var x = Math.Round(y, 2);
                delta    =x- LVL_ref;
                delta_old=Math.Abs(LVL_ref - LEVEL_Pin_old); //предыдущая дельта
                delta_new=Math.Abs (delta);              //текущая дельта
                sign     =Math.Sign(delta);              //знак текущей дельты
        //      Console.WriteLine("LVL_Pin_DBm:"+z);

                    if (sign<0)
                    {                        
                        if (ATT>0) ATT=ATT-0.5; 
                        else
                        {
                            stt = STATE.END;
                            MessageBox.Show("Слишком низкий уровень входного сигнала!");                            
                        } 
                        textBox_att_m54.Text=ATT.ToString();
                        error=ATT_SEND (); 
                        stt = STATE.ST2;
                    } else
                    {
                        if (delta_new>delta_old) 
                        {
                            if (ATT<31) ATT=ATT+0.5;
                            textBox_att_m54.Text=ATT.ToString();
                            error=ATT_SEND (); 
                        } 
                        stt = STATE.END;
                    }   
                
                if (error==-1) {stt = STATE.END;button_com_port.BackColor=Color.Red;MessageBox.Show("не работает компорт!");}//если ошибка в процессе работы - останавливаемся!
                LEVEL_Pin_old=LVL_Pin_DBm;               //запоминаем предыдущее значение
            } else
             if (stt == STATE.END)
             {
                 FLAG_filtr = 1;
                 Console.WriteLine("STATE.END");
                 timer5.Stop();
                 FLAG_CALIBR_CH=false;
                 stt = STATE.START;
                 btn_cal_ch.BackColor = SystemColors.Control;
                if (FLAG_TEST3)
                {
                    LEVEL_TEST_SIGNAL = LVL_Pin_DBm;//запоминаем уровень тестового сигнала
                }

                if (FLAG_TEST5) checkBox2.Checked = true;


             }
        }

        private void button17_Click(object sender, EventArgs e)
        {            
            FLAG_CORRECT = 0;            
            radioButton1.Checked = true;
            label61.Text = "РЕЖИМ :ТЕСТ9";
            textBox_att_m54.Text = "31,5";
            ATT_SEND();
            var freq_low = Convert.ToInt32(textBox_freq_m54.Text) - 2500000;
            var freq_hi = Convert.ToInt32(textBox_freq_m54.Text) + 2500000;
            textBox_freq_gen.Text = freq_low.ToString();
            textBox_level_gen.Text = "10";
            textBox1.Text = "10";
            textBox2.Text = freq_hi.ToString();
            btn_com_open_Click(null, null);
            button7_Click(null, null);
            btn_telnet_gen_Click(null, null);
            checkBox2.Checked = true;
            checkBox3.Checked = true;
        }

        private void btn_load_ach_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "text|*.txt|data|*.dat|fir coef|*.coff";
            openFileDialog1.Title = "Load an File";

            openFileDialog1.ShowDialog();
            
            if (openFileDialog1.CheckFileExists==true)
            {
                // получаем выбранный файл
                string filename = openFileDialog1.FileName;
                // сохраняем текст в файл
                try 
                {
                    VAR_IH_data.z = System.IO.File.ReadAllText(filename);
                    VAR_IH_data.lenght = sch_line(VAR_IH_data.z)-2;
                    FLAG_IH_load = true;
                    Kih_load();
                    ACH_error(1);
                    btn_load_ach.ForeColor = Color.Green;
                    textBox_error_ach.Text = "0";
                }
                catch
                {

                }                
            }            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int freq = Convert.ToInt32(textBox2.Text);
     //     if ((freq> 443000000)||(freq < 427000000)) FLAG_CORRECT = 3;
     //     if  (freq > 443000000)                     FLAG_CORRECT = 3;  
        }

        void IH_load ()
        {
            string path = @"x.txt";
            StreamReader sr = new StreamReader(path);

            if (sr!=null)
            {
                try
                {
                    VAR_IH_data.z = System.IO.File.ReadAllText(path);
                    VAR_IH_data.lenght = sch_line(VAR_IH_data.z) - 2;
                    FLAG_IH_load = true;
                    Kih_load();
                    ACH_error(1);
                    btn_load_ach.ForeColor = Color.Green;
                }
                catch
                {

                }
            }
        }

        void IH_load_name(string ch)
        {           

            string path = ch;//@"x.txt"
            StreamReader sr = new StreamReader(path);

            if (sr != null)
            {
                try
                {
                    VAR_IH_data.z = System.IO.File.ReadAllText(path);
                    VAR_IH_data.lenght = sch_line(VAR_IH_data.z) - 2;
                    FLAG_IH_load = true;
                    Kih_load();
                    ACH_error(1);
                    btn_load_ach.ForeColor = Color.Green;
                }
                catch
                {
                    Console.WriteLine("чёто не так с загрузкой файла!");
                }
            }
        }

        int find_number (int freq,double [] m)//ищет ближайший порядковый номер в массиве частот
        {
            int i = 0;
            while (freq>Convert.ToInt32(m[i]))
            {
                if (i <( m.Length-1)) i = i + 1; else break;
            }
            return (i);
        }

        double [] BUF_getter(int var,string text_from_file, int pos,int buf_n)//получает числовое  значение из стринговой переменной , разделённой рзделителем
        {
            int i = 0;
            double[] m = new double[buf_n];

            for (i = 0; i < buf_n; i++)//размер буфера
            {
                var value = data_finder(text_from_file, pos);
              if (var==1)  m[i] = value.Item1;//получаем значение
              if (var==2)  m[i] = value.Item2;
                            pos = value.Item3;//получаем порядковый номер
                if (pos == 999999) break;
            }
            return m;
        }

        public Tuple<double,double, int> data_finder(string text_from_file, int pos)
        {
            int i = 0;
            int a1, a2,a3,a4,a5;
            double data  = 0;
            double data2 = 0;
            int index;
            a1 = pos;
            i = a1;

            while ((text_from_file.Substring(i, 1) != "\r") && (i<(text_from_file.Length-1)))   //ищем новую строку
            {
                i = i + 1;
            }

            a3 = i+1;

            while ((text_from_file.Substring(i, 1) != ";")&&(i < (text_from_file.Length - 1)))  //ищем первый разделиетель
            {
                 i = i + 1;
            }
             i = i + 1;
            a1 = i;
            a4 = i - 1;

            if (i < (text_from_file.Length - 1)) //
            {
                while ((text_from_file.Substring(i, 1) != "\r") && (text_from_file.Substring(i, 1) != ";") && (i < (text_from_file.Length - 1))) //ищем второй разделитель
                {
                    i = i + 1;
                }
            }
            else
            {
                Debug.WriteLine("не хватает данных!");
                return Tuple.Create(0.0,0.0, 999999);
            }
            
            a2 = i  - a1;
            a5 = a4 - a3;

       //     Debug.WriteLine("a1:" + a1);
       //     Debug.WriteLine("a2:" + a2);

            index = i;

          //       Debug.WriteLine(">" + text_from_file.Substring(a3, a5));

            try
            {
                data2 = Convert.ToDouble(text_from_file.Substring(a3, a5));
            }
            catch
            {
                MessageBox.Show("Введите правильные данные");
                index = 999999;
            }
            try
            {
                data = Convert.ToDouble(text_from_file.Substring(a1, a2));
            }
            catch
            {
                MessageBox.Show("Введите правильные данные");
                index = 999999;
            }            

            return Tuple.Create(data2,data, index);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
            {
                if (FLAG_IH_load == true)
                {
                    if (checkBox1.Checked == true)
                    {
                        Plot2 fig1 = new Plot2(85.0, "общая АЧХ", "N", "Дб");
                        fig1.PlotData(M_ach);
                        fig1.Show();

                        Plot2 fig2 = new Plot2(85.0, "локальная АЧХ", "N", "Дб");
                        fig2.PlotData(Local_ACH);
                        fig2.Show();

                        Plot2 fig5 = new Plot2(10, "корректирующая АЧХ", "N", "Дб");
                        fig5.PlotData(DB_ACH_Korr);
                        fig5.Show();
                    }
                }
            }
            else
            {

            }           
        }

        private void btn_ach_control_Click(object sender, EventArgs e)
        {

        }


        private bool CFG_load()
        {
            bool error = false;
            string path = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            path = System.IO.Path.GetDirectoryName(path);

            // получаем выбранный файл
            string filename = "cfg.dat";
           try
            {
                XmlSerializer xmlSerialaizer = new XmlSerializer(typeof(Config));
                FileStream fr = new FileStream(filename, FileMode.Open);

                // преобразуем строку в байты
                //byte[] array = new byte[fr.Length];
                // считываем данные
                //fr.Read(array, 0, array.Length);
                // декодируем байты в строку
                //string textFromFile = System.Text.Encoding.Default.GetString(array);
                //Console.WriteLine($"Текст из файла: {textFromFile}");

                cfg = (Config)xmlSerialaizer.Deserialize(fr);
      
                fr.Close();                
                
                FLAG_filtr     = Convert.ToInt32(cfg.FLAG_filtr);
                CORRECT_FREQ   = Convert.ToInt32(cfg.CORRECT_FREQ);
                Koeff_measure0 = Convert.ToDouble(cfg.Koeff_measure0);
                Koeff_measure1 = Convert.ToDouble(cfg.Koeff_measure1);
                Koeff_measure2 = Convert.ToDouble(cfg.Koeff_measure2);
                Koeff_measure3 = Convert.ToDouble(cfg.Koeff_measure3);
                N_filtr_usr0   = Convert.ToInt32(cfg.N_filtr_usr0);
                N_filtr_usr1   = Convert.ToInt32(cfg.N_filtr_usr1);
                SPUR0_REMOVE_N = Convert.ToInt32(cfg.SPUR0_REMOVE_N);
                SPUR1_REMOVE_N = Convert.ToInt32(cfg.SPUR1_REMOVE_N);
                N_usredneniy_noise= Convert.ToInt32(cfg.N_usredneniy_noise);
                Console.WriteLine("Успешно загружен cfg.dat!");
                label_cfg.Visible = false;

            }
            catch
            {
                label_cfg.Visible = true;
                Console.WriteLine("Что-то не то с cfg.dat!");
            }

            return error;
        }

    }
}
