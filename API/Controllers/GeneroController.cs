using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;

[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class GeneroController : BaseAPiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GeneroController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GeneroDto>>> Get()
    {
        var generos = await _unitOfWork.Generos.GetAllAsync();
        return _mapper.Map<List<GeneroDto>>(generos);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<GeneroPersDto>>> Get11([FromQuery] Params genParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Generos.GetAllAsync(genParams.PageIndex, genParams.PageSize, genParams.Search);
        var lstGenDto = _mapper.Map<List<GeneroPersDto>>(registros);
        return new Pager<GeneroPersDto>(lstGenDto, totalRegistros, genParams.PageIndex, genParams.PageSize, genParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int id)
    {
        var genero = await _unitOfWork.Generos.GetByIdAsync(id);
        return Ok(genero);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Genero>> Post(GeneroPersDto generoDto)
    {
        var genero = _mapper.Map<Genero>(generoDto);
        _unitOfWork.Generos.Add(genero);
        await _unitOfWork.SaveAsync();
        if (generoDto == null)
        {
            return BadRequest();
        }
        generoDto.Id = genero.Id;
        return CreatedAtAction(nameof(Post), new { id = generoDto.Id }, generoDto);
    }

    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GeneroPersDto>> Put(int id, [FromBody] GeneroPersDto generoDto)
    {
        if (generoDto == null)
        {
            return NotFound();
        }
        var genero = _mapper.Map<Genero>(generoDto);
        _unitOfWork.Generos.Update(genero);
        await _unitOfWork.SaveAsync();

        return generoDto;
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var genero = await _unitOfWork.Generos.GetByIdAsync(id);
        if (genero == null)
        {
            return NotFound();
        }

        _unitOfWork.Generos.Remove(genero);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }
}
