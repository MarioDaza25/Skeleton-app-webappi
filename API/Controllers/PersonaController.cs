using API.Dtos;
using API.Helpers;
using API.Service;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class PersonaController : BaseAPiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public PersonaController(IUnitOfWork unitOfWork,IMapper mapper, IUserService userService )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userService = userService;
    }


    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> Get()
    {
        var personas = await _unitOfWork.Personas.GetAllAsync();
        return _mapper.Map<List<PersonaDto>>(personas);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PersonaFullDto>>> Get11([FromQuery] Params personaParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Personas.GetAllAsync(personaParams.PageIndex, personaParams.PageSize, personaParams.Search);
        var lstPersonaDto = _mapper.Map<List<PersonaFullDto>>(registros);
        return new Pager<PersonaFullDto>(lstPersonaDto, totalRegistros, personaParams.PageIndex, personaParams.PageSize, personaParams.Search);
    } 


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int id)
    {
        var persona = await _unitOfWork.Personas.GetByIdAsync(id);
        return Ok(persona);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Persona>> Post(PersonaDto personaDto)
    {
        var persona = _mapper.Map<Persona>(personaDto);
        _unitOfWork.Personas.Add(persona);
        await _unitOfWork.SaveAsync();
        if (personaDto == null)
        {
            return BadRequest();
        }
        personaDto.Id = persona.Id;
        return CreatedAtAction(nameof(Post), new { id = personaDto.Id }, personaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PersonaDto>> Put(int id, [FromBody] PersonaDto personaDto)
    {
        if (personaDto == null)
        {
            return NotFound();
        }
        var persona = _mapper.Map<Persona>(personaDto);
        _unitOfWork.Personas.Update(persona);
        await _unitOfWork.SaveAsync();

        return personaDto;
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var persona = await _unitOfWork.Personas.GetByIdAsync(id);
        if (persona == null)
        {
            return NotFound();
        }

        _unitOfWork.Personas.Remove(persona);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }
}
