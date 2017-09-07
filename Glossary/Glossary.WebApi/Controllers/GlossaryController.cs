using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using AutoMapper;
using Glossary.Services.DTO;
using Glossary.Services.Interfaces;
using Glossary.WebApi.Models;
using Glossary.WebApi.Models.Glossary;

namespace Glossary.WebApi.Controllers
{
    //[EnableCors("http://localhost:60986", "*", "GET, POST, PUT, DELETE, OPTIONS")]
    public class GlossaryController : ApiController
    {
        private readonly IGlossaryRepository _glossaryRepository;
        private readonly IMapper _mapper;

        public GlossaryController(IGlossaryRepository glossaryRepository, IMapper mapper)
        {
            _glossaryRepository = glossaryRepository;
            _mapper = mapper;
        }

       
        [AcceptVerbs("GET")]
        [ResponseType(typeof(IEnumerable<GlossaryListResponse>))]
        public HttpResponseMessage Get()
        {

            var glossaryListResponse = new GlossaryListResponse();
            try
            {

                glossaryListResponse.Glossaries = _glossaryRepository.Get();
                glossaryListResponse.Code = InternalApiStatusCode.Success;

                return Request.CreateResponse(HttpStatusCode.OK, glossaryListResponse);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        // GET: api/glossary/5

        [HttpGet]
        [AcceptVerbs("GET")]
        [ResponseType(typeof(GlossaryResponse))]
        public HttpResponseMessage Get(int id)
        {

            if (id <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new BaseApiResponse
                {
                    Code = InternalApiStatusCode.FailedRequestValidation,
                    Message = "Couldn't read Card Number or it is not found"
                });
            }


            var glossaryResposne = new GlossaryResponse();
            try
            {
                var glossary = _glossaryRepository.Get(id);

                if (glossary == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new BaseApiResponse
                    {
                        Code = InternalApiStatusCode.Error,
                        Message = "glossary item is not found"
                    });

                glossaryResposne.Glossary = glossary;
                glossaryResposne.Code = InternalApiStatusCode.Success;
                glossaryResposne.Message = "Glossary item added";


                return Request.CreateResponse(HttpStatusCode.OK, glossaryResposne);
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


        // POST: api/glossary
       
        [AcceptVerbs("POST")]
        [ResponseType(typeof(GlossaryResponse))]
        public HttpResponseMessage Post([FromBody]GlossaryDto glossary)
        {
            if ((glossary == null))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new BaseApiResponse
                {
                    Code = InternalApiStatusCode.Error,
                    Message = "Invalid glossary object"
                });

            }

            if (string.IsNullOrEmpty(glossary.Definition) || string.IsNullOrEmpty(glossary.Term))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new BaseApiResponse
                {
                    Code = InternalApiStatusCode.FailedRequestValidation,
                    Message = "Invalid glossary object"
                });

            }

            var glossaryResposne = new GlossaryResponse();
            try
            {
                if (_glossaryRepository.FindTerm(glossary.Term.Trim()))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new BaseApiResponse
                    {
                        Code = InternalApiStatusCode.Conflict,
                        Message = "Glossary item already exists !"
                    });


                var glossaryResult = _glossaryRepository.Add(glossary);

                glossaryResposne.Glossary = glossaryResult;
                glossaryResposne.Code = InternalApiStatusCode.Success;

                return Request.CreateResponse(HttpStatusCode.Created, glossaryResposne);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


        // PUT: api/glossary


       
        [ResponseType(typeof(GlossaryResponse))]
        public HttpResponseMessage Put(int id, [FromBody]GlossaryRequest glossary)
        {

            if (string.IsNullOrEmpty(glossary.Definition) || string.IsNullOrEmpty(glossary.Term))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new BaseApiResponse
                {
                    Code = InternalApiStatusCode.FailedRequestValidation,
                    Message = "Invalid glossary object"
                });

            }
            
            var glossaryResposne = new GlossaryResponse();
            try
            {
                var glossaryResult = _glossaryRepository.Update(new GlossaryDto
                {
                    GlossaryId = id,
                    Definition = glossary.Definition,
                    Term = glossary.Term
                });

                if (glossaryResult == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new BaseApiResponse
                    {
                        Code = InternalApiStatusCode.Error,
                        Message = "Glossary item is not found"
                    });

                glossaryResposne.Glossary = glossaryResult;
                glossaryResposne.Code = InternalApiStatusCode.Success;
                glossaryResposne.Message = "Glossary item updated";

                return Request.CreateResponse(HttpStatusCode.OK, glossaryResposne);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


        // DELETE: api/glossary/5

       
        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(int id)
        {
            var glossary = _glossaryRepository.Get(id);

            if (glossary == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, new BaseApiResponse
                {
                    Code = InternalApiStatusCode.Error,
                    Message = "Glossary item is not found"
                });

            try
            {
                _glossaryRepository.Remove(id);

                return Request.CreateResponse(HttpStatusCode.OK, new BaseApiResponse
                {
                    Code = InternalApiStatusCode.Success,
                    Message = "Glossary item removed"
                });

            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            
        }

        //[HttpOptions]
        //public HttpResponseMessage Options()
        //{
        //    var response = new HttpResponseMessage();
        //    response.StatusCode = HttpStatusCode.OK;
        //    return response;
        //}

    }
}
