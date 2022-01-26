using Doosy.Domain.Constants;
using Doosy.Domain.Entities;
using Doosy.Domain.Interfaces;
using Doosy.Domain.Requests;
using Doosy.Domain.Requests.Filters;
using Doosy.Domain.Responses;
using Doosy.Framework.Domain;
using Doosy.Framework.Domain.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Doosy.Domain.Services
{
    public class PersonService : ServiceBase, IPersonService
    {

        protected ICommandRepository<Person> commandRepository;
        protected IQueryRepository<Person, PersonFilter> queryRepository;
        IValidatorBase<PersonRequest> validator;
        IExcelExporter excelExporter;
        public PersonService(
            ICommandRepository<Person> commandRepository,
            IQueryRepository<Person, PersonFilter> queryRepository,
            IValidatorBase<PersonRequest> validator,
            IExcelExporter excelExporter,ILogger<PersonService> logger) :base(logger)
        {
            this.commandRepository = commandRepository;
            this.queryRepository = queryRepository;
            this.validator = validator;
            this.excelExporter = excelExporter;
        }

        public override string EntityName => EntityNames.PersonEntity;
        public CreationResponse Create(PersonRequest request)
        {
            var response = new CreationResponse();
            try
            {
                var validationresults = this.validator.Validate(request);
                if (!validationresults.Any())
                {
                    var person = new Person();

                        person.Id = System.Guid.NewGuid().ToString();
                        person.Firstname = request.Firstname;
                        person.Surname = request.Surname;
                        person.Gender = request.Gender;
                        person.CreatedBy = request.UserId;

                    commandRepository.Create(person);

                    var IdValue = person.GetType().GetProperty("Id").GetValue(person, null);

                    PopulateId(response, IdValue);
                    response.Id = IdValue;
                    response.IsSuccessful = true;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Messages = validationresults;
                }
            }
            catch (Exception ex)
            {
                LogError(response, ex, $"Unknown Servere Error occured.");
            }

            return response;
        }
        protected virtual void PopulateId(CreationResponse response, object idValue)
        {

        }
        public ExportResponse Export(PersonFilter filter)
        {
            var response = new ExportResponse();

            try
            {
                var filterResults = queryRepository.Filter(filter);

                response.Url = excelExporter.Export("Export", filterResults);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                LogError(response, ex, $"Error occured when Exporting data.");
            }
            return response;
        }
        public PagedDataResponce<PersonListResponse> Filter(PersonFilter filter)
        {
            var response = new PagedDataResponce<PersonListResponse>();

            var results = new List<PersonListResponse>();

            try
            {
                var filterResults = queryRepository.Filter(filter).ToList();

                foreach (var item in filterResults)

                    results.Add(new PersonListResponse
                    {
                        Id = item.Id,
                        Firstname = item.Firstname,
                        Surname = item.Surname,
                        CreatedBy = item.CreatedBy,
                        DateCreated = item.DateCreated.Value.ToString("dd MMM yyyy")
                    });

                response = results.Paginate(filter.Page);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                LogError(response, ex, "Error occured when filtering data.");
            }
            return response;
        }
        public ObjectResponse<Person> GetById(object id)
        {
            var response = new ObjectResponse<Person>();

            try
            {
                var filterResults = queryRepository.GetById(id);
                response.Data = filterResults;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                LogError(response, ex, "Error occured reading data.");
            }
            return response;
        }
        public ResponseBase Update(PersonRequest request)
        {
            var response = new ResponseBase();
            try
            {

                var validationresults = this.validator.Validate(request);
                if (!validationresults.Any())
                {
                    var person = queryRepository.GetById(request.Id);

                    person.Firstname = request.Firstname;
                    person.Surname = request.Surname;
                    person.Gender = request.Gender;

                    commandRepository.Update(person);
                    response.IsSuccessful = true;

                }
                else
                {
                    response.IsSuccessful = false;
                    response.Messages = validationresults;
                }
            }
            catch (Exception ex)
            {
                LogError(response, ex, $"Unknown Servere Error occured.");
            }

            return response;
        }
    }
}
