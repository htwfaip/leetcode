using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeSolution.HardProblems
{
    public class RegularExpressionMatch
    {
        public bool IsMatch(string s, string p)
        {
            /*
             * Dynamic Programming Approach
             * M[i, j] = IsMatch for S[0,i] and P[0,j]
             * 
             * M[i,j] 
             *  If P[j] != "*"
             *    M[i,j] = (S[i] == P[j] || P[j] == '.') && M[i-1,j-1]
             *  
             *  If P[j] == "*"
             *    If (P[j-1] == S[i] || P[j-1] == '.')
             *      M[i,j] = M[i-1,j-2] || M[i-1,j]
             *    else
             *      M[i,j] = M[i,j-2]
             */

            int pLength = p.Length;
            int sLength = s.Length;

            // By default bool array is false             
            bool[,] match = new bool[pLength+1, sLength+1];

            match[0, 0] = true;
            
            //for (int i = 1; i <= s.Length; i++)
                //match[0, i] = false;
            
            for (int i = 2; i <= pLength; i = i +2)
            {
                match[i, 0] = match[i - 2, 0] && p[i-1] == '*';
            }

            for (int j = 1; j <= sLength; j++)
            {
                for (int i = 1; i <= pLength; i++)
                {
                    if (p[i-1] != '*')
                    {
                        match[i, j] = (p[i - 1] == s[j - 1] || p[i - 1] == '.') && match[i - 1, j - 1];
                    }
                    else
                    {
                        match[i, j] = (p[i - 2] == s[j - 1] || p[i - 2] == '.') ? match[i , j - 1] || match[i - 2, j ] : match[i - 2, j];
                    }
                }
            }

            return match[pLength, sLength];
        }

        public static void Test()
        {
            var rem = new RegularExpressionMatch();

            Console.WriteLine(rem.IsMatch("aa", "a"));
            Console.WriteLine(rem.IsMatch("aa", "aa"));
            Console.WriteLine(rem.IsMatch("aaa", "aa"));
            Console.WriteLine(rem.IsMatch("aa", "a*"));
            Console.WriteLine(rem.IsMatch("aa", ".*"));
            Console.WriteLine(rem.IsMatch("aab", "c*a*b"));
            Console.WriteLine(rem.IsMatch("a", "ab*"));
            Console.WriteLine(rem.IsMatch("bbbba", ".*a*a"));
        }
    }
}
