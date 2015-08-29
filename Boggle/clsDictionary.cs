using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Boggle
{
    public class clsDictionary
    {
        private Dictionary<int, string> boggleDictionary;
        public Dictionary<string, string> validWords;
        public clsDictionary()
        {
            int key = 0;
            validWords = new Dictionary<string, string>();
            boggleDictionary = new Dictionary<int, string>();
            string file = System.Environment.CurrentDirectory + "\\words.txt";
            using (StreamReader reader = new StreamReader(file))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    key++;
                    boggleDictionary.Add(key, line);
                }
            }
            
            
        }

        public bool doesWordExist(string str)
        {
            KeyValuePair<int, string> tmpDict = boggleDictionary.Where(v => v.Value.StartsWith(str)).Take(1).SingleOrDefault();
            if (tmpDict.Value != null)
            
            {
                if (boggleDictionary.ContainsValue(str.ToUpper()))
                {
                    try
                    {
                        validWords.Add(str, str);
                    }
                    catch (System.ArgumentException)
                    {
                        Debug.Write("{0} already added. Repeatation not Allowed", str);
                    }
                }
                return true;
            }
            return false;
        }
      
    }
}
