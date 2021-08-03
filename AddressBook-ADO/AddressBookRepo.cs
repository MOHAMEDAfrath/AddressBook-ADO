using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook_ADO
{
   public class AddressBookRepo
    {
        public static string connectionString = @"Server=.;Database=addressbook_service;Trusted_Connection=True;";
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public int GetContactDetails()
        {
            List<AddressBook> employeepayroll = new List<AddressBook>();
            AddressBook eREmployeeModel = new AddressBook();

            try
            { 
                using (sqlConnection)
                {
                    //query execution
                    string query = @"select * from address_book_table";
                    SqlCommand command = new SqlCommand(query, this.sqlConnection);
                    //open sql connection
                    sqlConnection.Open();
                    //sql reader to read data from db
                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            eREmployeeModel = GetDetail(sqlDataReader);
                            employeepayroll.Add(eREmployeeModel);
                        }

                    }
                    //close reader
                    sqlDataReader.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

                sqlConnection.Close();
            }
            return employeepayroll.Count;
        }
        public AddressBook GetDetail(SqlDataReader sqlDataReader)
        {

            AddressBook addressBook = new AddressBook();
            addressBook.FirstName = Convert.ToString(sqlDataReader["FirstName"]);
            addressBook.LastName = Convert.ToString(sqlDataReader["LastName"]);
            addressBook.Address = Convert.ToString(sqlDataReader["Address"]+" "+sqlDataReader["City"]+" "+sqlDataReader["State"]+" "+sqlDataReader["ZipCode"]);
            addressBook.PhoneNumber = Convert.ToString(sqlDataReader["PhoneNumber"]);
            addressBook.email = Convert.ToString(sqlDataReader["email"]);
            addressBook.addressBookName = Convert.ToString(sqlDataReader["AddressBookName"]);
            addressBook.addressBookType = Convert.ToString(sqlDataReader["TypeOfAddressBook"]);
            Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5} | {6}", addressBook.FirstName,addressBook.LastName,addressBook.Address,addressBook.PhoneNumber,addressBook.email,addressBook.addressBookName,addressBook.addressBookType);
            return addressBook;

        }

    }
}
