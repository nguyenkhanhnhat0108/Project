using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Repository.Account
{
    public interface IAccountRepository : IDisposable
    {
        void CheckEmailandSendOOP(string email);
        bool CheckOOP(string oop,string email);
    }
}