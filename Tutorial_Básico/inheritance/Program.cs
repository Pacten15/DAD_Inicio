using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;


namespace inheritance
{
    public class Person
    {
        private string name;
        public Person(string name)
        {
            this.name = name;
        }
        public virtual void ShowInfo()
        {
            Console.WriteLine("Name:{0}", name);
        }
    }

    public class Employee : Person
    {
        private string company;
        public Employee(string name, string company) : base(name)
        {
            this.company = company;
        }
        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine("Company: {0}", company);
        }
    }

    public class Test
    {
        public static void Main(string[] args)
        {
            Person pessoa = new Employee("John", "CBA");
            Employee empr = new Employee("Lou", "NMN");
            pessoa.ShowInfo();
            empr.ShowInfo();    
        }
    }
    
}

