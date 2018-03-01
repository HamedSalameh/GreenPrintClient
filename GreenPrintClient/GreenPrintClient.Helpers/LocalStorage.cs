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

        string[] dataFileNames = { "pn.dat", "ea.dat" };
        public enum dataType
        {
            PhoneNumbers,

            EmailAddresses
        }

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
                    File.WriteAllBytes($"{appPath}\\{dataFileNames[(int)dataType.PhoneNumbers]}", dataAsBytes);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return phoneList;
        }

        public List<string> AddEmailAddress(string emailAddress)
        {
            if (emailList == null)
                emailList = new List<string>();

            if (string.IsNullOrEmpty(emailAddress))
                throw new ArgumentException("Empty or invalid email address", nameof(emailAddress));

            if (emailList.Contains(emailAddress) == false)
            {
                byte[] dataAsBytes = null;
                try
                {
                    emailList.Add(emailAddress);

                    string dat = JsonConvert.SerializeObject(emailList);

                    dataAsBytes = System.Text.Encoding.UTF8.GetBytes(dat);
                    if (dataAsBytes == null || dataAsBytes.Length < 1)
                        return emailList;
                }
                catch (Exception Ex)
                {
                    Ex.Data.Add("UIMessage", "Could not save cached phone number list");
                    throw;
                }

                try
                {
                    File.WriteAllBytes($"{appPath}\\{dataFileNames[(int)dataType.EmailAddresses]}", dataAsBytes);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return emailList;
        }

        private List<string> LoadData(dataType dataType)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = $"{path}\\{dataFileNames[(int)dataType]}";

            try
            {
                if (File.Exists(fileName))
                {
                    byte[] dat = File.ReadAllBytes(fileName);
                    if (dat != null && dat.Length > 0)
                    {
                        string list = System.Text.Encoding.UTF8.GetString(dat);

                        if (dataType == dataType.EmailAddresses)
                        {
                            emailList = JsonConvert.DeserializeObject<List<String>>(list);
                            return emailList;
                        }
                        else
                        {
                            phoneList = JsonConvert.DeserializeObject<List<String>>(list);
                            return phoneList;
                        }
                        
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }

            return null;
        }

        public List<string> LoadPhoneNumbers()
        {
            return LoadData(dataType.PhoneNumbers);
        }

        public List<string> LocalEMailAddresses()
        {
            return LoadData(dataType.EmailAddresses);
        }
    }
}
