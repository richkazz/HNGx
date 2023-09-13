using HNGx.Abstractions;
using HNGx.Constants;
using HNGx.Data;
using HNGx.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace HNGx.EndpointDefinitions
{
    public class PersonEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var auth = app.MapGroup("/api").WithOpenApi();

            auth.MapGet("/get-all-persons", GetPersons)
                .Produces((int)HttpStatusCode.OK,typeof(List<Person>))
                .WithOpenApi();

            auth.MapGet("/{id}", GetPersonById)
                .WithName("GetPersonById")
                .WithOpenApi();

            auth.MapPost("/", PostPerson)
                .AddEndpointFilter<PostPersonFilter>()
                . WithOpenApi();

            auth.MapPut("/", UpdatePerson)
                .AddEndpointFilter<PutPersonFilter>()
                .WithOpenApi();

            auth.MapDelete("/{id}", DeletePerson).
                    WithOpenApi();
        }
        [ProducesResponseType(typeof(List<Person>), StatusCodes.Status200OK)]
        public async Task<IResult> GetPersons(HNGxDbContext dbContext)
        {
            var response = await dbContext.Persons.ToListAsync();
            return TypedResults.Ok(response);
        }
        public async Task<Results<Ok<Person>, NotFound<ErrorResponse>>> GetPersonById(HNGxDbContext dbContext, int id)
        {
            var person = await dbContext.Persons.FindAsync(id);
            if (person == null)
            {
                return TypedResults.NotFound(new ErrorResponse(ErrorConstants.PERSON_NOT_FOUND_ERROR));
            }
            return TypedResults.Ok(person);
        }
        public async Task<IResult> PostPerson(HNGxDbContext dbContext, PersonRequest personRequest)
        {
            var person = new Person() { Name = personRequest.Name };
            dbContext.Persons.Add(person);
            await dbContext.SaveChangesAsync();
            return Results.CreatedAtRoute("GetPersonById",new { person.Id},person);
        }
        private async Task<Results<Ok<Person>,NotFound<ErrorResponse>>> UpdatePerson(HNGxDbContext dbContext, Person person)
        {
            var per = await dbContext.Persons.FirstOrDefaultAsync(x => x.Id == person.Id);
            if (per == null)
            {
                return TypedResults.NotFound(new ErrorResponse(ErrorConstants.PERSON_NOT_FOUND_ERROR));
            }
            per.Name = person.Name;
            dbContext.Persons.Update(per);
            await dbContext.SaveChangesAsync();
            return TypedResults.Ok(person);
        }
       
        private async Task<Results<Ok,NotFound<ErrorResponse>>> DeletePerson(HNGxDbContext dbContext, int id)
        {
            var person = await dbContext.Persons.FindAsync(id);
            if (person == null) 
            {
                return TypedResults.NotFound(new ErrorResponse(ErrorConstants.PERSON_NOT_FOUND_ERROR));
            }
            dbContext.Persons.Remove(person);
            await dbContext.SaveChangesAsync();
            return TypedResults.Ok();
        }
    }
    public class PostPersonFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync
            (EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            var personRequest = context.GetArgument<PersonRequest>(1);
            if (string.IsNullOrWhiteSpace(personRequest.Name))
                return await Task.FromResult(Results.BadRequest(new ErrorResponse(ErrorConstants.PERSON_NAME_ERROR)));

            return await next(context);
        }
    }
    public class PutPersonFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync
            (EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            var person = context.GetArgument<Person>(1);
            if (string.IsNullOrWhiteSpace(person.Name))
                return await Task.FromResult(Results.BadRequest(new ErrorResponse(ErrorConstants.PERSON_NAME_ERROR)));
            else if(person.Id == 0)
                return await Task.FromResult(Results.BadRequest(new ErrorResponse(ErrorConstants.PERSON_ID_ERROR)));
            return await next(context);
        }
    }
    public class ErrorResponse
    {
       public  ErrorResponse(string error)
       {
            Error = error;
       }
       public string Error { get; set; }
    }
}

