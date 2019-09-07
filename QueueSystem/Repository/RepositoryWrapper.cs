using Entities;
using Repository.Interfaces;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IQueueRepository _queue;
        private IUserRepository _user;

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public IQueueRepository Queue {
            get
            {
                if (_queue == null)
                    _queue = new QueueRepository(_repoContext);

                return _queue;
            }

        }

        public IUserRepository User {
            get
            {
                if (_user == null)
                    _user = new UserRepository(_repoContext);

                return _user;
            }
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}
