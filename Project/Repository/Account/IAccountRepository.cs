using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Repository.Account
{
    public interface IAccountRepository : IDisposable
    {
        bool CheckEmail(string email);
        bool CheckOOP(string oop,string email);
        void SendOOPByEmail(string email);
        void SendNotifyEmailIncorrectOOP(string email);
    }
}