using Application.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    internal class MyRepositoryAsync<T> :RepositoryBase<T>, IrepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public MyRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
