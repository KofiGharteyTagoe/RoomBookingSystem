using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LoginAppmain
{
    public class SearchForUsers
    {
    }

    public class getSearchedUsers
    {

        public DataTable getBookingInfo(int UserId)
        {
            DataTable bookingInfo = new DataTable();
            bookingInfo = DatabaseConnections.GetBookingOfUser(UserId, false);

            return bookingInfo;
        }

        public DataTable getInfoOnUser(int UserId)
        {
            DataTable userInfo = new DataTable();
            userInfo= DatabaseConnections.SearchByUserID(UserId);

            return userInfo;
        }


        public DataTable getUsersInfo(string searchItem, string searchValue)
        {
            DataTable users = new DataTable();

            searchValue.ToLower();


            if (searchItem.Equals("Name"))
            {

                //Search for users where  name is= Firstname
                DataTable nameResult = new DataTable();
                nameResult = DatabaseConnections.SearchByFullName(searchValue);

                return nameResult;
            }

            else if (searchItem.Equals("User Name"))
            {
                //Search for users where username is= Username
                DataTable userNameResult = new DataTable();
                userNameResult = DatabaseConnections.SearchByUserName(searchValue);

                return userNameResult;
            }

            else if (searchItem.Equals("Position"))
            {
                int searchData = 0;
                //View all users in this position
                if (searchValue.Equals("admin"))
                {
                    searchData = 1;
                }
                else if (searchValue.Equals("novus team"))
                {
                    searchData = 2;
                }
                else if (searchValue.Equals("novus"))
                {
                    searchData = 3;
                }
                else if (searchValue.Equals("trainee"))
                {
                    searchData = 3;
                }
                else if (searchValue.Equals("non novus staff"))
                {
                    searchData = 4;
                }

                DataTable positionResult = new DataTable();
                positionResult = DatabaseConnections.SearchByAccessLevel(searchData);

                return positionResult;
            }

            if (searchItem.Equals("Email address"))
            {
                //search for user with email address
                DataTable emailResult = new DataTable();
                emailResult = DatabaseConnections.SearchByEmail(searchValue);

                return emailResult;
            }

            return users;
        }
    }
}