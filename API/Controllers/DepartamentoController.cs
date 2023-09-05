using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class DepartamentoController : BaseAPiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DepartamentoController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DepartamentoDto>>> Get()
    {
        var departamentos = await _unitOfWork.Departamentos.GetAllAsync();
        return _mapper.Map<List<DepartamentoDto>>(departamentos);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<DepxCiudadDto>>> Get11([FromQuery] Params depParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Departamentos.GetAllAsync(depParams.PageIndex, depParams.PageSize, depParams.Search);
        var lstDepDto = _mapper.Map<List<DepxCiudadDto>>(registros);
        return new Pager<DepxCiudadDto>(lstDepDto, totalRegistros, depParams.PageIndex, depParams.PageSize, depParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int id)
    {
        var departamento = await _unitOfWork.Departamentos.GetByIdAsync(id);
        return Ok(departamento);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Departamento>> Post(DepartamentoDto departamentoDto)
    {
        var departamento = _mapper.Map<Departamento>(departamentoDto);
        _unitOfWork.Departamentos.Add(departamento);
        await _unitOfWork.SaveAsync();
        if (departamentoDto == null)
        {
            return BadRequest();
        }
        departamentoDto.Id = departamento.Id;
        return CreatedAtAction(nameof(Post), new { id = departamentoDto.Id }, departamentoDto);
    }

    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DepartamentoDto>> Put(int id, [FromBody] DepartamentoDto departamentoDto)
    {
        if (departamentoDto == null)
        {
            return NotFound();
        }
        var departamento = _mapper.Map<Departamento>(departamentoDto);
        _unitOfWork.Departamentos.Update(departamento);
        await _unitOfWork.SaveAsync();

        return departamentoDto;
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var departamento = await _unitOfWork.Departamentos.GetByIdAsync(id);
        if (departamento == null)
        {
            return NotFound();
        }

        _unitOfWork.Departamentos.Remove(departamento);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }
}