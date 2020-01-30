using System;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Test
{
    public class CommandsTestsBase : IDisposable
    {
        protected readonly AppDbContext DbContext;

        public CommandsTestsBase()
        {
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            DbContext = new AppDbContext(options);
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
