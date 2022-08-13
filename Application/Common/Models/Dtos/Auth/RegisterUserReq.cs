using Caffe.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Caffe.Application.Common.Models.Dtos.Auth;

public class RegisterUserReq
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
    public IFormFile? SnapShot { get; set; }
    public Gender Gender { get; set; }
    public string PhoneNumber { get; set; }
    public Guid CityId { get; set; }
}