using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MinimalisticTelnet;

namespace fft_writer
{
  public  class IZM_Generator
    {
        private string MODEL;
        private string CMD_MSG;
        public string host { set; get; }
        public int port { set; get; }

       public IZM_Generator (string m) //конструктор
        {
            MODEL = m;
        }



        public string FREQ (int freq) //команда установки частоты
        {
            string prompt="error!";

            if (MODEL=="MXG")
            {
                prompt= ":FREQ " + freq.ToString() + "Hz;";
            } else
            if (MODEL == "SMA 100 A")
            {
                prompt = "freq " + freq.ToString() + " Hz;";
            }
            CMD_MSG = CMD_MSG + prompt;

            return prompt;
        }

        public string POW(string pow)   //команда установки мощности
        {
            string prompt = "error!";

            if (MODEL == "MXG")
            {
                prompt = ":POW " + pow + "DBM;";
            }
            else
            if (MODEL == "SMA 100 A")
            {
                prompt = "pow " + pow + " DBm;";
            }
            CMD_MSG = CMD_MSG + prompt;

            return prompt;
        }

        public string OUT(int outp)   //команда ВКЛ/ВЫКЛ выхода
        {
            string prompt = "error!";
            string z;
            if (MODEL == "MXG")
            {
                if (outp == 1) z = " ON"; else z = " OFF";
                prompt = ":OUTPut " + z + ";\n";
            }
            else
            if (MODEL == "SMA 100 A")
            {
                if (outp == 1) z = " ON"; else z = " OFF";
                prompt = "OUTP " + z + " ";
            }
            CMD_MSG = CMD_MSG + prompt;

            return prompt;
        }

        public string SEND()
        {
            string prompt = "";
            string msg = "";

        //    try
            {
                TelnetConnection tc = new TelnetConnection(host, port);
                while (tc.IsConnected == false) { };
                if (MODEL == "MXG") prompt = CMD_MSG + "\r";
                else
                if (MODEL == "SMA 100 A") prompt = CMD_MSG + "";

                tc.WriteLine(prompt);
                tc.CLOSE();
                //   msg = tc.Read();//  
            }
      //      catch
            {
       //         MessageBox.Show("Что то не то с IP/порт адресами!");
            }            

            CMD_MSG="";
            return msg;
        }

        

        public string CMD_AM (bool st)
        {
            string prompt = "error!";
            string z;
            if (MODEL == "MXG")
            {               
                prompt = ":OUTPut " + ";\n";
            }
            else
            if (MODEL == "SMA 100 A")
            {
                if (st)
                {
                    prompt  = "PM:STAT" + " OFF;";
                    prompt += "LFO1:FREQ 100kHz;";
                    prompt += "AM:INT:SOUR LF1;";
                    prompt += "AM:DEPT 1;";
                    prompt += "AM:SOUR INT;";
                    prompt += "AM:STAT" + " ON;";
                } else
                {
                     prompt  = "AM:STAT" + " OFF";
                }
               
            }
            CMD_MSG = CMD_MSG + prompt;

            return prompt;
        }
    



    }
}
