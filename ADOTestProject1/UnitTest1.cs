using AddressBook_ADO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ADOTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        AddressBookRepo addressBookRepo;
        [TestInitialize]
        public void SetUp()
        {
            addressBookRepo = new AddressBookRepo();
        }
        //Get Contact Count
        [TestMethod]
        public void TestMethodGetContactCount()
        {
            int actual = addressBookRepo.GetContactDetails();
            int expected = 6;
            Assert.AreEqual(expected, actual);
        }
        //Insert into table
        [TestMethod]
        public void TestMethodInsertIntoTable()
        {
            AddressBook addressBook = new AddressBook();
            addressBook.FirstName = "Mohd";
            addressBook.LastName = "Aadil";
            addressBook.Address = "Hello Street";
            addressBook.City = "Chennai";
            addressBook.State = "TamilNadu";
            addressBook.ZipCode = "7874152";
            addressBook.PhoneNumber = "7412587845";
            addressBook.email = "abcd@gmail.com";
            addressBook.addressBookName = "Collegue";
            addressBook.addressBookType = "Friends";
            int actual = addressBookRepo.InsertIntoTable(addressBook);
            int expected = 1;
            Assert.AreEqual(expected, actual);
        }
        //Modify data
        [TestMethod]
        public void TestMethodToCheckModify()
        {
            AddressBook addressBook = new AddressBook();
            addressBook.FirstName = "xxx";
            addressBook.LastName = "yyy";
            addressBook.PhoneNumber = "7412369852";
            addressBook.email = "xyz@gmail.com";
            int actual = addressBookRepo.ModifyDetails(addressBook);
            int expected = 1;
            Assert.AreEqual(expected, actual);
        }
        //Delete the person
        [TestMethod]
        public void DeleteContact()
        {
            AddressBook addressBook = new AddressBook();
            addressBook.FirstName = "Mohd";
            addressBook.LastName = "Aadil";
            int actual = addressBookRepo.DeletePerson(addressBook);
            int expected = 1;
            Assert.AreEqual(expected, actual);
        }
    }
}
