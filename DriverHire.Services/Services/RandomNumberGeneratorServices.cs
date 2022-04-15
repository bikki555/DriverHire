using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Services.Services
{
    public interface IRandomNumberGeneratorServices
    {
        string GetRandomNumber(int length);
    }
    public class RandomNumberGeneratorServices: IRandomNumberGeneratorServices
    {
        public string GetRandomNumber(int length)
        {
            var random = new Random();
            const string allowedChars = "0123456789";
            char[] chars = new char[length];
            var result = string.Empty;

            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[random.Next(allowedChars.Length)];
            }
            result = new string(chars);

            return result;
        }
    }
}
