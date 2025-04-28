using Pon.GenericXmlSerializer.Exceptions;
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
        public static XmlDocument ToXmlDocument<T>(this T objectToSerialize)
        {
            if (objectToSerialize is null)
            {
                throw new ArgumentNullException(nameof(objectToSerialize));
            }

            XmlDocument document = new();
            XmlSerializer serializer = new(objectToSerialize.GetType());

            try
            {
                using MemoryStream stream = new();
                serializer.Serialize(stream, objectToSerialize);
                stream.Position = 0;
                document.Load(stream);

                return document;
            }
            catch (Exception e)
            {
                throw new SerializingException("Error serializing object. See inner exception." , e);
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
            return objectToSerialize.ToXmlString(false);
        }

        /// <summary>
        /// Extension method that serializes generic object to Xml
        /// </summary>
        /// <typeparam name="T">Generic type of the object</typeparam>
        /// <param name="objectToSerialize">The object to serialize</param>
        /// <param name="indent">bool to determine if the XML should be indented</param>
        /// <returns>XML as string indented</returns>
        public static string ToXmlString<T>(this T objectToSerialize, bool indent)
        {
            XmlDocument document = objectToSerialize.ToXmlDocument();

            using StringWriter stringWriter = new();
            using XmlWriter? xmlTextWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = indent });
            document.WriteTo(xmlTextWriter);
            xmlTextWriter.Flush();
            string result = stringWriter.GetStringBuilder().ToString();

            return result;
        }

        /// <summary>
        /// Extension method that serializes generic object to XML and outputs to a file
        /// </summary>
        /// <typeparam name="T">Generic type of the object</typeparam>
        /// <param name="objectToSerialize">The object to serialize</param>
        /// <param name="outputPath">The path where the file would be placed</param>
        public static string ToXmlFile<T>(this T objectToSerialize, string outputPath)
        {
            try
            {
                XmlDocument document = ToXmlDocument(objectToSerialize);
                string[] output = outputPath.Split('.');

                if (output[^1] != "xml")
                {
                    outputPath += ".xml";
                }

                FileStream fs = File.Create(outputPath);
                using StringWriter stringWriter = new();
                using XmlWriter? xmlTextWriter = XmlWriter.Create(stringWriter);
                document.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                string result = stringWriter.GetStringBuilder().ToString();
                fs.Write(System.Text.Encoding.ASCII.GetBytes(result), 0, result.Length);
                return $"File saved successfully to path: {outputPath}";
            }
            catch(SerializingException)
            {
                throw;
            }
            catch(Exception e)
            {
                throw new Exception("Error saving file. See inner exception", e);
            }
        }
    }
}
