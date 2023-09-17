using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace Name_List
{
    public class program
    {

        static void Main(string[] args)
        {

            ArrayList Lista = new ArrayList();

            lista_name nomes = new lista_name(Lista);

            nomes.add_name("John");
            nomes.add_name("NONO");
            nomes.add_name("albedo");

            Console.WriteLine(nomes.return_all_names());
            nomes.erase_all_names();
            Console.WriteLine("Lista Vazia: " + nomes.get_lista_name().Count);
        }

    }
}
