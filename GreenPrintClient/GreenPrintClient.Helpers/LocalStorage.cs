using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GreenPrintClient.Helpers
{
    public class LocalStorage
    {
        List<String> phoneList;
        List<String> emailList;
        string appPath;

        public LocalStorage()
        {
            phoneList = new List<string>();
            emailList = new List<string>();

            appPath = AppDomain.CurrentDomain.BaseDirectory;
        }

        public List<string> AddPhoneNumber(string phoneNumber)
        {
            if (phoneList == null)
                phoneList = new List<string>();

            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentException("Empty or invalid phone number", nameof(phoneNumber));

            if (phoneList.Contains(phoneNumber) == false)
            {
                byte[] dataAsBytes = null;
                try
                {
                    phoneList.Add(phoneNumber);

                    string dat = JsonConvert.SerializeObject(phoneList);

                    dataAsBytes = System.Text.Encoding.UTF8.GetBytes(dat);
                    if (dataAsBytes == null || dataAsBytes.Length < 1)
                        return phoneList;
                }
                catch (Exception Ex)
                {
                    Ex.Data.Add("UIMessage", "Could not save cached phone number list");
                    throw;
                }

                try
                {
                    File.WriteAllBytes($"{appPath}\\pn.dat", dataAsBytes);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return phoneList;
        }

        public List<string> LoadPhoneNumbers()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            try
            {
                if (File.Exists($"{path}\\pn.dat"))
                {
                    byte[] dat = File.ReadAllBytes($"{path}\\pn.dat");
                    if (dat != null && dat.Length > 0)
                    {
                        string phones = System.Text.Encoding.UTF8.GetString(dat);

                        phoneList = JsonConvert.DeserializeObject<List<String>>(phones);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }

            return phoneList;
        }

        public List<string> LocalEMailAddresses()
        {
            return null;
        }
    }
}
