using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

namespace GenericXmlSerializer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ObjetoSerializable> listaParaSerializar = new List<ObjetoSerializable>();

            AgregarItemsALista(listaParaSerializar);
            var result = listaParaSerializar.SerializeToXml();

            using(var stringWriter = new StringWriter())
            {
                using(var xmlTextWriter = XmlWriter.Create(stringWriter))
                {
                    result.WriteTo(xmlTextWriter);
                    xmlTextWriter.Flush();
                    Console.WriteLine(stringWriter.GetStringBuilder().ToString());
                }
            }

            Console.ReadKey();
        }

        private static void AgregarItemsALista(List<ObjetoSerializable> lista)
        {
            for(int i = 0; i < 5; i++)
            {
                ObjetoSerializable o = new ObjetoSerializable()
                {
                    CUIT = 12345678901,
                    cuentas = string.Format("12{0}", i),
                    inscripciones = string.Format("54{0}", i),
                    cajas = null
                };

                lista.Add(o);
            }
        }
    }
}
