using DriverHire.Data.Context;
using DriverHire.Repository.Interfaces;
using DriverHire.Repository.Repository;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Repository
{
    public class UnitofWork : IUnitofWork
    {

        private readonly DriverHireContext _context;
        private IDbContextTransaction transaction;
        public UnitofWork(DriverHireContext context)
        {
            _context = context;
        }
        //unit of work main //
        public async Task<int> SaveAsync()
        => await _context.SaveChangesAsync();
        public IDbContextTransaction CurrentTransaction => transaction;
        public async Task BeginTransactionAsync()
        {
            transaction ??= await _context.Database.BeginTransactionAsync();
        }
        public async Task CommitAsync()
        {
            await transaction?.CommitAsync();
        }
        public async Task RollbackAsync()
        {
            await transaction?.RollbackAsync();
        }
        

    }
}

