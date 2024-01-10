using Microsoft.AspNetCore.Mvc;

namespace EFCoreProjetoFinal.Controllers
{
    public abstract class MainController : ControllerBase
    {
        protected ActionResult CustomResponse(bool isSuccess, object result = null)
        {
            if (isSuccess)
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = result
            });
        }
    }
}
