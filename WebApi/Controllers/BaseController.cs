using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected Guid GetUserId()
    {
        
        var foo = "10";
   
        var subClaim = User.Claims.FirstOrDefault(x => x.Type == "sub") 
                       ?? throw new Exception("Not valid JWT: sub not provided");
        
        var result = Guid.TryParse(subClaim.Value, out var id);
        if (!result)
        {
            throw new Exception("Not valid JWT: incorrect sub format");
        }

        return Guid.NewGuid();
    }
    
}
