using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IRepositoryWrapper
    {
        IQueueRepository Queue { get; }
        IUserRepository User { get; }
        void Save();
    }
}
