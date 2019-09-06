using Entities;
using Entities.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public class QueueRepository : RepositoryBase<Queue>, IQueueRepository
    {
        public QueueRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

    }
}
