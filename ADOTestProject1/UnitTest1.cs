using AddressBook_ADO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
            int expected = 5;
            Assert.AreEqual(expected, actual);
        }
        // Insert into table
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
        //Prints based on city name
        [TestMethod]
        public void PrintContactBasedOnCityName()
        {
            int actual = addressBookRepo.PrintDataBasedOnCity("Chennai");
            int expected =2;
            Assert.AreEqual(expected, actual);
        }
        //Print Count based on city and state
        [TestMethod]
        public void PrintCountBasedOnCityAndState()
        {
            List<int> actual = addressBookRepo.PrintCountBasedOnCityAndStateName();
            int[] temp = { 1, 1, 2, 1 };
            var expected = new List<int>(temp);
            CollectionAssert.AreEqual(actual, expected);
        }
        //Checks for sorted name
        [TestMethod]
        public void SortBasedOnNameGivenCity()
        {
            List<string> actual = addressBookRepo.PrintSortedNameBasedOnCity("Chennai");
            string[] temp = { "Amir","Ram"};
            var expected = new List<string>(temp);
            CollectionAssert.AreEqual(actual,expected);
        }
        //count based on type
        [TestMethod]
        public void TestMethodPrintCountBasedOnType()
        {
            List<int> actual = addressBookRepo.PrintCountBasedOnAddressBookType();
            int[] temp = {2,2,1};
            var expected = new List<int>(temp);
            CollectionAssert.AreEqual(actual, expected);
        }
        /// <summary>
        /// Test cases after ER
        /// Repeating the above test cases after ER
        /// </summary>
        [TestMethod]
        public void PrintContactBasedOnCityNameAfterER()
        {
            int actual = addressBookRepo.PrintDataBasedOnCityAfterER("Chennai");
            int expected = 2;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void PrintCountBasedOnCityAndStateAfterER()
        {
            List<int> actual = addressBookRepo.PrintCountBasedOnCityAndStateNameAfterER();
            int[] temp = { 1, 2, 1};
            var expected = new List<int>(temp);
            CollectionAssert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void SortBasedOnNameGivenCityAfterER()
        {
            List<string> actual = addressBookRepo.PrintSortedNameBasedOnCityAfterER("Chennai");
            string[] temp = { "Amir", "Ram" };
            var expected = new List<string>(temp);
            CollectionAssert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void TestMethodPrintCountBasedOnTypeAfterER()
        {
            List<int> actual = addressBookRepo.PrintCountBasedOnAddressBookTypeAfterER();
            int[] temp = {1, 3, 2 };
            var expected = new List<int>(temp);
            CollectionAssert.AreEqual(actual, expected);
        }
        //TestMethod to check values are updated into date column of every row
        [TestMethod]
        public void TestMethodForInsertDateColumn()
        {
            string actual = addressBookRepo.UpdateDateColumn();
            string expected = "All transaction are updated";
            Assert.AreEqual(actual,expected);
        }
        //Test method to check the data  within date range
        [TestMethod]
        public void TestMethodForRetrieveDataBasedOnData()
        {
            List<string> actual = addressBookRepo.RetrieveDataBasedOnDateRange();
            string[] temp = { "Uma Devi","Dhanush Raj" };
            var expected = new List<string>(temp);
            CollectionAssert.AreEqual(actual, expected);
        }
        //insert into multiple table
        [TestMethod]
        public void InsertOnMultipleTableUsingTransaction()
        {
            string actual = addressBookRepo.InsertIntoTablesForTRQuery();
            string expected = "All transaction are updated";
            Assert.AreEqual(actual, expected);
        }
       

    }
}
