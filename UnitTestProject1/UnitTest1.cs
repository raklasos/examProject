using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddPerson()
        {
            DataBase dataBase = new DataBase();
            dataBase.openConnection();

            string comDel = " INSERT INTO Person([name], [phone_number], [password], [role_id]) Values(@name, @phone_number, @password, 1)";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            SqlParameter pr1, pr2, pr3;

            pr1 = new SqlParameter("@name", "Alex");
            pr2 = new SqlParameter("@phone_number", "8(999)-999-99-99");
            pr3 = new SqlParameter("@password", "jeNeS34");

            cmd1.Parameters.Add(pr1);
            cmd1.Parameters.Add(pr2);
            cmd1.Parameters.Add(pr3);
            cmd1.ExecuteNonQuery();

            comDel = " SELECT Person.name FROM Person WHERE phone_number = @phone_number";
            cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            pr1 = new SqlParameter("@phone_number", "8(999)-999-99-99");
            cmd1.Parameters.Add(pr1);
            var result = cmd1.ExecuteScalar();
            var expected = "Alex";

            dataBase.closeConnection();

            Assert.AreEqual(expected, result, "Ожидаемое имя не было получено!");
        }

        [TestMethod]
        public void ExistsPerson()
        {
            DataBase dataBase = new DataBase();
            dataBase.openConnection();
            try
            {
                string comDel = " INSERT INTO Person([name], [phone_number], [password], [role_id]) Values(@name, @phone_number, @password, 1)";
                SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
                SqlParameter pr1, pr2, pr3;

                pr1 = new SqlParameter("@name", "Alex");
                pr2 = new SqlParameter("@phone_number", "8(999)-999-99-99");
                pr3 = new SqlParameter("@password", "jeNeS34");

                cmd1.Parameters.Add(pr1);
                cmd1.Parameters.Add(pr2);
                cmd1.Parameters.Add(pr3);
                cmd1.ExecuteNonQuery();
                dataBase.closeConnection();
            }
            catch
            {
                return;
            }
            Assert.Fail("Ожидаемое исключение не было получено");
        }

        [TestMethod]
        public void ChangePersonsName()
        {
            DataBase dataBase = new DataBase();
            dataBase.openConnection();

            string comDel = "UPDATE Person Set name = @name WHERE phone_number = @phone_number";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            SqlParameter pr1, pr2;

            pr1 = new SqlParameter("@name", "Alexy");
            pr2 = new SqlParameter("@phone_number", "8(999)-999-99-99");

            cmd1.Parameters.Add(pr1);
            cmd1.Parameters.Add(pr2);
            cmd1.ExecuteNonQuery();

            comDel = "SELECT Person.name FROM Person WHERE phone_number = @phone_number";
            cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            pr1 = new SqlParameter("@phone_number", "8(999)-999-99-99");
            cmd1.Parameters.Add(pr1);
            var result = cmd1.ExecuteScalar();
            string expected = "Alexy";
            dataBase.closeConnection();

            Assert.AreEqual(expected, result, "Ожидаемое имя не было получено!");
        }

        [TestMethod]
        public void ChangePersonsPassword()
        {
            DataBase dataBase = new DataBase();
            dataBase.openConnection();

            string comDel = "UPDATE Person Set password = @password WHERE phone_number = @phone_number";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            SqlParameter pr1, pr2;

            pr1 = new SqlParameter("@password", "89dJe34");
            pr2 = new SqlParameter("@phone_number", "8(999)-999-99-99");

            cmd1.Parameters.Add(pr1);
            cmd1.Parameters.Add(pr2);
            cmd1.ExecuteNonQuery();

            comDel = "SELECT Person.password FROM Person WHERE phone_number = @phone_number";
            cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            pr1 = new SqlParameter("@phone_number", "8(999)-999-99-99");
            cmd1.Parameters.Add(pr1);
            var result = cmd1.ExecuteScalar();
            string expected = "89dJe34";
            dataBase.closeConnection();

            Assert.AreEqual(expected, result, "Ожидаемое имя не было получено!");
        }

        [TestMethod]
        public void DeletePerson()
        {
            DataBase dataBase = new DataBase();
            dataBase.openConnection();
            string comDel = "DELETE FROM Person WHERE phone_number = @phone_number";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            
            SqlParameter pr1;
            pr1 = new SqlParameter("@phone_number", "8(932)-523-62-22");
            cmd1.Parameters.Add(pr1);
            cmd1.ExecuteNonQuery();
            
            
            comDel = "SELECT Person.name FROM Person WHERE phone_number = @phone_number";
            cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            pr1 = new SqlParameter("@phone_number", "8(932)-523-62-22");
            cmd1.Parameters.Add(pr1);
            var result = cmd1.ExecuteScalar();
            dataBase.closeConnection();
            
            Assert.AreEqual(null, result, "Ожидаемое имя не было получено!");
        }




        [TestMethod]
        public void AddBook()
        {
            DataBase dataBase = new DataBase();
            dataBase.openConnection();

            string comDel = " INSERT INTO Book([name], [year], [publisher], [status_id]) Values(@name, @year, @publisher, 1)";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            SqlParameter pr1, pr2, pr3;

            pr1 = new SqlParameter("@name", "Erm");
            pr2 = new SqlParameter("@year", "2004");
            pr3 = new SqlParameter("@publisher", "Lyberts");

            cmd1.Parameters.Add(pr1);
            cmd1.Parameters.Add(pr2);
            cmd1.Parameters.Add(pr3);
            cmd1.ExecuteNonQuery();

            comDel = " SELECT Book.year FROM Book WHERE name = @name";
            cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            pr1 = new SqlParameter("@name", "Erm");
            cmd1.Parameters.Add(pr1);
            var result = cmd1.ExecuteScalar();
            var expected = 2004;

            dataBase.closeConnection();

            Assert.AreEqual(expected, result, "Ожидаемый год не был получен!");
        }

        [TestMethod]
        public void ChangeBookYear()
        {
            DataBase dataBase = new DataBase();
            dataBase.openConnection();

            string comDel = "UPDATE Book Set year = @year WHERE name = @name";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            SqlParameter pr1, pr2;

            pr1 = new SqlParameter("@year", 1981);
            pr2 = new SqlParameter("@name", "1984");

            cmd1.Parameters.Add(pr1);
            cmd1.Parameters.Add(pr2);
            cmd1.ExecuteNonQuery();

            comDel = "SELECT Book.year FROM Book WHERE name = @name";
            cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            pr1 = new SqlParameter("@name", "1984");
            cmd1.Parameters.Add(pr1);
            int result = Convert.ToInt32(cmd1.ExecuteScalar());
            int expected = 1981;
            dataBase.closeConnection();

            Assert.AreEqual(expected, result, "Ожидаемое имя не было получено!");
        }


        [TestMethod]
        public void ChangeBookPublisher()
        {
            DataBase dataBase = new DataBase();
            dataBase.openConnection();

            string comDel = "UPDATE Book Set publisher = @publisher WHERE name = @name";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            SqlParameter pr1, pr2;

            pr1 = new SqlParameter("@publisher", "Morlos");
            pr2 = new SqlParameter("@name", "1984");

            cmd1.Parameters.Add(pr1);
            cmd1.Parameters.Add(pr2);
            cmd1.ExecuteNonQuery();

            comDel = "SELECT Book.publisher FROM Book WHERE name = @name";
            cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            pr1 = new SqlParameter("@name", "1984");
            cmd1.Parameters.Add(pr1);
            var result = cmd1.ExecuteScalar();
            string expected = "Morlos";
            dataBase.closeConnection();

            Assert.AreEqual(expected, result, "Ожидаемое имя не было получено!");
        }

        [TestMethod]
        public void ChangeBookStatus()
        {
            DataBase dataBase = new DataBase();
            dataBase.openConnection();

            string comDel = "UPDATE Book Set status_id = @status_id WHERE name = @name";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            SqlParameter pr1, pr2;

            pr1 = new SqlParameter("@status_id", 2);
            pr2 = new SqlParameter("@name", "Erm");

            cmd1.Parameters.Add(pr1);
            cmd1.Parameters.Add(pr2);
            cmd1.ExecuteNonQuery();

            comDel = "SELECT Book.status_id FROM Book WHERE name = @name";
            cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            pr1 = new SqlParameter("@name", "Erm");
            cmd1.Parameters.Add(pr1);
            var result = cmd1.ExecuteScalar();
            int expected = 2;
            dataBase.closeConnection();

            Assert.AreEqual(expected, result, "Ожидаемое имя не было получено!");
        }

        [TestMethod]
        public void DeleteBook()
        {
            DataBase dataBase = new DataBase();
            dataBase.openConnection();
            string comDel = "DELETE FROM Book WHERE name = @name";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());

            SqlParameter pr1;
            pr1 = new SqlParameter("@name", "Erm");
            cmd1.Parameters.Add(pr1);
            cmd1.ExecuteNonQuery();


            comDel = "SELECT Book.year FROM Book WHERE name = @name";
            cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
            pr1 = new SqlParameter("@name", "2004");
            cmd1.Parameters.Add(pr1);
            var result = cmd1.ExecuteScalar();
            dataBase.closeConnection();

            Assert.AreEqual(null, result, "Ожидаемое название не было получено!");
        }
    }
}
