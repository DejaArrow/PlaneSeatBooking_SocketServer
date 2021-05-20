using System.IO;
using System.Xml.Serialization;

namespace Shared
{
    public class DataItem : ISerialisableData
    {
        public string Id { get; set; }
        //Private constructor to allow for serialisation
        private DataItem() { }
        public DataItem(string data)
        {
            this.Id = data;
        }
    }
    /// <summary>
    /// DataItem serialisation.
    /// </summary>
    public static class DataItemSerialisation
    {
        /// <summary>
        /// Gets the serialised DataItem.
        /// </summary>
        /// <returns>The serialised ISerialisableData string.</returns>
        /// <param name="dataItem">Data item.</param>
        public static string GetSerialisedDataItem(DataItem dataItem)
        {
            XmlSerializer serialiser = new XmlSerializer(dataItem.GetType());
            using (StringWriter sw = new StringWriter())
            {
                serialiser.Serialize(sw, dataItem);
                return sw.ToString();
            }
        }

        /// <summary>
        /// Gets the deserialised DataItem.
        /// </summary>
        /// <returns>The DataItem.</returns>
        /// <param name="serialisedData">Serialised DataItem.</param>
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

