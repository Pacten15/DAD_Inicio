using System;

// try adding/removing the "virtual" keyword before 
// the "class" keyword and observe the result
public class SuperClass
{
    public void printName()
    {
        Console.WriteLine("SuperClass");
    }
}

public class SubClass : SuperClass
{

    // try using the "new", "override" keywords or nothing before 
    // the "void" keyword and observe the result
    public new void printName()
    {
        Console.WriteLine("SubClass");
    }
}

public class Test
{
    public static void Main(string[] args)
    {
        SuperClass superClass = new SubClass();
        SubClass subClass = new SubClass();
        Console.Write("Super class reference with subclass instance prints:");
        superClass.printName();
        Console.Write("Subclass reference with subclass instance prints:");
        subClass.printName();
    }
}

