using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DecoderServer.Interfaces;
using DecoderServer.Model.utils;

namespace DecoderServer.Model
{
    class DecoderHandler: IDecoderHandler
    {
        private string InputString { get; set; }

        public DecoderHandler(string inputString)
        {
            InputString = inputString;
        }

        public string EncrypteString()
        {
            char[] inputStringToChar = InputString.ToCharArray();
            using (var context = new MyContext())
            {
                for (int i = 0; i < inputStringToChar.Length; i++)
                {
                    
                    var tmp = inputStringToChar[i].ToString();
                    inputStringToChar[i] = Convert.ToChar(context.Ciphers.Where(x => x.From == tmp).Select(x => x.To).FirstOrDefault());
                    
                } 
              
            }
           
           return new string(inputStringToChar);
        }

        public string DecrypteString()
        {
            char[] inputStringToChar = InputString.ToCharArray();
            using (var context = new MyContext())
            {
                for (int i = 0; i < inputStringToChar.Length; i++)
                {
                    var tmp = inputStringToChar[i].ToString();
                    inputStringToChar[i] = Convert.ToChar(context.Ciphers.Where(x => x.To == tmp).Select(x => x.From).FirstOrDefault());

                }

            }

            return new string(inputStringToChar);
        }
    }
}
