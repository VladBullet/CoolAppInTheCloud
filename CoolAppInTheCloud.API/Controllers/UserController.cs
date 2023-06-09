﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CoolAppInTheCloud;
using CoolAppInTheCloud.Data.Models;
using CoolAppInTheCloud.Helpers_Extensions;
using CoolAppInTheCloud.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = AuthorizationPolicies.Admin)]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("getUserById/{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpGet("getUserByUsername/{username}")]
    public async Task<IActionResult> GetUserByUsername(string username)
    {
        var user = _userService.GetUserByUsername(username);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpGet("getAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = _userService.GetAllUsers();
        return Ok(users);
    }

    [HttpPost("createUser")]
    public async Task<IActionResult> CreateUser(User user)
    {
        _userService.CreateUser(user);
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    [HttpPut("updateUser/{id}")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        if (id != user.Id) // make sure the id didn't change
        {
            return BadRequest();
        }

        // make sure the password is hashed on update
        if ((_userService.GetUserById(user.Id)).Password != user.Password)
        {
            user.Password = user.Password.ToMd5();
        }

        _userService.UpdateUser(user);
        return NoContent();
    }

    [HttpDelete("deleteUser/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }

        _userService.DeleteUser(user);
        return NoContent();
    }
}
