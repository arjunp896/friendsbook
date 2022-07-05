using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using PrjLavalifeWebServer.util;

namespace PrjLavalifeWebServer
{
    internal class DbService
    {
        public static DataSet setUsers;

        internal static int ValidateUser(string email, string password)
        {
            OleDbConnection mycon = DBConnection.GetConnection();

            mycon.Open();

            String sql = "SELECT RefUser FROM Users" +
                            " WHERE username = @email AND psw = @psw;";
            OleDbCommand mycmd = new OleDbCommand(sql, mycon);

            mycmd.Parameters.AddWithValue("email", email);
            mycmd.Parameters.AddWithValue("psw", password);

            OleDbDataReader myRder = mycmd.ExecuteReader();

            int userid;
            if (myRder.Read())
            {
                userid = myRder.GetInt32(0);

                mycon.Close();
                return userid;
            }

            mycon.Close();
            return -1;
        }

        internal static DataRow GetUserByUserid(string userid)
        {
            OleDbConnection mycon = DBConnection.GetConnection();

            mycon.Open();

            String sql = "SELECT username FROM Users" +
                            " WHERE RefUser = @userid";
            OleDbCommand mycmd = new OleDbCommand(sql, mycon);

            mycmd.Parameters.AddWithValue("userid", userid);

            OleDbDataReader myRder = mycmd.ExecuteReader();

            DataRow user = null;

            if (myRder.Read())
            {

                user = GetUserByUsername(myRder.GetValue(0).ToString());

                mycon.Close();

            }

            mycon.Close();
            return user;

        }

        internal static DataTable GetMessages(string currentUser)
        {
            OleDbConnection mycon = DBConnection.GetConnection();

            String sql = @"SELECT        Messages.RefMessage, Messages.Message, Messages.RefSender, Messages.RefReceiver, Messages.CreateDate, Messages.IsNew, Users.RefUser, Users.username, Users.firstname, Users.lastname, Users.ImageUrl
FROM            (Messages INNER JOIN
                         Users ON Messages.RefReceiver = Users.RefUser)
WHERE        (Messages.RefSender = @cu) OR
                         (Messages.RefReceiver = @cu)";


            OleDbCommand mycmd = new OleDbCommand(sql, mycon);
            mycmd.Parameters.AddWithValue("cu", int.Parse(currentUser));

            OleDbDataAdapter myadp = new OleDbDataAdapter(mycmd);

            DataTable messages = new DataTable();

            myadp.Fill(messages);

            mycon.Close();

            return messages;
        }

        internal static DataRow GetUserByUsername(string username)
        {
            OleDbConnection mycon = DBConnection.GetConnection();

            mycon.Open();

            string sql = String.Format("SELECT * FROM Users WHERE username = '{0}'", username);

            OleDbDataAdapter userAdapter = new OleDbDataAdapter(sql, mycon);

            DataTable dt = new DataTable();
            userAdapter.Fill(dt);

            DataRow row = dt.Rows[0];

            sql = @"SELECT      DrinkingTypes.DrinkingType, Ethinicities.EthinicityName, 
                                Interested.InterestedType, SmokingTypes.SmokingType, 
                                Religions.ReligionName, BodyTypes.BodyTypeName, 
                                LookingFor.LookingType, Educations.EducationType
                                FROM            Religions, SmokingTypes, BodyTypes, DrinkingTypes, Educations, 
                                                Ethinicities, Interested, LookingFor
                                WHERE           (BodyTypes.RefBodyType = @body) AND (DrinkingTypes.RefDrinking = @drink) 
                                                AND (Educations.RefEducation = @edu) AND (Ethinicities.RefEthinicity = @ethinicity) 
                                                AND (Interested.RefInterested = @interst) AND (LookingFor.RefLookingFor = @look) AND 
                                             (Religions.RefReligion = @religion) AND (SmokingTypes.RefSmoking = @smoke)";
            OleDbCommand mycmd = new OleDbCommand(sql, mycon);

            int refBodyType = 0;
            int refDrinking = 0;
            int refEducation = 0;
            int refEthinicity = 0;
            int refInterested = 0;
            int refLookingFor = 0;
            int refReligion = 0;
            int refSmoking = 0;
                 



            try
            {
                refBodyType = int.Parse(row["RefBodyType"].ToString());
                refDrinking = int.Parse(row["RefDrinking"].ToString());
                refEducation = int.Parse(row["RefEducation"].ToString());
                refEthinicity = int.Parse(row["RefEthinicity"].ToString());
                refInterested = int.Parse(row["RefInterested"].ToString());
                refLookingFor = int.Parse(row["RefLookingFor"].ToString());
                refReligion = int.Parse(row["RefReligion"].ToString());
                refSmoking = int.Parse(row["RefSmoking"].ToString());
            }
            catch (Exception ex)
            {

            }



            mycmd.Parameters.AddWithValue("body", refBodyType);
            mycmd.Parameters.AddWithValue("drink", refDrinking);
            mycmd.Parameters.AddWithValue("edu", refEducation);
            mycmd.Parameters.AddWithValue("ethinicity", refEthinicity);
            mycmd.Parameters.AddWithValue("interst", refInterested);
            mycmd.Parameters.AddWithValue("look", refLookingFor);
            mycmd.Parameters.AddWithValue("religion", refReligion);
            mycmd.Parameters.AddWithValue("smoke", refSmoking);



            OleDbDataReader reader = mycmd.ExecuteReader();

            if (reader.Read())
            {
                row["RefDrinking"] = reader.GetValue(0);
                row["RefEthinicity"] = reader.GetValue(1);
                row["RefInterested"] = reader.GetValue(2);
                row["RefSmoking"] = reader.GetValue(3);
                row["RefReligion"] = reader.GetValue(4);
                row["RefBodyType"] = reader.GetValue(5);
                row["RefLookingFor"] = reader.GetValue(6);
                row["RefEducation"] = reader.GetValue(7);

                mycon.Close();

                return row;

            }
            mycon.Close();
            return row;
        }

