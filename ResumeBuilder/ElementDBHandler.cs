using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResumeBuilder.Models;

namespace ResumeBuilder
{
    public sealed class DatabaseHandler
    {
        private static readonly string conString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        private static readonly DatabaseHandler instance = new DatabaseHandler();

        private DatabaseHandler()
        {
            CreatePersonalInfoTable();
            CreateContactInfoTable();
            CreateEducationTable();
            CreateReferenceTable();
            CreateWorkExperienceTable();
        }

        public static DatabaseHandler Instance
        {
            get { return instance; }
        }

        private void CreatePersonalInfoTable()
        {
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS PersonalInfo (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                        Name TEXT, 
                        Email TEXT, 
                        Phone TEXT
                    )";
                cmd.ExecuteNonQuery();
            }
        }

        public void AddEmptyPersonalInfo()
        {
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                using (var cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "INSERT INTO PersonalInfo (Name, Email, Phone) VALUES (NULL, NULL, NULL)";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void CreateContactInfoTable()
        {
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS ContactInfo (
                Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                Description TEXT, 
                Telephone TEXT, 
                Address TEXT
                )";
                cmd.ExecuteNonQuery();
            }
        }

        private void CreateEducationTable()
        {
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Education (
                Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                Certification TEXT, 
                SchoolName TEXT, 
                YearGraduated INTEGER
                )";
                cmd.ExecuteNonQuery();
            }
        }

