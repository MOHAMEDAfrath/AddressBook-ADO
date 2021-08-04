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
        public int DeletePerson(AddressBook addressBook)
        {
            int change = 0;
            try
            {
                using (sqlConnection)
                {
                    //spUdpateEmployeeDetails is stored procedure
                    SqlCommand sqlCommand = new SqlCommand("spDeleteFromTable", this.sqlConnection);
                    //setting command type as stored procedure
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    //sending params 
                    sqlCommand.Parameters.AddWithValue("@FirstName", addressBook.FirstName);
                    sqlCommand.Parameters.AddWithValue("@LastName", addressBook.LastName);
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
        public int PrintDataBasedOnCity(string city)
        {
            List<AddressBook> contacts = new List<AddressBook>();
            AddressBook addressBook = new AddressBook();
            //query to be executed
            string query = @"select * from address_book_table where City ="+"'"+city+"'";
            SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    addressBook = GetDetail(sqlDataReader);
                    contacts.Add(addressBook);
                }
            }
            return contacts.Count;
        }
        public List<int> PrintCountBasedOnCityAndStateName()
        {
            List<int> number = new List<int>();
            //query to be executed
            string query = @"select count(*) from address_book_table group by City,State";
            SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    //add number of person based on count to the list
                    number.Add(Convert.ToInt32(sqlDataReader[0]));
                }
            }
            else
            {
                return null;
            }
            return number;
        }
        public List<string> PrintSortedNameBasedOnCity(string city)
        {
            List<string> names = new List<string>();
            //query to be executed
            string query = @"select FirstName from address_book_table  where City =" + "'" + city + "' order by FirstName";
            SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    //add number of person based on count to the list
                    names.Add(Convert.ToString(sqlDataReader[0]));
                }
            }
            return names;
        } 
        public List<int> PrintCountBasedOnAddressBookType()
        {
            List<int> types = new List<int>();
            //query to be executed
            string query = @"select count(*) from address_book_table group by TypeOfAddressBook";
            SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    //add number of person based on count to the list
                    types.Add(Convert.ToInt32(sqlDataReader[0]));
                }
            }
            return types;

        }
        ////
        ///
        ////////////After ER////////////
        ///Tables are contact_list,address_book_type,contact_map_type
        ///
        public AddressBook GetDetailAfterER(SqlDataReader sqlDataReader)
        {

            AddressBook addressBook = new AddressBook();
            addressBook.FirstName = Convert.ToString(sqlDataReader["FirstName"]);
            addressBook.LastName = Convert.ToString(sqlDataReader["LastName"]);
            addressBook.Address = Convert.ToString(sqlDataReader["Address"] + " " + sqlDataReader["City"] + " " + sqlDataReader["State"] + " " + sqlDataReader["ZipCode"]);
            addressBook.PhoneNumber = Convert.ToString(sqlDataReader["PhoneNumber"]);
            addressBook.email = Convert.ToString(sqlDataReader["email"]);
            addressBook.addressBookName = Convert.ToString(sqlDataReader["AddressBookName"]);
            Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5} ", addressBook.FirstName, addressBook.LastName, addressBook.Address, addressBook.PhoneNumber, addressBook.email, addressBook.addressBookName);
            return addressBook;

        }
        public int PrintDataBasedOnCityAfterER(string city)
        {
            List<AddressBook> contacts = new List<AddressBook>();
            AddressBook addressBook = new AddressBook();
            //query to be executed
            string query = @"select contact_list.ContactId,contact_list.FirstName,contact_list.LastName,contact_list.Address,contact_list.City,contact_list.State,contact_list.ZipCode,contact_list.PhoneNumber,contact_list.email, contact_list.AddressBookName from contact_list where City ='"+city+"'";
            SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    addressBook = GetDetailAfterER(sqlDataReader);
                    contacts.Add(addressBook);
                }
            }
            return contacts.Count;
        }
        public List<int> PrintCountBasedOnCityAndStateNameAfterER()
        {
            List<int> number = new List<int>();
            //query to be executed
            string query = @"select count(*) from contact_list group by city";
            SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    //add number of person based on count to the list
                    number.Add(Convert.ToInt32(sqlDataReader[0]));
                }
            }
            else
            {
                return null;
            }
            return number;
        }
        public List<string> PrintSortedNameBasedOnCityAfterER(string city)
        {
            List<string> names = new List<string>();
            //query to be executed
            string query = @"select FirstName from contact_list where City =" + "'" + city + "' order by FirstName";
            SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    //add number of person based on count to the list
                    names.Add(Convert.ToString(sqlDataReader[0]));
                }
            }
            return names;
        }
        public List<int> PrintCountBasedOnAddressBookTypeAfterER()
        {
            List<int> types = new List<int>();
            //query to be executed
            string query = @"select count(*) as noofcontacts,address_book_type.AddressBookType from contact_list inner join contact_map_type on contact_list.ContactId = contact_map_type.contactId inner join address_book_type on contact_map_type.typeId = address_book_type.TypeId group by address_book_type.AddressBookType";
            SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    //add number of person based on count to the list
                    types.Add(Convert.ToInt32(sqlDataReader[0]));
                }
            }
            return types;

        }
        //////////Transaction//////////
        ///Alter table executed only once
        public void AlterTable()
        {
            using (sqlConnection)
            {
                sqlConnection.Open();
                //begins sql transaction
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    //performs the query
                    sqlCommand.CommandText = "Alter table contact_list add date_added Date";
                    sqlCommand.ExecuteNonQuery();
                    //commits if all the above transactions are executed
                    sqlTransaction.Commit();
                    Console.WriteLine("All transaction are updated");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //if any error roll backs to the last point
                    sqlTransaction.Rollback();
                }
            }
            sqlConnection.Close();

        }
        public string UpdateDateColumn()
        {
            string result = "Not Success";
            using (sqlConnection)
            {
                sqlConnection.Open();
                //begins sql transaction
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    //performs the query
                    sqlCommand.CommandText = "update contact_list set date_added = '2021-07-12' where FirstName = 'Ram' or FirstName = 'Amir'";
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.CommandText = "update contact_list set date_added = '2021-06-01' where FirstName = 'Dhanush' or FirstName = 'Uma'";
                    sqlCommand.ExecuteNonQuery();
                    //commits if all the above transactions are executed
                    sqlTransaction.Commit();
                    Console.WriteLine("All transaction are updated");
                    result = "All transaction are updated";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //if any error roll backs to the last point
                    sqlTransaction.Rollback();
                }
            }
            sqlConnection.Close();
            return result;
        }
        public List<string> RetrieveDataBasedOnDateRange()
        {
            List <string> addresses =  new List<string>();
            using (sqlConnection)
            {
                //query execution
                string query = @"select * from contact_list where date_added BETWEEN Cast('2021-06-01' as Date) and Cast('2021-07-03' as Date);";
                SqlCommand command = new SqlCommand(query, this.sqlConnection);
                //open sql connection
                sqlConnection.Open();
                SqlDataReader sqlDataReader = command.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while(sqlDataReader.Read())
                    addresses.Add(Convert.ToString(sqlDataReader[1]) +" "+Convert.ToString(sqlDataReader[2]));
                }
            }
            return addresses;
        }
        public string InsertIntoTablesForTRQuery()
        {
            string update = "Not Successful";
            using (sqlConnection)
            {
                sqlConnection.Open();
                //begins sql transaction
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    //performs the query
                    sqlCommand.CommandText = "Insert into contact_list values ('Malik','hamsa','xxxx','yyyy','zzzz','Zip1234','7412036040','malikk@acu.in','Neighbour','2021-01-20')";
                    sqlCommand.ExecuteScalar();
                    sqlCommand.CommandText = "Insert into contact_map_type values('5','2')";
                    sqlCommand.ExecuteNonQuery();
                    //commits if all the above transactions are executed
                    sqlTransaction.Commit();
                    Console.WriteLine("All transaction are updated");
                    update = "All transaction are updated";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //if any error roll backs to the last point
                    sqlTransaction.Rollback();
                }
            }
            sqlConnection.Close();

            return update;

        }

    }
}
