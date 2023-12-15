using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilder
{
    public sealed class ElementDBHandler
    {

        static readonly string conString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        static readonly ElementDBHandler instance = new ElementDBHandler();



        private ElementDBHandler()
        {
            CreateTable();

            Element newE1 = new Element
            {
                CategoryName = "Things ",
                Description = "Bellingham",
                Date = "2 april"
            };
            Element newE2 = new Element
            {
                CategoryName = "Quoi",
                Description = "Pourquoi",
                Date = "2 june"
            };


            Element newE3 = new Element
            {
                CategoryName = "HOO",
                Description = "RAYY",
                Date = "2 july"
            };

            AddElement(newE1);
            AddElement(newE2);
            AddElement(newE3);

        }

        public static ElementDBHandler Instance
        {
            get { return instance; }
        }

        public void CreateTable()
        {
            using (SQLiteConnection con = new SQLiteConnection(conString))
            {
                con.Open();
                string drop = "drop table if exists ELEMENTS;";
                SQLiteCommand command1 = new SQLiteCommand(drop, con);
                command1.ExecuteNonQuery();

                string table = "Create table ELEMENTS (ID integer primary key," +
                    "CategoryName text, " +
                    "Description text, Date text);";


                SQLiteCommand command2 = new SQLiteCommand(table, con);
                command2.ExecuteNonQuery();



            }
        }

        public int AddElement(Element element)
        {
            int rows = 0;
            int newId = 0;
            using (SQLiteConnection con = new SQLiteConnection(conString))
            {
                con.Open();
                //create a parameterized query
                string query = "INSERT INTO ELEMENTS (CategoryName, Description, Date) VALUES(@CategoryName, " +
                                "@Description, @Date)";

                SQLiteCommand insertcom = new SQLiteCommand(query, con);

                //pass values to the querry parameters
                insertcom.Parameters.AddWithValue("@CategoryName", element.CategoryName);
                insertcom.Parameters.AddWithValue("@Description", element.Description);
                insertcom.Parameters.AddWithValue("@Date", element.Date);


                try
                {
                    rows = insertcom.ExecuteNonQuery();
                    //let get the rowid inserted
                    insertcom.CommandText = " select last_insert_rowid()";
                    Int64 LastRowID64 = Convert.ToInt64(insertcom.ExecuteScalar());
                    //grab the bottom 32-bits as the unique id of the row
                    newId = Convert.ToInt32(LastRowID64);
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine("error generated. Details: " + e.ToString());
                }
            }
            return newId;
        }

        public Element GetElement(int id)
        {
            Element element = new Element();

            using (SQLiteConnection con = new SQLiteConnection(conString))
            {
                con.Open();
                SQLiteCommand getcom = new SQLiteCommand("Select * from Persons " +
                    "WHERE Id= @Id", con);
                getcom.Parameters.AddWithValue("@Id", id);

                using (SQLiteDataReader reader = getcom.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (Int32.TryParse(reader["Id"].ToString(), out int id2))
                        {
                            element.Id = id2;
                        }
                        element.CategoryName = reader["CategoryName"].ToString();
                        element.Description = reader["Description"].ToString();
                        element.Date = reader["Date"].ToString();
                    }
                }
            }
            return element;
        }

        public int UpdateElement(Element element)

        {
            int row = 0;
            using (SQLiteConnection con = new SQLiteConnection(conString))
            {

                con.Open();
                string query = "UPDATE ELEMENTS SET CategoryName= @CategoryName, Description= @Description," +
                    "Date= @Date WHERE Id=@Id";

                SQLiteCommand updatecom = new SQLiteCommand(query, con);
                updatecom.Parameters.AddWithValue("@Id", element.Id);
                updatecom.Parameters.AddWithValue("@CategoryName", element.CategoryName);
                updatecom.Parameters.AddWithValue("@Description", element.Description);
                updatecom.Parameters.AddWithValue("@Date", element.Description);


                try
                {
                    row = updatecom.ExecuteNonQuery();
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine("error generated. Details: " + e.ToString());
                }
            }
            return row;
        }

        public int DeleteElement(Element element)
        {
            int row = 0;
            using (SQLiteConnection con = new SQLiteConnection(conString))
            {
                con.Open();
                string query = "DELETE FROM ELEMENTS WHERE id= @Id";
                SQLiteCommand deletecom = new SQLiteCommand(query, con);
                deletecom.Parameters.AddWithValue("@Id", element.Id);
                try
                {
                    row = deletecom.ExecuteNonQuery();
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine("Error geenrated detials:" + e.ToString());
                }
                return row;
            }

        }

        public List<Element> ReadAllElements()
        {
            List<Element> listElements = new List<Element>();
            using (SQLiteConnection con = new SQLiteConnection(conString))
            {
                con.Open();
                SQLiteCommand com = new SQLiteCommand("Select * from ELEMENTS", con);
                using (SQLiteDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Create a Person Object
                        Element element = new Element();
                        if (Int32.TryParse(reader["Id"].ToString(), out int id))
                        {
                            element.Id = id;
                        }
                        element.CategoryName = reader["CategoryName"].ToString();
                        element.Description = reader["Description"].ToString();
                        element.Date = reader["Date"].ToString();



                        listElements.Add(element);


                    }
                }
            }
            return listElements;
        }
    }
}
