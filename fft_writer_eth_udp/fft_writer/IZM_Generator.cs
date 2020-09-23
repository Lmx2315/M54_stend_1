using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinimalisticTelnet;

namespace fft_writer
{
    class IZM_Generator
    {
        private string MODEL;
        public int FREQ   { get; set; }
        public int POW    { get; set; }
        public int OUTPut { get; set; }

        IZM_Generator (string m) //конструктор
        {
            MODEL = m;
        }

        string CMD_FREQ (int freq) //команда установки частоты
        {
            string prompt="error!";

            if (MODEL=="MXG")
            {
                prompt= "FREQ " + freq.ToString() + "Hz;";
            } else
            if (MODEL == "SMA 100 A")
            {
                prompt = "freq " + freq.ToString() + " Hz;";
            }
            return prompt;
        }

        string CMD_POW(int pow)   //команда установки мощности
        {
            string prompt = "error!";

            if (MODEL == "MXG")
            {
                prompt = "POW " + pow.ToString() + "DBM;";
            }
            else
            if (MODEL == "SMA 100 A")
            {
                prompt = "pow " + pow.ToString() + " DBm;";
            }
            return prompt;
        }

        string CMD_OUT(int outp)   //команда ВКЛ/ВЫКЛ выхода
        {
            string prompt = "error!";
            string z;
            if (MODEL == "MXG")
            {
                if (outp == 1) z = " ON"; else z = " OFF";
                prompt = "OUTPut " + z + ";\n";
            }
            else
            if (MODEL == "SMA 100 A")
            {
                prompt = "outp " + outp.ToString() + " DBm;";
            }
            return prompt;
        }

        public string CMD_FREQ(int freq, string host, int port)
        {
            TelnetConnection tc = new TelnetConnection(host, port);
            string prompt = ""; 
            string msg= "CMD_FREQ ERROR!";

            while (tc.IsConnected && prompt.Trim() != "exit")
            {
                if (MODEL == "MXG"      )  prompt = "FREQ " + freq.ToString() + "Hz;\n"; else
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

            while (tc.IsConnected && prompt.Trim() != "exit")
            {
                if (MODEL == "MXG") prompt = "POW " + pow.ToString() + "DBM;\n";
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

            while (tc.IsConnected && prompt.Trim() != "exit")
            {
                if (MODEL == "MXG") prompt = "OUTPut " + outp + ";\n";
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


    }
}
