using Caliburn.Micro;
using CustomersLibrary.Models;
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Customers.ViewModels
{
    public class MainViewModel : PropertyChangedBase
    {
        private BindableCollection<CustomerModel> _customers = new BindableCollection<CustomerModel>();
        public BindableCollection<CustomerModel> Customers
        {
            get
            {
                return _customers;
            }
            set
            {
                _customers = value;
                NotifyOfPropertyChange(() => Customers);
            }
        }
        readonly XmlSerializer serializer = new XmlSerializer(typeof(BindableCollection<CustomerModel>));
        
        public MainViewModel()
        {
            ReadDataXML();
        }


        public void SaveToXML()
        {
            var userPath = @"%USERPROFILE%\CustomerData.xml";
            var filePath = Environment.ExpandEnvironmentVariables(userPath);
            File.Delete(filePath);
            using (FileStream stream = File.OpenWrite(filePath))
            {
                serializer.Serialize(stream, Customers);
            }
        }


        public void CancelData()
        {
            Customers.Clear();
            ReadDataXML();
        }


        public void ReadDataXML()
        {
            var userPath = @"%USERPROFILE%\CustomerData.xml";
            var filePath = Environment.ExpandEnvironmentVariables(userPath);
            if (File.Exists(filePath))
            {
                using (FileStream streamRead = File.OpenRead(filePath))
                {
                    Customers = (BindableCollection<CustomerModel>)serializer.Deserialize(streamRead);
                }
            }
        }

    }
}
