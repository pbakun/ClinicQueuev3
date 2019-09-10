using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepositoryWrapper
    {
        IQueueRepository Queue { get; }
        IUserRepository User { get; }
        void Save();
        Task SaveAsync();
    }
}
