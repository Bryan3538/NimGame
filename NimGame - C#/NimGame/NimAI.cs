using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimGame
{
    public class NimAi
    {
        public NimAi() { }

        public void GetMove(Pile[] piles, out int pileIndex, out int sticksToRemove)
        {
            pileIndex = FindIndexOfMax(piles);
            sticksToRemove = piles[pileIndex].Count - Xor(piles[pileIndex].Count, NimSum(piles));

            if (sticksToRemove == 0)
                sticksToRemove = 1;
        }

        private int FindIndexOfMax(Pile[] piles)
        {
            int max = -1;
            int maxIndex = 0;

            for (int i = 0; i < piles.Length; i++)
            {
                if (piles[i].Count > max)
                {
                    max = piles[i].Count;
                    maxIndex = i;
                }
            }

            return maxIndex;
        }

        private int NimSum(Pile[] piles)
        {
            int nimsum = 0;

            foreach (Pile pile in piles)
            {
                nimsum = Xor(nimsum, pile.Count);
            }

            return nimsum;
        }

        private int Xor(int numOne, int numTwo)
        {
            int[] binaryIntOne = BinaryConverter.IntegerToBinary(numOne);
            int[] binaryIntTwo = BinaryConverter.IntegerToBinary(numTwo);
            int length = Math.Min(binaryIntOne.Length, binaryIntTwo.Length);
            int maxlength = Math.Max(binaryIntOne.Length, binaryIntTwo.Length);
            int[] result = new int[maxlength];

            for(int i = 0; i < length; i++)
            {
                //if they are both 0 or both 1, result is 0
                if((binaryIntOne[i] == 1 && binaryIntTwo[i] == 1) || 
                   (binaryIntOne[i] == 0 && binaryIntTwo[i] == 0))
                {
                    result[i] = 0;
                }
                else //One bit is 1, the other is 0
                {
                    result[i] = 1;
                }
            }

            if(length < maxlength)
            {
                int[] longest;
                if (binaryIntOne.Length > binaryIntTwo.Length)
                    longest = binaryIntOne;
                else
                    longest = binaryIntTwo;

                for (int i = length; i < maxlength; i++)
                    result[i] = longest[i];
            }

            return BinaryConverter.BinaryToInteger(result);
        }
    }
}
