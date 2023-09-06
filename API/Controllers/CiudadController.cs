using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;

[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class CiudadController : BaseAPiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CiudadController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CiudadDto>>> Get()
    {
        var ciudades = await _unitOfWork.Ciudades.GetAllAsync();
        return _mapper.Map<List<CiudadDto>>(ciudades);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<CiudadPersDto>>> Get11([FromQuery] Params ciuParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Ciudades.GetAllAsync(ciuParams.PageIndex, ciuParams.PageSize, ciuParams.Search);
        var lstCiuDto = _mapper.Map<List<CiudadPersDto>>(registros);
        return new Pager<CiudadPersDto>(lstCiuDto, totalRegistros, ciuParams.PageIndex, ciuParams.PageSize, ciuParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int id)
    {
        var ciudad = await _unitOfWork.Ciudades.GetByIdAsync(id);
        return Ok(ciudad);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Ciudad>> Post(CiudadPersDto ciudadDto)
    {
        var ciudad = _mapper.Map<Ciudad>(ciudadDto);
        _unitOfWork.Ciudades.Add(ciudad);
        await _unitOfWork.SaveAsync();
        if (ciudadDto == null)
        {
            return BadRequest();
        }
        ciudadDto.Id = ciudad.Id;
        return CreatedAtAction(nameof(Post), new { id = ciudadDto.Id }, ciudadDto);
    }

    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CiudadPersDto>> Put(int id, [FromBody] CiudadPersDto ciudadDto)
    {
        if (ciudadDto == null)
        {
            return NotFound();
        }
        var ciudad = _mapper.Map<Ciudad>(ciudadDto);
        _unitOfWork.Ciudades.Update(ciudad);
        await _unitOfWork.SaveAsync();

        return ciudadDto;
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var ciudad = await _unitOfWork.Ciudades.GetByIdAsync(id);
        if (ciudad == null)
        {
            return NotFound();
        }

        _unitOfWork.Ciudades.Remove(ciudad);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }
}
