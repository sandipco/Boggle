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
        public int totalPoints { get; set; }
        public clsDictionary()
        {
            int key = 0;
            totalPoints = 0;
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
                        int points = 0;
                        int len = 0;
                        if (str.Contains("Q"))
                            len = str.Length + 1;
                        else
                            len = str.Length;
                        switch (len)
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                                points = 1;
                                break;
                            case 5:
                                points = 2;
                                break;
                            case 6:
                                points = 3;
                                break;
                            case 7:
                                points = 5;
                                break;
                            default:
                                points = 11;
                                break;

                        }
                        this.totalPoints+=points;
                        validWords.Add(str, str + " Points " + points.ToString());
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
