using HNGx.Abstractions;
using HNGx.Constants;
using HNGx.Data;
using HNGx.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HNGx.EndpointDefinitions
{
    public class PersonEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var auth = app.MapGroup("/api/Person").WithOpenApi();

            auth.MapGet("/get-all-persons", GetPersons)
                
                .WithOpenApi();

            auth.MapGet("/", GetPersons)
                .WithName("GetPersonById")
                .WithOpenApi();

            auth.MapPost("/", PostPerson).
                WithOpenApi();

            auth.MapPut("/", UpdatePerson).
                WithOpenApi();

            auth.MapDelete("/", DeletePerson).
                    WithOpenApi();
        }

        public async Task<ActionResult<IEnumerable<Person>>> GetPersons(HNGxDbContext dbContext)
        {
            return await dbContext.Persons.ToListAsync();
        }
        public async Task<IResult> GetPersonById(HNGxDbContext dbContext, int id)
        {
            if (id == 0)
            {
                return TypedResults.BadRequest(ErrorConstants.PERSON_ID_ERROR);
            }
            var person = await dbContext.Persons.FindAsync(id);

            if (person == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(person);
        }
        public async Task<IResult> PostPerson(HNGxDbContext dbContext, PersonRequest personRequest)
        {
            if(personRequest.Name == null)
            {
                return TypedResults.BadRequest(ErrorConstants.PERSON_NAME_ERROR);
            }
            var person = new Person() { Name = personRequest.Name };
            dbContext.Persons.Add(person);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute("GetPersonById", new { person.Id }, person);
        }
        private async Task<IResult> UpdatePerson(HNGxDbContext dbContext, Person person)
        {
            if (person.Name == null)
            {
                return TypedResults.BadRequest(ErrorConstants.PERSON_NAME_ERROR);
            }
            else if(person.Id == 0)
            {
                return TypedResults.BadRequest(ErrorConstants.PERSON_ID_ERROR);
            }
            dbContext.Persons.Update(person);
            await dbContext.SaveChangesAsync();
            return TypedResults.Ok(person);
        }

        private async Task<IResult> DeletePerson(HNGxDbContext dbContext, int id)
        {
            if(id == 0)
            {
                return TypedResults.BadRequest(ErrorConstants.PERSON_ID_ERROR);
            }
            var person = await dbContext.Persons.FindAsync(id);
            if (person == null) 
            {
                return TypedResults.Ok();
            }
            dbContext.Persons.Remove(person);
            await dbContext.SaveChangesAsync();
            return TypedResults.Ok();
        }
    }
}
