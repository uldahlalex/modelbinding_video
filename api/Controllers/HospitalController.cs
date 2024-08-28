using Microsoft.AspNetCore.Mvc;
using service;
using videoprep.Models;

namespace api.Controllers;

[Route("api/[controller]")]
public class HospitalController(IGenericService service) : ControllerBase
{
    [Route("patients")]
    public ActionResult<IEnumerable<Patient>> GetPatients([FromQuery] string orderBy)
    {
        var patients = service.GetAll<Patient>(null, orderBy);
        return Ok(patients);
    }

    [HttpPost]
    [Route("patients")]
    public ActionResult<Patient> CreatePatient(
        [FromBody] CreatePatientDto patientDto,
        [FromHeader] string authenticationToken
        )
    {
        if (string.IsNullOrWhiteSpace(authenticationToken))
        {
            return Unauthorized("no token");
        }
        var p = new Patient() { Name = patientDto.Name };
        service.Add<Patient>(p);
        return Ok(p);
    }
}

public class CreatePatientDto
{
    public string Name { get; set; }
}