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
    }
}
