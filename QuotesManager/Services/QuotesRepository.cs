using QuotesManager.Models;
using System;
using System.Collections.Generic;
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
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Quotes[])ctx.Cache[CacheKey]).ToList();
                    currentData.Add(quotes);
                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
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