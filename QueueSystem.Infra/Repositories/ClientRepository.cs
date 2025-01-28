using Microsoft.EntityFrameworkCore;
using QueueSystem.Domain.Models;
using QueueSystem.Infra.Data;
using QueueSystem.Infra.Repositories.Interfaces;

namespace QueueSystem.Infra.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationContext _context;

        public ClientRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ClientModel client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }

        public Task<ClientModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ClientModel>> GetByQueueIdAsync(int queueId)
        {
            return await _context.Clients
                .Where(c => c.QueueId == queueId)
                .OrderBy(c => c.Priority)
                .ThenBy(c => c.Status)
                .ToListAsync();
        }

        public async Task UpdateAsync(ClientModel client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }
    }
}