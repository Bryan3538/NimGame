using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimGame
{
    public class BinaryConverter
    {
        private BinaryConverter() { }

        public static int[] IntegerToBinary(int n)
        {
            int size = 32;
            int[] bits = new int[size];

            for (int i = 0; i < bits.Length; i++)
            {
                if (n >= Math.Pow(2, size - 1 - i))
                {
                    bits[i] = 1;
                    n -= (int)Math.Pow(2, size - 1 - i);
                }
                else
                    bits[i] = 0;
            }

            return bits;
        }

        public static int BinaryToInteger(int[] bits)
        {
            int sum = 0;

            for (int i = 0; i < bits.Length; i++)
            {
                if (bits[i] == 1)
                    sum += (int)Math.Pow(2, bits.Length - i - 1);
            }

            return sum;
        }
    }
}
