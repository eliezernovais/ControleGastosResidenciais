using Backend.Data;
using Backend.Services.Interfaces;
namespace Backend.Services
{
    public class TotalService(AppDbContext context) : ITotalService
    {
    }
}
