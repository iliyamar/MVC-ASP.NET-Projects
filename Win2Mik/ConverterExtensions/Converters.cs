using System;
using System.Collections.Generic;
using System.Text;

namespace ConverterExtensions
{
    public static class Converters
    {
        public static string W2Mik(string line, Encoding enc)
        {

            var inputInBytes = enc.GetBytes(line);

            for (int i = 0; i < inputInBytes.Length; i++)
            {
                if (inputInBytes[i] >= 192 && inputInBytes[i] <= 255)
                {
                    inputInBytes[i] -= 64;
                }
            }

            return enc.GetString(inputInBytes);
        }


    }
}
