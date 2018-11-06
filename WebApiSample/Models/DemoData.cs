using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSample.Models
{
    public class DemoData
    {
        public static readonly List<List<string>> Contacts = new List<List<string>>();
        public static readonly List<string> File1 = new List<string>
        {
            "f_1_test_1@example.com",
            "f_1_test_2@example.com",
            "f_1_test_3@example.com",
            "f_1_test_4@example.com",
            "f_1_test_5@example.com"
        };
        public static readonly List<string> File2 = new List<string>
        {
            "f_2_test_1@example.com",
            "f_2_test_2@example.com",
            "f_2_test_3@example.com",
            "f_2_test_4@example.com",
            "f_2_test_5@example.com"
        };
        public static readonly List<string> File3 = new List<string>
        {
            "f_3_test_1@example.com",
            "f_3_test_2@example.com",
            "f_3_test_3@example.com",
            "f_3_test_4@example.com",
            "f_3_test_5@example.com"
        };

        public static List<List<string>> GetMultiple
        {
            get
            {
                if (Contacts.Count <= 0)
                {
                    Contacts.Add(File1);
                    Contacts.Add(File2);
                    Contacts.Add(File3);
                }
                return Contacts;
            }
        }
    }
}