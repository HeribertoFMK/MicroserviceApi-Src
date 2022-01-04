using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Interfaces.Services.User;
using System;
using System.Net;
using Api.Domain.Entities;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IUserService service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(await service.GetAll());

            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [HttpGet]
        [Route("(id)", Name = "GetWithId")]
        public async Task<ActionResult> Get(Guid id, [FromServices] IUserService service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            try
            {
                return Ok(await service.Get(id));

            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserEntity user, [FromServices] IUserService service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await service.Post(user);
                if (result != null)
                {
                    return Created(
                        new Uri(Url.Link("GetWithId", new { id = result.Id })), result

                    );

                }
                else
                {
                    return BadRequest();
                }

            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserEntity user, [FromServices] IUserService service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await service.Put(user);
                if (result != null)
                {
                    return Ok(result);

                }
                else
                {
                    return BadRequest();
                }

            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, [FromServices] IUserService service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {


                return Ok(await service.Delete(id));

            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}




