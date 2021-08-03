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
        public int InsertIntoTable(AddressBook addressBook)
        {
            int change = 0;
            try
            {
                using (sqlConnection)
                {
                    //spInsertintoTable is insert procedure
                    SqlCommand sqlCommand = new SqlCommand("spInsertintoTable", this.sqlConnection);
                    //setting command type as stored procedure
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@FirstName",addressBook.FirstName);
                    sqlCommand.Parameters.AddWithValue("@LastName", addressBook.LastName);
                    sqlCommand.Parameters.AddWithValue("@Address",addressBook.Address);
                    sqlCommand.Parameters.AddWithValue("@City", addressBook.City);
                    sqlCommand.Parameters.AddWithValue("@State", addressBook.State);
                    sqlCommand.Parameters.AddWithValue("@ZipCode", addressBook.ZipCode);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", addressBook.PhoneNumber);
                    sqlCommand.Parameters.AddWithValue("@email", addressBook.email);
                    sqlCommand.Parameters.AddWithValue("@addressBookName",addressBook.addressBookName);
                    sqlCommand.Parameters.AddWithValue("@addressBookType", addressBook.addressBookType);
                    sqlConnection.Open();
                    //returns the number of rows updated
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result != 0)
                        change = 1;


                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return change;
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
        public int ModifyDetails(AddressBook addressBook)
        {
            int change = 0;
            try
            {
                using (sqlConnection)
                {
                    //spUdpateEmployeeDetails is stored procedure
                    SqlCommand sqlCommand = new SqlCommand("spModify", this.sqlConnection);
                    //setting command type as stored procedure
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    //sending params 
                    sqlCommand.Parameters.AddWithValue("@FirstName", addressBook.FirstName);
                    sqlCommand.Parameters.AddWithValue("@LastName", addressBook.LastName);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", addressBook.PhoneNumber);
                    sqlCommand.Parameters.AddWithValue("@email",addressBook.email);
                    sqlConnection.Open();
                    //returns the number of rows updated
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result != 0)
                        change = 1;

                    //close reader
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //closes the connection
                sqlConnection.Close();

            }
            return change;

        }

    }
}
