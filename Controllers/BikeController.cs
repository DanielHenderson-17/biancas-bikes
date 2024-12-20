using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BiancasBikes.Data;
using Microsoft.EntityFrameworkCore;
using BiancasBikes.Models;
using BiancasBikes.Models.DTOs;

namespace BiancasBikes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BikeController : ControllerBase
{
    private BiancasBikesDbContext _dbContext;

    public BikeController(BiancasBikesDbContext context)
    {
        _dbContext = context;
    }

    // [HttpGet]
    // // [Authorize]
    // public IActionResult Get()
    // {
    //     return Ok(_dbContext
    //         .Bikes
    //         .Select(b => new BikeDTO
    //         {
    //             Id = b.Id,
    //             Brand = b.Brand,
    //             Color = b.Color,
    //             BikeTypeId = b.BikeTypeId,
    //             OwnerId = b.OwnerId
    //         })
    //         .ToList());
    // }
    [HttpGet]
    //[Authorize]
    public IActionResult Get()
    {
        return Ok(_dbContext.Bikes.Include(b => b.Owner).ToList());
    }

    [HttpGet("inventory")]
    [Authorize]
    public IActionResult Inventory()
    {
        int inventory = _dbContext
        .Bikes
        .Where(b => b.WorkOrders.Any(wo => wo.DateCompleted == null))
        .Count();

        return Ok(inventory);
    }
}