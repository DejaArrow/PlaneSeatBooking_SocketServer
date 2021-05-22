using System.IO;
using System.Xml.Serialization;

namespace Shared
{
    public class DataItem : ISerialisableData
    {
        public string package { get; set; }
        //Private constructor to allow for serialisation
        private DataItem() { }
        public DataItem(string data)
        {
            this.package = data;
        }
    }
   
    // DataItem serialisation.
    
    public static class DataItemSerialisation
    {
       
        // Gets the serialised DataItem.
      
        public static string GetSerialisedDataItem(DataItem dataItem)
        {
            XmlSerializer serialiser = new XmlSerializer(dataItem.GetType());
            using (StringWriter sw = new StringWriter())
            {
                serialiser.Serialize(sw, dataItem);
                return sw.ToString();
            }
        }

        
        // Gets the deserialised DataItem.
        
        public static DataItem GetDataItem(string serialisedData)
        {
            XmlSerializer deserialiser = new XmlSerializer(typeof(DataItem));
            using (TextReader tr = new StringReader(serialisedData))
            {
                return (DataItem)deserialiser.Deserialize(tr);
            }
        }
    }
}

