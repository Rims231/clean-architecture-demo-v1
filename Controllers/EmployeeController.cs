using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;

namespace clean_architecture_demo_v1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IMediator mediator, IEmployeeRepository employeeRepository)
        {
            _mediator = mediator;
            _employeeRepository = employeeRepository;
        }

        // GET: api/employee
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            return Ok(employees);
        }

        // GET: api/employee/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound($"Employee with ID {id} was not found.");

            return Ok(employee);
        }

        // POST: api/employee — uses MediatR
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeEntity employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _mediator.Send(new AddEmployeeCommand(employee));
            return CreatedAtAction(nameof(GetEmployeeById), new { id = created.Id }, created);
        }

        // PUT: api/employee/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeEntity employee)
        {
            if (id != employee.Id)
                return BadRequest("ID in URL does not match ID in request body.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (existing == null)
                return NotFound($"Employee with ID {id} was not found.");

            await _employeeRepository.UpdateEmployeeAsync(employee);
            return Ok(employee);
        }

        // DELETE: api/employee/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound($"Employee with ID {id} was not found.");

            await _employeeRepository.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
}