using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories
{
    internal class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
    {
        private readonly CashFlowDbContext _ctx;
        public UserRepository(CashFlowDbContext context)
        {
            _ctx = context;
        }

        public async Task Add(User user)
        {
            await _ctx.Users.AddAsync(user);
        }
        public async Task<bool> ExistActiveUserWithEmail(string email)
        {
            return await _ctx.Users.AnyAsync(user => user.Email.Equals(email));
        }

        public async Task<User?> GetUserByEmail(string email) 
        {
            return await _ctx.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
        }
    }
}
