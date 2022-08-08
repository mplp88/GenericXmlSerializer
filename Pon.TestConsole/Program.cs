using Pon.GenericXmlSerializer;
using Pon.Model;

List<SerializableObject> listForSerializing = new List<SerializableObject>();

AddItemsToList(listForSerializing);
Console.WriteLine(listForSerializing.ToXmlString());
Console.WriteLine();
Console.WriteLine(listForSerializing.ToXmlString(true));
Console.WriteLine();
Console.WriteLine(listForSerializing.ToXmlString(false));
//result.ToXmlFile("C:\\test.xml");
Console.ReadKey();

void AddItemsToList(List<SerializableObject> lista)
{
    for (int i = 0; i < 5; i++)
    {
        var o = new SerializableObject()
        {
            CUIT = 12345678901,
            cuentas = string.Format("12{0}", i),
            inscripciones = string.Format("54{0}", i),
            cajas = null
        };

        lista.Add(o);
    }
}