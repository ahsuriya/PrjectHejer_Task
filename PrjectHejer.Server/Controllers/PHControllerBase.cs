using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PrjectHejer.Infrastructure.Generics;
using System.Collections;
using System.Reflection;

namespace PrjectHejer.Server.Controllers
{
    public class PHControllerBase : ControllerBase
    {
        protected IActionResult OkResponse<T>(T data, string message = "Request successful")
        {
            var response = new ResponseModel<T>(true, message, data);
            return Ok(response);
        }

        protected IActionResult CreatedResponse<T>(T data, string message = "Resource created successfully", string? location = null)
        {
            var response = new ResponseModel<object>(true, message, data);
            return Created(location, response);
        }

        protected IActionResult BadRequestResponse(string message = "An error occurred", object data = null)
        {
            var response = new ResponseModel<object>(false, message, data);
            return BadRequest(response);
        }

        protected IActionResult NotFoundResponse(string message = "Resource not found", object data = null)
        {
            var response = new ResponseModel<object>(false, message, data);
            return NotFound(response);
        }

        protected IActionResult DeletedResponse(string message = "Resource deleted successfully")
        {
            var response = new ResponseModel<object>(true, message, null);
            return NoContent();
        }

        protected IActionResult InternalServerErrorResponse(string message = "An internal server error occurred", object data = null)
        {
            var response = new ResponseModel<object>(false, message, data);
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        [NonAction]
        public virtual OkObjectResult Ok([ActionResultObjectValue] object? value)
        {
            object transformed;

            if (value == null)
            {
                transformed = new { data = (object?)null };
            }
            else if (value is IEnumerable enumerable && value.GetType() != typeof(string))
            {
                var items = enumerable.Cast<object?>().Select(TransformObject).ToList();
                transformed = new
                {
                    data = new
                    {
                        items,
                        count = items.Count
                    }
                };
            }
            else if (IsComplexType(value))
            {
                transformed = new { data = TransformObject(value) };
            }
            else
            {
                transformed = new { data = new { value } };
            }

            return new OkObjectResult(transformed);
        }

        private object? TransformObject(object? obj)
        {
            if (obj == null || !IsComplexType(obj))
                return obj;

            var type = obj.GetType();
            var dict = new Dictionary<string, object?>();

            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                dict[prop.Name] = TransformObject(prop.GetValue(obj));
            }

            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                dict[field.Name] = TransformObject(field.GetValue(obj));
            }

            return dict;
        }

        private bool IsComplexType(object obj)
        {
            if (obj == null) return false;
            var type = obj.GetType();

            // Collections are handled separately
            if (typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string))
                return false;

            return type.IsClass && type != typeof(string);
        }
    }
}
