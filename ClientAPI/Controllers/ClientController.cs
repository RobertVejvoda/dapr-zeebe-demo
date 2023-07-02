using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ClientAPI.Controllers;

[ApiController]
[Route("/api/clients")]
public class ClientController : ControllerBase
{
    private readonly ILogger<ClientController> logger;

    public ClientController(ILogger<ClientController> logger)
    {
        this.logger = logger;
    }

    [HttpPost("on-register")]
    public async Task<ActionResult<ClientResponse>> RegisterClient([FromBody] ClientRequest request)
    {
        await Task.Delay(1000);

        var message = $"Client {request.ClientId} registered.";
        logger.LogInformation(message);

        logger.LogInformation("Zeebe JobKey: {Value}", Request.Headers["X-Zeebe-Job-Key"]!);
        logger.LogInformation("Zeebe JobType: {Value}", Request.Headers["X-Zeebe-Job-Type"]!);
        logger.LogInformation("Zeebe ProcessInstanceKey: {Value}", Request.Headers["X-Zeebe-Process-Instance-Key"]!);
        logger.LogInformation("Zeebe BpmnProcessId: {Value}", Request.Headers["X-Zeebe-Bpmn-Process-Id"]!);
        logger.LogInformation("Zeebe ProcessDefinitionVersion: {Value}", Request.Headers["X-Zeebe-Process-Definition-Version"]!);
        logger.LogInformation("Zeebe ProcessDefinitionKey: {Value}", Request.Headers["X-Zeebe-Process-Definition-Key"]!);
        logger.LogInformation("Zeebe ElementId: {Value}", Request.Headers["X-Zeebe-Element-Id"]!);
        logger.LogInformation("Zeebe ElementInstanceKey: {Value}", Request.Headers["X-Zeebe-Element-Instance-Key"]!);
        logger.LogInformation("Zeebe Worker: {Value}", Request.Headers["X-Zeebe-Worker"]!);
        logger.LogInformation("Zeebe Retries: {Value}", Request.Headers["X-Zeebe-Retries"]!);
        logger.LogInformation("Zeebe Deadline: {Value}", Request.Headers["X-Zeebe-Deadline"]!);

        return Created("/", new ClientResponse(message));
    }

    [HttpPost("on-wait-here")]
    public async Task<ActionResult<NullResponse>> WaitHere()
    {
        logger.LogInformation("Long running job for 10 min., Worker JobKey: {Value}", Request.Headers["X-Zeebe-Job-Key"]!);

        await Task.Delay(TimeSpan.FromMinutes(10));

        return Ok(new NullResponse());
    }
}

public record ClientRequest([Required] int ClientId);

public record ClientResponse(string Result);

public record NullResponse();