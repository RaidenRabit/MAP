using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MAPeApi.Models;

namespace MAPeApi.DataAccess
{
    public class DbUser
    {
        private SqlConnection connection;

        public DbUser()
        {
            DbConnection c = new DbConnection();
            connection = c.GetConnection();
        }
        
        public int Authenticate(string username, string password)
        {
            try
            {
                string stmt = "SELECT userID FROM Users WHERE username=@0 AND passwordHash=HASHBYTES('SHA2_512', @1+CAST(Salt AS NVARCHAR(36)))";
                using (SqlCommand cmd = new SqlCommand(stmt, connection))
                {
                    cmd.Parameters.AddWithValue("@0", username);
                    cmd.Parameters.AddWithValue("@1", password);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        if (reader.HasRows)
                        {
                            int id = Int32.Parse(reader["userID"].ToString());
                            return id;
                        }
                    }
                }
                throw new Exception("No such user");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        /// <summary>
        /// Find UserInfo by multiple parameters
        /// </summary>
        /// <param name="what">string of what you are looking for</param>
        /// <param name="by">type by which the search should be done (userID, username, email, nickname)</param>
        /// <returns>Returns list of Profile object and it's id</returns>
        public User GetUser(string what, FindUserBy by)
        {
            string stmt;

            switch (by)
            {
                case FindUserBy.userID:
                    stmt = "SELECT * FROM Users WHERE userID = @0";
                    break;
                case FindUserBy.username:
                    stmt = "SELECT * FROM Users WHERE username = @0;";
                    break;
                case FindUserBy.email:
                    stmt = "SELECT * FROM Users WHERE email = @0;";
                    break;
                case FindUserBy.nickname:
                    stmt = "SELECT * FROM Users WHERE nickname = @0;";
                    break;
                default:
                    throw new Exception("'by' parameter must be of type FindUserBy");
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(stmt, connection))
                {
                    cmd.Parameters.AddWithValue("@0", what);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            User user = new User
                            {
                                UserId = Int32.Parse(reader["userID"].ToString()),
                                Username = reader["username"].ToString(),
                                Email = reader["email"].ToString(),
                                Nickname = reader["nickname"].ToString()
                            };
                            return user;
                        }
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        /*
        /// <summary>
        /// Creates new ProfileInfo
        /// </summary>
        /// <param name="profile">New ProfileInfo</param>
        /// <returns>Returns the ID assigned to the profile by the database or -1 if it fails and prints error in console</returns>
        public int CreateProfile(Profile profile, SqlTransaction transaction, SqlConnection connection)
        {
            try
            {
                string stmt1 = "DECLARE @salt UNIQUEIDENTIFIER=NEWID() INSERT INTO Profile(username, salt, passwordHash, email, nickname)" +
                    "OUTPUT INSERTED.profileID values (@0, @salt, HASHBYTES('SHA2_512', @1 +CAST(@salt AS NVARCHAR(36))), @2, @3 );";
                using (SqlCommand cmd = new SqlCommand(stmt1, connection, transaction))
                {
                    cmd.Parameters.AddWithValue("@0", profile.Username);
                    cmd.Parameters.AddWithValue("@1", profile.Password);
                    cmd.Parameters.AddWithValue("@2", profile.Email);
                    cmd.Parameters.AddWithValue("@3", profile.Nickname);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        int id = reader.GetInt32(0);
                        return id;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return -1;
        }
        
        /// <summary>
        /// Updates ProfileInfo
        /// </summary>
        /// <param name="id">The ID of the info you want to change</param>
        /// <param name="profile">New ProfileInfo</param>
        /// <returns>Returns true if succeded, false otherwise and prints error in console</returns>
        public bool UpdateProfile(int id, Profile profile, SqlTransaction transaction, SqlConnection connection)
        {
            try
            {
                string stmt = "UPDATE Profile SET username = ISNULL(NULLIF(@0, ''), username), email = ISNULL(NULLIF(@1, ''), email), nickname = ISNULL(NULLIF(@2, ''), nickname) WHERE profileID = @3;";

                using (SqlCommand cmd = new SqlCommand(stmt, connection, transaction))
                {

                    cmd.Parameters.AddWithValue("@0", profile.Username);
                    cmd.Parameters.AddWithValue("@1", profile.Email);
                    cmd.Parameters.AddWithValue("@2", profile.Nickname);
                    cmd.Parameters.AddWithValue("@3", profile.ProfileID);
                    cmd.ExecuteNonQuery();
                }

                if (profile.Password != null)
                    if (profile.Password != "")
                    {
                        stmt = "DECLARE @salt UNIQUEIDENTIFIER=NEWID() UPDATE Profile  SET salt = @salt, passwordHash = HASHBYTES('SHA2_512', @0 +CAST(@salt AS NVARCHAR(36))) WHERE profileId = @1";

                        using (SqlCommand cmd = new SqlCommand(stmt, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@0", profile.Password);
                            cmd.Parameters.AddWithValue("@1", profile.ProfileID);
                            cmd.ExecuteNonQuery();
                        }
                    }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Deletes ProfileInfo
        /// !!! Deleteing Profile, WILL ALSO DELETE LINKED PROFILE AND ALL THE ACTIONS THAT PROFILE HAS MADE
        /// </summary>
        /// <param name="id">ID of info you want to delete</param>
        /// <returns>Returns true if succedes, false otherwise and prints error in console</returns>
        public bool DeleteProfile(int id, SqlTransaction transaction, SqlConnection connection)
        {
            if (id < 1)
                return false;
            try
            {
                string stmt = "DELETE FROM Profile WHERE profileID = @0";
                using (SqlCommand cmd = new SqlCommand(stmt, connection, transaction))
                {
                    cmd.Parameters.AddWithValue("@0", id);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }*/
    }
}