        private void CreateReferenceTable()
        {
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Reference (
                Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                Name TEXT, 
                Description TEXT, 
                ContactInfo TEXT
                )";
                cmd.ExecuteNonQuery();
            }
        }

        private void CreateWorkExperienceTable()
        {
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS WorkExperience (
                Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                CompanyName TEXT, 
                Role TEXT, 
                YearsWorked INTEGER
                )";
                cmd.ExecuteNonQuery();
            }
        }

        public int AddPersonalInfo(PersonalInfo personalInfo)
        {
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "INSERT INTO PersonalInfo (Name, Email, PhoneNumber) VALUES (@Name, @Email, @PhoneNumber)";
                cmd.Parameters.AddWithValue("@Name", personalInfo.Name);
                cmd.Parameters.AddWithValue("@Email", personalInfo.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", personalInfo.Phone);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT last_insert_rowid()";
                return (int)(long)cmd.ExecuteScalar();
            }
        }

        public int AddContactInfo(ContactInfo contactInfo)
        {
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "INSERT INTO ContactInfo (Description, Telephone, Address) VALUES (@Description, @Telephone, @Address)";
                cmd.Parameters.AddWithValue("@Description", contactInfo.Description);
                cmd.Parameters.AddWithValue("@Telephone", contactInfo.Telephone);
                cmd.Parameters.AddWithValue("@Address", contactInfo.Address);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT last_insert_rowid()";
                return (int)(long)cmd.ExecuteScalar();
            }
        }

        public int AddEducation(Education education)
        {
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "INSERT INTO Education (Certification, SchoolName, YearGraduated) VALUES (@Certification, @SchoolName, @YearGraduated)";
                cmd.Parameters.AddWithValue("@Certification", education.Certification);
                cmd.Parameters.AddWithValue("@SchoolName", education.SchoolName);
                cmd.Parameters.AddWithValue("@YearGraduated", education.YearGraduated);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT last_insert_rowid()";
                return (int)(long)cmd.ExecuteScalar();
            }
        }

        public int AddReference(Reference reference)
        {
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "INSERT INTO Reference (Name, Description, ContactInfo) VALUES (@Name, @Description, @ContactInfo)";
                cmd.Parameters.AddWithValue("@Name", reference.Name);
                cmd.Parameters.AddWithValue("@Description", reference.Description);
                cmd.Parameters.AddWithValue("@ContactInfo", reference.ContactInfo);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT last_insert_rowid()";
                return (int)(long)cmd.ExecuteScalar();
            }
        }

        public int AddWorkExperience(WorkExperience workExperience)
        {
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "INSERT INTO WorkExperience (CompanyName, Role, YearsWorked) VALUES (@CompanyName, @Role, @YearsWorked)";
                cmd.Parameters.AddWithValue("@CompanyName", workExperience.CompanyName);
                cmd.Parameters.AddWithValue("@Role", workExperience.Role);
                cmd.Parameters.AddWithValue("@YearsWorked", workExperience.YearsWorked);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT last_insert_rowid()";
                return (int)(long)cmd.ExecuteScalar();
            }
        }

        public bool EditPersonalInfo(PersonalInfo personalInfo)
        {
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "UPDATE PersonalInfo SET Name = @Name, Email = @Email, Phone = @Phone WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", personalInfo.Id);
                cmd.Parameters.AddWithValue("@Name", personalInfo.Name);
                cmd.Parameters.AddWithValue("@Email", personalInfo.Email);
                cmd.Parameters.AddWithValue("@Phone", personalInfo.Phone);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool EditContactInfo(ContactInfo contactInfo)
        {
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "UPDATE ContactInfo SET Description = @Description, Telephone = @Telephone, Address = @Address WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", contactInfo.Id);
                cmd.Parameters.AddWithValue("@Description", contactInfo.Description);
                cmd.Parameters.AddWithValue("@Telephone", contactInfo.Telephone);
                cmd.Parameters.AddWithValue("@Address", contactInfo.Address);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool EditEducation(Education education)
        {
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "UPDATE Education SET Certification = @Certification, SchoolName = @SchoolName, YearGraduated = @YearGraduated WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", education.Id);
                cmd.Parameters.AddWithValue("@Certification", education.Certification);
                cmd.Parameters.AddWithValue("@SchoolName", education.SchoolName);
                cmd.Parameters.AddWithValue("@YearGraduated", education.YearGraduated);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool EditReference(Reference reference)
        {
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "UPDATE Reference SET Name = @Name, Description = @Description, ContactInfo = @ContactInfo WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", reference.Id);
                cmd.Parameters.AddWithValue("@Name", reference.Name);
                cmd.Parameters.AddWithValue("@Description", reference.Description);
                cmd.Parameters.AddWithValue("@ContactInfo", reference.ContactInfo);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool EditWorkExperience(WorkExperience workExperience)
        {
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "UPDATE WorkExperience SET CompanyName = @CompanyName, Role = @Role, YearsWorked = @YearsWorked WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", workExperience.Id);
                cmd.Parameters.AddWithValue("@CompanyName", workExperience.CompanyName);
                cmd.Parameters.AddWithValue("@Role", workExperience.Role);
                cmd.Parameters.AddWithValue("@YearsWorked", workExperience.YearsWorked);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool DeletePersonalInfo(int id)
        {
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "DELETE FROM PersonalInfo WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool DeleteContactInfo(int id)
        {
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "DELETE FROM ContactInfo WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool DeleteEducation(int id)
        {
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "DELETE FROM Education WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool DeleteReference(int id)
        {
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "DELETE FROM Reference WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool DeleteWorkExperience(int id)
        {
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "DELETE FROM WorkExperience WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public List<PersonalInfo> ReadAllPersonalInfo()
        {
            var list = new List<PersonalInfo>();
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand("SELECT * FROM PersonalInfo", con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PersonalInfo
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["PhoneNumber"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public List<ContactInfo> ReadAllContactInfo()
        {
            var list = new List<ContactInfo>();
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand("SELECT * FROM ContactInfo", con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ContactInfo
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Description = reader["Description"].ToString(),
                            Telephone = reader["Telephone"].ToString(),
                            Address = reader["Address"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public List<Education> ReadAllEducation()
        {
            var list = new List<Education>();
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand("SELECT * FROM Education", con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Education
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Certification = reader["Certification"].ToString(),
                            SchoolName = reader["SchoolName"].ToString(),
                            YearGraduated = Convert.ToInt32(reader["YearGraduated"])
                        });
                    }
                }
            }
            return list;
        }

        public List<Reference> ReadAllReferences()
        {
            var list = new List<Reference>();
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand("SELECT * FROM Reference", con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Reference
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            ContactInfo = reader["ContactInfo"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public List<WorkExperience> ReadAllWorkExperiences()
        {
            var list = new List<WorkExperience>();
            using (var con = new SQLiteConnection(conString))
            {
                con.Open();
                var cmd = new SQLiteCommand("SELECT * FROM WorkExperience", con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new WorkExperience
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            CompanyName = reader["CompanyName"].ToString(),
                            Role = reader["Role"].ToString(),
                            YearsWorked = Convert.ToInt32(reader["YearsWorked"])
                        });
                    }
                }
            }
            return list;
        }
    }
}
