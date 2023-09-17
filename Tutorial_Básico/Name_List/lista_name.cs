using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Name_List
{
    interface IListaNomes<T>
    {
        void add_name(string name);
        string return_all_names();
        void erase_all_names();

    }
    public class lista_name : IListaNomes<lista_name>
    {

        ArrayList list;

        public lista_name(ArrayList lista)
        {
            this.list = lista;
        }

        public ArrayList get_lista_name()
        {
            return list;
        }

        public void add_name(string name)
        {
            list.Add(name);
        }

        public string return_all_names()
        {
            string accum = "";
            for (int i = 0; i < list.Count; i++)
            {
                accum += " " + list[i];
            }
            return accum;
        }

        public void erase_all_names()
        {
            list.Clear();
        }

    }
}
