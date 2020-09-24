using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public string POW(int pow)   //команда установки мощности
        {
            string prompt = "error!";

            if (MODEL == "MXG")
            {
                prompt = ":POW " + pow.ToString() + "DBM;";
            }
            else
            if (MODEL == "SMA 100 A")
            {
                prompt = "pow " + pow.ToString() + " DBm;";
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
                prompt = "outp " + outp.ToString() + " DBm;";
            }
            CMD_MSG = CMD_MSG + prompt;

            return prompt;
        }

        public string SEND()
        {
            TelnetConnection tc = new TelnetConnection(host, port);
            string prompt = "";
            string msg = "";

            while (tc.IsConnected == false) { };
            if (MODEL == "MXG")       prompt = CMD_MSG + "\r";
                else
            if (MODEL == "SMA 100 A") prompt = CMD_MSG + "";
            
            tc.WriteLine(prompt);
            tc.CLOSE();
       //   msg = tc.Read();//  

            CMD_MSG="";
            return msg;
        }

        /*
                public string CMD_FREQ(int freq, string host, int port)
                {
                    TelnetConnection tc = new TelnetConnection(host, port);
                    string prompt = ""; 
                    string msg= "CMD_FREQ ERROR!";

                    while (tc.IsConnected==false) { };
               //     while (tc.IsConnected && prompt.Trim() != "exit")
                    {
                        if (MODEL == "MXG"      )  prompt = "FREQ " + freq.ToString() + "Hz;\n\r"; else
                        if (MODEL == "SMA 100 A")  prompt = "freq " + freq.ToString() + " Hz";
                        tc.WriteLine(prompt);
                        msg = tc.Read();//
                        prompt = "exit";
                    }
                    return msg;
                }

                public string CMD_POW(int pow, string host, int port)
                {
                    TelnetConnection tc = new TelnetConnection(host, port);
                    string prompt = "";
                    string msg = "CMD_POW ERROR!";

                    while (tc.IsConnected == false) { };
             //       while (tc.IsConnected && prompt.Trim() != "exit")
                    {
                        if (MODEL == "MXG") prompt = "POW " + pow.ToString() + "DBM;\n\r";
                        else
                        if (MODEL == "SMA 100 A") prompt = "pow " + pow.ToString() + " DBm";
                        tc.WriteLine(prompt);
                        msg = tc.Read();//
                        prompt = "exit";
                    }
                    return msg;
                }

                public string CMD_OUTp(string outp, string host, int port)
                {
                    TelnetConnection tc = new TelnetConnection(host, port);
                    string prompt = "";
                    string msg = "CMD_OUTp ERROR!";
                    string data = "";
                    if (outp == "ON") data = " 1"; else data = " 0";

                    while (tc.IsConnected == false) { };
              //      while (tc.IsConnected && prompt.Trim() != "exit")
                    {
                        if (MODEL == "MXG") prompt = "OUTPut " + outp + ";\n\r";
                        else
                        if (MODEL == "SMA 100 A") prompt = "outp " + data + "";
                        tc.WriteLine(prompt);
                        msg = tc.Read();//
                        prompt = "exit";
                    }

                    return msg;
                }

                public string CMD_AM (string cmd,string host,int port)
                {
                    TelnetConnection tc = new TelnetConnection(host, port);
                    string prompt = "";

                    return "OK";
                }
        */



    }
}
