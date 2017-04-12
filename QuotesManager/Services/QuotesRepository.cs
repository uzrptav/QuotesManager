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

        public QuotesRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var contacts = new Quotes[]
                    {                      
		                    new Quotes
                            {
                                quoteId = 1,
                                quoteText ="Скажи мне, кто тебя хвалит, и я тебе скажу, в чем ты ошибся.",
                                author = "Владимир Ильич Ленин"
                            },
                            new Quotes
                            {
                                quoteId = 2,
                                quoteText ="Деньги — это сводник между потребностью и предметом, между жизнью и жизненными средствами человека.",
                                author = "Карл Маркс"
                            },
                            new Quotes
                            {
                                quoteId = 3,
                                quoteText ="Хуй и пизда - из одного гнезда.",
                                author = "Виталыч"
                            }
                    };

                    ctx.Cache[CacheKey] = contacts;
                }
            }
        }

        public bool SaveQuotes(Quotes quotes)
        {           
                try
                {
                    using (SqlConnection openCon = new SqlConnection("workstation id=FreeCodeCamp.mssql.somee.com;packet size=4096;user id=uzrptav_SQLLogin_1;pwd=amvcsm5bx4;data source=FreeCodeCamp.mssql.somee.com;persist security info=False;initial catalog=FreeCodeCamp"))
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

        public Quotes[] GetAllQuotes()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Quotes[])ctx.Cache[CacheKey];
            }

            return new Quotes[]
        {
            new Quotes
            {
               quoteId = 2,
               quoteText ="Placeholder",
               author = "Placeholder"
            }
        };
        }
    }
}