        internal static bool CheckUsername(string username)
        {
            OleDbConnection mycon = DBConnection.GetConnection();

            mycon.Open();

            String sql = "SELECT RefUser FROM Users" +
                            " WHERE username = @username";
            OleDbCommand mycmd = new OleDbCommand(sql, mycon);

            mycmd.Parameters.AddWithValue("username", username);

            OleDbDataReader myRder = mycmd.ExecuteReader();

            if (myRder.HasRows)
            {
                mycon.Close();
                return false;
            }

            mycon.Close();
            return true;
        }

        internal static bool CreateMessage(String message, string sender, string receiver)
        {
            string sql = @"INSERT INTO Messages
                         (Message, RefSender, RefReceiver)
                          VALUES(@msg,@sender,@receiver)";

            OleDbConnection mycon = DBConnection.GetConnection();

            mycon.Open();

            OleDbCommand mycmd = new OleDbCommand(sql, mycon);

            mycmd.Parameters.AddWithValue("msg", message);
            mycmd.Parameters.AddWithValue("sender", int.Parse(sender));
            mycmd.Parameters.AddWithValue("receiver", int.Parse(receiver));

            int c = mycmd.ExecuteNonQuery();

            if (c > 0)
            {
                mycon.Close();
                return true;
            }

            mycon.Close();

            return false;
        }

        internal static List<KeyValuePair<string, string>> GetInterstedFields()
        {
            List<KeyValuePair<string, string>> fields = new List<KeyValuePair<string, string>>();

            OleDbConnection mycon = DBConnection.GetConnection();

            mycon.Open();

            string sql = "SELECT * FROM Interested";

            OleDbCommand mycmd = new OleDbCommand(sql, mycon);

            OleDbDataReader myrder = mycmd.ExecuteReader();

            while (myrder.Read())
            {
                fields.Add(new KeyValuePair<string, string>(myrder[0].ToString(), myrder[1].ToString()));
            }

            mycon.Close();

            return fields;
        }



        internal static int CreateUser(string fname, string lname, string usename, string gender, string interested, DateTime dob, string email, string password)
        {

            OleDbConnection mycon = DBConnection.GetConnection();

            mycon.Open();

            string sql = "INSERT INTO Users " +
                         "(firstname, lastname, username, gender, email, psw, BirthDate) " +
                         "VALUES(@fn,@ln,@un,@gender,@email,@psw,@dob)";

            OleDbCommand mycmd = new OleDbCommand(sql, mycon);

            mycmd.Parameters.AddWithValue("fn", fname);
            mycmd.Parameters.AddWithValue("ln", lname);
            mycmd.Parameters.AddWithValue("un", usename);
            mycmd.Parameters.AddWithValue("gender", gender);
            mycmd.Parameters.AddWithValue("email", email);
            mycmd.Parameters.AddWithValue("psw", password);
            mycmd.Parameters.AddWithValue("dob", dob);

            int c = mycmd.ExecuteNonQuery();


            if (c > 0)
            {
                mycmd.CommandText = "Select @@Identity;";

                int id = (int)mycmd.ExecuteScalar();

                mycon.Close();

                return id;
            }

            mycon.Close();

            return -1;
        }

