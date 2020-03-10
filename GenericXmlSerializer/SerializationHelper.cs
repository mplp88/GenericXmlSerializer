using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace GenericXmlSerializer
{
    public static class SerializationHelper
    {
        /// <summary>
        /// Extension method that serializes generic object to Xml
        /// </summary>
        /// <typeparam name="T">Generic type of the object</typeparam>
        /// <param name="objectToSerialize">The object to serialize</param>
        /// <returns>returns XmlDocument of the object</returns>
        public static XmlDocument SerializeToXml<T>(this T objectToSerialize)
        {
            var result = new XmlDocument();
            var ser = new XmlSerializer(objectToSerialize.GetType());
            
            try
            {
                using(var ms = new MemoryStream())
                {
                    ser.Serialize(ms, objectToSerialize);
                    ms.Position = 0;
                    result.Load(ms);
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
