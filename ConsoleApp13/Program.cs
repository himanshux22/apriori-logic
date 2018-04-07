using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp13
{
    class Program
    {

        static Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "1", "m, o, n, k, e, y" },
                { "2", "d, o, n, k, e, y" },
                { "3", "m, a, k, e" },
                { "4", "m, u, c, k, y" },
                { "5", "c, o,o, k, e" }
            };

        public string[]  SplitString(string s) {
            string[] values = s.Split(',');
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();
            }
            return values;
        }


        public static void Main(string[] args)
        {

            

            
     
            int support =60;
            int confidence = 80;
            Apriori(dic, support, confidence);
         
                Console.Read();

        }

        public static void Apriori(Dictionary<string, string> dic, int support, int confidence)
        {
            FindSingleSupportcount(support);



        }

        public static int ItemCount(string item, ArrayList s1)
        {
            int count = 0;

            foreach (string s in s1)
            {
                if (s == item) {
                    count++;
                }
            }
            return count;

        }

     
        public static ArrayList FindSingleSupportcount(int support)
        {
            Program p = new Program();
            ArrayList a= new ArrayList();//collection of dublicate and other elements
            //convert support to number from percentage
            int supportInNumber = support * dic.Count / 100;

            foreach (KeyValuePair<string, string>item in dic)
            {
                a.AddRange(p.SplitString(item.Value));
            }

            HashSet<String> unique = new HashSet<string>();

            ArrayList s1 = a;



            //adding arraylist to hashset to make unique items
            foreach (String item in s1)
            {
                unique.Add(item);
            }
            //single support list
            var SupportOfSingle = new Dictionary<string, int>();
            //adding the hashset value to dictionary with support count
            foreach (String item in unique)
            {
                SupportOfSingle.Add(item, ItemCount(item, s1));
            }

            //list with greater support
            var SupportOfSingle1 = new Dictionary<string, int>();
            //checking item greater than the support
            foreach (var e in SupportOfSingle)
            {
                if (e.Value >= supportInNumber)
                {
                    SupportOfSingle1.Add(e.Key, e.Value);
                }
            }
            //printing the items
            foreach (KeyValuePair<string, int> item in SupportOfSingle1)
            {
                Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
            }
            //creating the itemset with two pair of items
            CreateItemset(SupportOfSingle1);

            return a;
        }

        public static void CreateItemset(Dictionary<string, int> supportOfSingle1)
        {


            var temp = supportOfSingle1;
            //copying the unique element from first result 
            //copying it to arraylist to sort it out
            ArrayList s1 = new ArrayList();
            ArrayList twoElementsPair = new ArrayList();

            foreach (var item in temp)
            {
                s1.Add(item.Key);
            }

            s1.Sort();

            for (int i = 0; i < s1.Count ; i++)
            {

                for (int j= i+1; j < s1.Count; j++)
                {

                    twoElementsPair.Add(s1[i] + "." + s1[j]);
            } }
            /*print list of pairs
                foreach (var item in twoElementsPair)
            {
                Console.WriteLine(item);
            }
            */
            supportForpairs(twoElementsPair);

        }

        public static void supportForpairs(ArrayList twoElementsPair)
        {
            
            var SupportOfPair = new Dictionary<string, int>();
            foreach (string item in twoElementsPair)
            {
                SupportOfPair.Add(item, ItemCountPair(item));

            }

        //printing the pairs
            
            foreach (KeyValuePair<string, int> item in SupportOfPair)
            {
                Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
            }

        }

        public static int ItemCountPair(string item)
        {
            string[] ret = item.ToString().Split('.');
            int count = 0;
            //Console.WriteLine(ret[0]);

            foreach (string items in dic.Values)
                {
                    if (items.Contains(ret[0])&& items.Contains(ret[1]))
                    {
                     count++;
                    }
            }

            
            return count;
        }
    }
}