        internal static DataSet LoadDashboardSetUsers(string userid)
        {
            DataSet myset = new DataSet();

            OleDbConnection mycon = DBConnection.GetConnection();

            mycon.Open();

            string sql = "SELECT * FROM USERS WHERE RefUser <> @userid;";

            OleDbCommand mycmd = new OleDbCommand(sql, mycon);

            mycmd.Parameters.AddWithValue("userid", userid);

            OleDbDataAdapter userAdapter = new OleDbDataAdapter(mycmd);

            userAdapter.Fill(myset, Constants.TABLE_USERS);

            //------------------------------------------------------------

            sql = "SELECT * FROM LookingFor";

            OleDbDataAdapter lokingAdapter = new OleDbDataAdapter(sql, mycon);

            lokingAdapter.Fill(myset, Constants.TABLE_LOOKING_FOR);


            //------------------------------------------------------------

            sql = "SELECT * FROM Ethinicities";

            OleDbDataAdapter thinicitiesAdapter = new OleDbDataAdapter(sql, mycon);

            thinicitiesAdapter.Fill(myset, Constants.TABLE_ETHINICITIES);

            //------------------------------------------------------------

            sql = "SELECT * FROM Religions";

            OleDbDataAdapter religionsAdapter = new OleDbDataAdapter(sql, mycon);

            religionsAdapter.Fill(myset, Constants.TABLE_RELIGIONS);

            //------------------------------------------------------------

            sql = "SELECT MIN(BirthDate) AS MinDate, MAX(BirthDate) " +
                  "AS MaxDate FROM Users";

            mycmd = new OleDbCommand(sql, mycon);

            OleDbDataReader myrder = mycmd.ExecuteReader();

            if (myrder.Read())
            {
                DataTable ageTable = new DataTable(Constants.TABLE_AGES);

                DateTime minDate = myrder.GetDateTime(0);
                DateTime maxDate = myrder.GetDateTime(1);

                var today = DateTime.Today;

                var minAge = today.Year - maxDate.Year;
                var maxAge = today.Year - minDate.Year;

                ageTable.Clear();

                ageTable.Columns.Add("Year");

                DataRow dr;

                for (int i = minAge; i <= maxAge; i++)
                {
                    dr = ageTable.NewRow();

                    dr["Year"] = i;

                    ageTable.Rows.Add(dr);
                }

                myset.Tables.Add(ageTable);
            }

            mycon.Close();

            return myset;
        }


        internal static DataSet LoadDropDownForStep2()
        {
            DataSet myset = new DataSet();

            string sql = "";

            OleDbConnection mycon = DBConnection.GetConnection();

            mycon.Open();

            sql = "SELECT * FROM BodyTypes";

            OleDbDataAdapter bodyTypeAdapte = new OleDbDataAdapter(sql, mycon);

            bodyTypeAdapte.Fill(myset, Constants.TABLE_BODY_TYPES);

            //------------------------------------------------------------

            sql = "SELECT * FROM Ethinicities";

            OleDbDataAdapter thinicitiesAdapter = new OleDbDataAdapter(sql, mycon);

            thinicitiesAdapter.Fill(myset, Constants.TABLE_ETHINICITIES);

            //------------------------------------------------------------

            sql = "SELECT * FROM Religions";

            OleDbDataAdapter religionsAdapter = new OleDbDataAdapter(sql, mycon);

            religionsAdapter.Fill(myset, Constants.TABLE_RELIGIONS);

            //------------------------------------------------------------

            sql = "SELECT * FROM Educations";

            OleDbDataAdapter educationAdapter = new OleDbDataAdapter(sql, mycon);

            educationAdapter.Fill(myset, Constants.TABLE_EDUCATION);


            //------------------------------------------------------------


            sql = "SELECT * FROM LookingFor";

            OleDbDataAdapter lookingAdapter = new OleDbDataAdapter(sql, mycon);

            lookingAdapter.Fill(myset, Constants.TABLE_LOOKING_FOR);

            sql = "SELECT * FROM SmokingTypes";

            OleDbDataAdapter smokingAdapter = new OleDbDataAdapter(sql, mycon);

            smokingAdapter.Fill(myset, Constants.TABLE_SMOKING);

            sql = "SELECT * FROM DrinkingTypes";

            OleDbDataAdapter drinkingAdapter = new OleDbDataAdapter(sql, mycon);

            drinkingAdapter.Fill(myset, Constants.TABLE_DRINKING);

            return myset;
        }

        internal static DataTable GetHeights()
        {



            DataTable tabHeight = new DataTable(Constants.TABLE_HEIGHT);

            tabHeight.Columns.Add("height");
            tabHeight.Columns.Add("value");


            for (int i = 3; i <= 8; i++)
            {
                DataRow row = tabHeight.NewRow();

                row["height"] = i;
                row["value"] = i;

                tabHeight.Rows.Add(row);
            }

            return tabHeight;
        }

        internal static DataTable GetInches()
        {

            DataTable tabInche = new DataTable(Constants.TABLE_INCHE);

            tabInche.Columns.Add("inche");
            tabInche.Columns.Add("value");


            for (int i = 0; i <= 11; i++)
            {
                DataRow row = tabInche.NewRow();

                row["inche"] = i;
                row["value"] = i;

                tabInche.Rows.Add(row);
            }

            return tabInche;
        }

    }
}