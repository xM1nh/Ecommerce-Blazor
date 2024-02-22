using Server.Infrastructure.Common;
using Server.Infrastructure.Interfaces;
using SharedLibrary.Models;

namespace Server.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
}
