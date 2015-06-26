using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimGame
{
    public class Pile
    {
        private int count;

        public Pile()
        {
            count = 0;
        }

        public Pile(int count)
        {
            this.Count = count;
        }

        public int Count
        {
            get { return count; }
            set
            {
                if (value >= 0)
                    count = value;
                else
                    throw new ArgumentException(
                        String.Format("Count must be positive and less than {0}", int.MaxValue));
            }
        }

        public int[] Binary
        {
            get { return BinaryConverter.IntegerToBinary(count); }
        }

        public override string ToString()
        {
            int[] binary = Binary;
            int positionOfFirstOne = -1;
            StringBuilder builder = new StringBuilder();
            string output;

            //find the first 1 so as to achieve minimum length
            for (int i = 0; i < binary.Length; i++)
            {
                if (binary[i] == 1)
                {
                    positionOfFirstOne = i;
                    break;
                }
            }

            //place the bits in a string, insuring there's at least a 0 there
            if (positionOfFirstOne >= 0)
            {
                for (int i = positionOfFirstOne; i < binary.Length; i++)
                {
                    builder.Append(binary[i]);
                }
            }
            else
                builder.Append(0);

            //insert spaces for readability
            for (int i = builder.Length - 4; i > 0; i -= 4)
                builder.Insert(i, " ");


                return builder.ToString();
        }
    }
}
