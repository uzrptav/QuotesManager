using QuotesManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QuotesManager.Services
{
    public class QuotesRepository
    {
        private const string CacheKey = "QuotesStore";
        private const string connString = "workstation id=ManhattanUniversity.mssql.somee.com;packet size=4096;user id=uzrptav_SQLLogin_1;pwd=amvcsm5bx4;data source=ManhattanUniversity.mssql.somee.com;persist security info=False;initial catalog=ManhattanUniversity";

        //public QuotesRepository()
        //{
        //    //var ctx = HttpContext.Current;

        //    //if (ctx != null)
        //    //{
        //    //    if (ctx.Cache[CacheKey] == null)
        //    //    {
        //    //        var quotes = new List<Quotes>
        //    //        {                      
        //    //                new Quotes
        //    //                {
        //    //                    quoteId = 1,
        //    //                    quoteText ="Скажи мне, кто тебя хвалит, и я тебе скажу, в чем ты ошибся.",
        //    //                    author = "Владимир Ильич Ленин"
        //    //                },
        //    //                new Quotes
        //    //                {
        //    //                    quoteId = 2,
        //    //                    quoteText ="Деньги — это сводник между потребностью и предметом, между жизнью и жизненными средствами человека.",
        //    //                    author = "Карл Маркс"
        //    //                },
        //    //                new Quotes
        //    //                {
        //    //                    quoteId = 3,
        //    //                    quoteText ="Хуй и пизда - из одного гнезда.",
        //    //                    author = "Виталыч"
        //    //                }
        //    //        };

        //    //        ctx.Cache[CacheKey] = quotes;
        //    //   }
        //    //}
        //    GetAllQuotes();
        //}

        public bool SaveQuotes(Quotes quotes)
        {           
                try
                {
                    using (SqlConnection openCon = new SqlConnection(connString))
                        {
                            string saveQoute = "INSERT into Quotes (quoteText ,author) VALUES (@quoteText, @author)";

                            using (SqlCommand command = new SqlCommand(saveQoute))
                           {
                               command.Connection = openCon;
                               command.Parameters.Add("@quoteText", SqlDbType.VarChar).Value = quotes.quoteText;
                               command.Parameters.Add("@author", SqlDbType.VarChar).Value = quotes.author;                            
                                openCon.Open();

                                command.ExecuteNonQuery();
                           }
                         }

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
      

            return false;
        }

        public List<Quotes> GetAllQuotes()
        {
            List<Quotes> listQoutes = new List<Quotes>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    string query = "Select quoteText ,author FROM Quotes";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listQoutes.Add(new Quotes { quoteText = reader.GetString(0), author = reader.GetString(1) });
                            }
                        }
                    }
                }

                return listQoutes;
            }
            catch (Exception ex)
            {                
                return new List<Quotes>
                {
                    new Quotes
                    {
                       quoteId = 2,
                       quoteText =ex.Message,
                       author = "Error"
                    }
                };
            }

        
        }

        //public List<Quotes> GetAllQuotes()
        //{
        //    var ctx = HttpContext.Current;

        //    if (ctx != null)
        //    {
        //        return (List<Quotes>)ctx.Cache[CacheKey];
        //    }

        //    return new List<Quotes>
        //{
        //    new Quotes
        //    {
        //       quoteId = 2,
        //       quoteText ="Placeholder",
        //       author = "Placeholder"
        //    }
        //};
        //}
    }
}