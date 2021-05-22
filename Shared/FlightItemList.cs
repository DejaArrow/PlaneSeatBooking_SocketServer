using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Shared
{
    public class FlightItemList
    {
       
        public List<string> FlightIDs = new List<string>();

        //Private constructor to allow for serialisation
        private FlightItemList() { }
        public FlightItemList(List<string> flightIDs)
        {
            this.FlightIDs = flightIDs;
        }
    }
    
    // FlightItemList serialisation.
   
    public static class FlightItemListSerialisation
    {
        
        // Gets the serialised FlightItemList.
      
        public static string GetSerialisedDataItem(FlightItemList flightItemList)
        {
            XmlSerializer serialiser = new XmlSerializer(flightItemList.GetType());
            using (StringWriter sw = new StringWriter())
            {
                serialiser.Serialize(sw, flightItemList);
                return sw.ToString();
            }
        }

        
        // Gets the deserialised FlightItemList.
   
        public static FlightItemList GetDataItem(string serialisedData)
        {
            XmlSerializer deserialiser = new XmlSerializer(typeof(FlightItemList));
            using (TextReader tr = new StringReader(serialisedData))
            {
                return (FlightItemList)deserialiser.Deserialize(tr);
            }
        }
    }
}


