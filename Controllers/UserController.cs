using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DTOs;
using OnlineShop.Models;

namespace OnlineShop.Controllers;

[ApiController]
[Route("Users")]
public class UserController(ApplicationDbContext db) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrUpdateUserDTO request, CancellationToken cancellationToken)
    {
        OnlineShopUser? entity = OnlineShopUser.Create(
            request.Name,
            request.LastName,
            request.PhoneNumber,
            request.NationalCode
            );

        await db.AddAsync(entity, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreateOrUpdateUserDTO request, CancellationToken cancellationToken)
    {
        OnlineShopUser? entity = await db.Users.FindAsync([id], cancellationToken: cancellationToken);
        if (entity is null)
            return NotFound();

        entity.Update(
            request.Name,
            request.LastName,
            request.PhoneNumber,
            request.NationalCode,
            request.IsActive
            );

        db.Update(entity);
        await db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        OnlineShopUser? user = await db.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (user is null)
            return NotFound();

        db.Remove(user);
        await db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        OnlineShopUser? user = await db.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (user is null)
            return NotFound();

        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] string? q, [FromQuery] bool? isActive, CancellationToken cancellationToken)
    {
        IQueryable<OnlineShopUser> query = db.Users.AsNoTracking();

        if (string.IsNullOrWhiteSpace(q) is false)
            query.Where(model =>
                model.Name.Contains(q) ||
                model.LastName.Contains(q) ||
                model.NationalCode.Contains(q) ||
                model.PhoneNumber.Contains(q)
            );

        if (isActive.HasValue)
            query.Where(model => model.IsActive.Equals(isActive));

        List<OnlineShopUser> users = await query.ToListAsync(cancellationToken);

        return Ok(users);
    }



    [HttpPut("{id}/ToggleActivation")]
    public async Task<IActionResult> ToggleActivation([FromRoute] int id, CancellationToken cancellationToken)
    {
        OnlineShopUser? user = await db.Users.FindAsync([id], cancellationToken: cancellationToken);
        if (user is null)
        {
            return NotFound();
        }

        user.ToggleActivation();

        db.Update(user);
        await db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}