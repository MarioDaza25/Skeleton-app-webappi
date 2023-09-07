using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;

[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class TpersonaController : BaseAPiController
{
       private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TpersonaController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TipoPersonaDto>>> Get()
    {
        var tpersona = await _unitOfWork.TipoPersonas.GetAllAsync();
        return _mapper.Map<List<TipoPersonaDto>>(tpersona);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<TipoPxPersonDto>>> Get11([FromQuery] Params tperParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.TipoPersonas.GetAllAsync(tperParams.PageIndex, tperParams.PageSize, tperParams.Search);
        var lstTPDto = _mapper.Map<List<TipoPxPersonDto>>(registros);
        return new Pager<TipoPxPersonDto>(lstTPDto, totalRegistros, tperParams.PageIndex, tperParams.PageSize, tperParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int id)
    {
        var tpersona = await _unitOfWork.TipoPersonas.GetByIdAsync(id);
        return Ok(tpersona);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoPersona>> Post(TipoPersonaDto tpersonaDto)
    {
        var tpersona = _mapper.Map<TipoPersona>(tpersonaDto);
        _unitOfWork.TipoPersonas.Add(tpersona);
        await _unitOfWork.SaveAsync();
        if (tpersonaDto == null)
        {
            return BadRequest();
        }
        tpersonaDto.Id = tpersona.Id;
        return CreatedAtAction(nameof(Post), new { id = tpersonaDto.Id }, tpersonaDto);
    }

    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoPersonaDto>> Put(int id, [FromBody] TipoPersonaDto tPersonaDto)
    {
        if (tPersonaDto == null)
        {
            return NotFound();
        }
        var tpersona = _mapper.Map<TipoPersona>(tPersonaDto);
        _unitOfWork.TipoPersonas.Update(tpersona);
        await _unitOfWork.SaveAsync();

        return tPersonaDto;
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var tpersona = await _unitOfWork.TipoPersonas.GetByIdAsync(id);
        if (tpersona == null)
        {
            return NotFound();
        }

        _unitOfWork.TipoPersonas.Remove(tpersona);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }
}