using System.Threading.Tasks;
using Core.Entities.Identity;

namespace Core.Interfaces
{
    public interface ITokenServices
    {
        string GenerateToken(AppUser user);
    }
}