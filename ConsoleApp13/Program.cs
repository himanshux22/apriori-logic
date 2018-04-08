using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp13
{
    class Program
    {

        public string[]  SplitString(string s) {
            string[] values = s.Split(',');
            for (int i = 0; i<values.Length; i++)
            {
                values[i] = values[i].Trim();
            }
            return values;
        }


        public static void Main(string[] args)
        {
            //no dot and comma should be included in the keywords
    /*      Dictionary<string, string> dic = new Dictionary<string, string>
              { { "t1"  ,"12, Onion, Nintendo, Key-chain, Eggs, Yo-yo"},
            { "t2"  , " Doll, Onion, Nintendo, Key-chain, Eggs, Yo-yo"},
            { "t3"  ,"Mango, Apple, Key-chain, Eggs"},
            { "t4"  , "Mango, Umbrella, Corn, Key-chain, Yo-yo"},
            { "t5"  , "Corn, Onion, Onion, Key-chain, Ice-cream, Eggs"} }; */

   

               Dictionary<string, string> dic = new Dictionary<string, string>
             {
                 { "1", "1,2,3,4" },
                 { "2", "1,2,4" },
                 { "3", "1,2" },
                 { "4", "2,3,4" },
                   {"5","2,3" },
                   { "6","3,4" },
                   { "7","2,4"}


             };


            int support =43;
            int confidence = 80;
            Apriori(dic, support, confidence);
         
                Console.Read();

        }

        public static void Apriori(Dictionary<string, string> dic, int support, int confidence)
        {
            FindSingleSupportcount(support,dic);



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

     
        public static ArrayList FindSingleSupportcount(int support, Dictionary<string, string> dic)
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
            //adding the hashset value to dictionary with support count, list with greater support
            foreach (String item in unique)
            {
                //checking item greater than the support

                if (ItemCount(item, s1) >= supportInNumber)
                {
                    SupportOfSingle.Add(item, ItemCount(item, s1));
                }
            }

            //

            //printing the items
            Console.WriteLine("\nSingle item set");
            foreach (KeyValuePair<string, int> item in SupportOfSingle)
            {
                Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
            }
            //creating the itemset with two pair of items
            CreatePairItemset(SupportOfSingle, supportInNumber,dic);

            return a;
        }

        public static void CreatePairItemset(Dictionary<string, int> supportOfSingle,int supportInNumber, Dictionary<string, string> dic)
        {


            var temp = supportOfSingle;
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
            supportForpairs(twoElementsPair, supportInNumber,dic);

        }

        public static void supportForpairs(ArrayList twoElementsPair,int support, Dictionary<string, string> dic)
        {
            
            var SupportOfPair = new Dictionary<string, int>();
            //adding the elements to dictionary
            foreach (string item in twoElementsPair)
            {
                if (ItemCountPair(item, dic) >= support)
                {
                    SupportOfPair.Add(item, ItemCountPair(item, dic));
                }
            }



            //printing the pairs
            Console.WriteLine("\nDouble item set");
            foreach (KeyValuePair<string, int> item in SupportOfPair)
            {
                Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
            }

            //triple set creation
            CreateTripleItemset(SupportOfPair,dic,support);
        }

        public static int ItemCountPair(string item, Dictionary<string, string> dic)
        {
            string[] ret = item.ToString().Split('.');
            int count = 0;


            Program p = new Program();
            ArrayList a = new ArrayList();//collection of dublicate and other elements
         

            foreach (string ui in dic.Values)
            {
                a.Clear();
                a.AddRange(p.SplitString(ui));
                if (a.Contains(ret[0]) && a.Contains(ret[1]))
                {
                    count++;
                }

            }


            //checking the pair is present in the dictionary(main data)

           

            
            return count;
        }



        public static void CreateTripleItemset(Dictionary<string, int> SupportOfPair, Dictionary<string, string> dic,int support)
        {


            var temp = SupportOfPair;
            //copying the unique element from second result 
            //copying it to arraylist to sort it out
            ArrayList s1 = new ArrayList();
            ArrayList fourElementsPair = new ArrayList();

            foreach (var item in temp)
            {
                s1.Add(item.Key);
            }

            s1.Sort();

            for (int i = 0; i < s1.Count; i++)
            {

                for (int j = i + 1; j < s1.Count; j++)
                {

                    fourElementsPair.Add(s1[i] + "." + s1[j]);
                }
            }

            //create three pair
            HashSet<string> threeElementsPair = new HashSet<string>();

            CreateThreePair(fourElementsPair,  out threeElementsPair, dic);

            var SupportOfTriple = new Dictionary<string, int>();


            //adding the elements to dictionary
            foreach (string item in threeElementsPair)
            {
                if (ItemCountTriple(item, dic) >= support)
                {
                    SupportOfTriple.Add(item, ItemCountTriple(item, dic));
                }

            }

            //print list of triple
            Console.WriteLine("\nTriple item set");

            foreach (KeyValuePair<string, int> item in SupportOfTriple)
            {
                Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
            }


        }

        public static int ItemCountTriple(string item, Dictionary<string, string> dic)
        {

            string[] ret = item.ToString().Split('.');
            int count = 0;

            //checking the triplet is present in the dictionary(main data)
            Program p = new Program();
            ArrayList a = new ArrayList();//collection of dublicate and other elements


            foreach (string ui in dic.Values)
            {
                a.Clear();
                a.AddRange(p.SplitString(ui));
                if (a.Contains(ret[0]) && a.Contains(ret[1])&& a.Contains(ret[2]))
                {
                    count++;
                }

            }
            return count;
        }

        public static void CreateThreePair(ArrayList fourElementsPair,  out HashSet<string> threeElementsPair, Dictionary<string, string> dic)
        {
            string[] ret= { };
           
            ArrayList ret1=new ArrayList();
            //var SupportOfTriple = new Dictionary<string, int>();

            //converting four to three
            foreach (string fourVal in fourElementsPair) {
                
                ret= fourVal.ToString().Split('.');
                ret1.AddRange(CheckDublicate(ret));

            }
          

            HashSet<string> lastResult = new HashSet<string>();

            foreach (string x in ret1)
            {
                lastResult.Add(x);
            }
           

          
            threeElementsPair = lastResult;
        }


        public static ArrayList CheckDublicate(string[] ret)
        {
            ArrayList Temp=new ArrayList();
            ArrayList result = new ArrayList();
          
            string valThree = "";
            foreach (var res in ret.Distinct()) {

                Temp.Add(res);
            

            }
            //sorting the single elements
            Temp.Sort();
          
            foreach (string res in Temp)
            {

                if (valThree != "")
                {
                    valThree = valThree + "." + res;
                }
                else
                    valThree = res;
            }

            //counting the dots
            int count = valThree.Count(x => x == '.');

            if (count==2)
            {
                result.Add(valThree);
            }
           

            return result;
        }

        public static void FindConfidence()
        {


        }
    }


}
