using System;

namespace AddressBook_ADO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            AddressBookRepo addressBookRepo = new AddressBookRepo();
            // addressBookRepo.AlterTable();
            addressBookRepo.InsertIntoTablesForTRQuery();
        }
    }
}
