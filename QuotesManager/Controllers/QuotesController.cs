using QuotesManager.Models;
using QuotesManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuotesManager.Controllers
{
    public class QuotesController : ApiController
    {
        private QuotesRepository quotesRepository;


        public HttpResponseMessage Post(Quotes quotes)
        {
            this.quotesRepository.SaveQuotes(quotes);

            var response = Request.CreateResponse<Quotes>(System.Net.HttpStatusCode.Created, quotes);

            return response;
        }


        public QuotesController()
            {
                this.quotesRepository = new QuotesRepository();
            }

        public Quotes[] Get()
        {
            return quotesRepository.GetAllQuotes();
        }
    }
}
