using Inmobiliaria.Application.DTOs.Propierty;
using Inmobiliaria.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Api.Controllers;

[ApiController]
[Route("api/propierty")]
public class PropiertyController : ControllerBase
{
    private readonly IPropiertyService _service;

    public PropiertyController(IPropiertyService service)
    {
        _service = service;
    }

    // Todos pueden ver las propiedades (Cliente y Admin)
    [HttpGet]
    [Authorize(Roles = "Admin,Client")]
    public async Task<IActionResult> GetAll()
    {
        var list = await _service.GetAll();
        return Ok(list);
    }

    // Ver detalles
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Client")]
    public async Task<IActionResult> GetById(int id)
    {
        var property = await _service.GetById(id);
        if (property == null)
            return NotFound(new { error = "Propiedad no encontrada" });

        return Ok(property);
    }

    // Crear propiedad (solo Admin)
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromForm] PropiertyCreateRequest dto)
    {
        var result = await _service.CreatePropierty(dto);
        return Ok(result);
    }

    // Actualizar propiedad (solo Admin)
    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(PropiertyUpdateRequest dto)
    {
        var result = await _service.UpdatePropierty(dto);
        return Ok(result);
    }

    // Eliminar propiedad (solo Admin)
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeletePropierty(id);
        return Ok(result);
    }
}