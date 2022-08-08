using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Pon.GenericXmlSerializer
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
            var document = new XmlDocument();
            var ser = new XmlSerializer(objectToSerialize.GetType());

            try
            {
                using (var ms = new MemoryStream())
                {
                    ser.Serialize(ms, objectToSerialize);
                    ms.Position = 0;
                    document.Load(ms);
                }

                return document;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Extension method that serializes generic object to Xml
        /// </summary>
        /// <typeparam name="T">Generic type of the object</typeparam>
        /// <param name="objectToSerialize">The object to serialize</param>
        /// <returns>XML as string</returns>
        public static string ToXmlString<T>(this T objectToSerialize)
        {
            string res = string.Empty;
            var document = SerializeToXml(objectToSerialize);

            using (var stringWriter = new StringWriter())
            {
                using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                {
                    document.WriteTo(xmlTextWriter);
                    xmlTextWriter.Flush();
                    res = stringWriter.GetStringBuilder().ToString();
                }
            }

            return res;
        }

        /// <summary>
        /// Extension method that serializes generic object to Xml
        /// </summary>
        /// <typeparam name="T">Generic type of the object</typeparam>
        /// <param name="objectToSerialize">The object to serialize</param>
        /// <param name="indent">bool to determine if the XML should be indented</param>
        /// <returns>XML as string indented</returns>
        public static string ToXmlString<T>(this T objectToSerialize, bool indent = false)
        {
            string res = string.Empty;
            var document = SerializeToXml(objectToSerialize);

            using (var stringWriter = new StringWriter())
            {
                using (var xmlTextWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = indent }))
                {
                    document.WriteTo(xmlTextWriter);
                    xmlTextWriter.Flush();
                    res = stringWriter.GetStringBuilder().ToString();
                }
            }

            return res;
        }

        /// <summary>
        /// Extension method that serializes generic object to XML and outputs to a file
        /// </summary>
        /// <typeparam name="T">Generic type of the object</typeparam>
        /// <param name="objectToSerialize">The object to serialize</param>
        /// <param name="OutputPath">The path where the file would be placed</param>
        public static void ToXmlFile<T>(this T objectToSerialize, string OutputPath)
        {
            var document = SerializeToXml(objectToSerialize);
            var output = OutputPath.Split('.');

            if (output[output.Length - 1] != "xml")
            {
                OutputPath += ".xml";
            }

            var fs = File.Create(OutputPath);
            using (var stringWriter = new StringWriter())
            {
                using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                {
                    document.WriteTo(xmlTextWriter);
                    xmlTextWriter.Flush();
                    var s = stringWriter.GetStringBuilder().ToString();
                    fs.Write(System.Text.Encoding.ASCII.GetBytes(s), 0, s.Length);
                }
            }
        }
    }
}
