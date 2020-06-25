using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Project.Repository.Account
{
    public class AccountRepository : IAccountRepository
    {
        private Project_V0212Entities db = new Project_V0212Entities();

        public void CheckEmailandSendOOP(string email)
        {
            if (!String.IsNullOrEmpty(email))
            {
                var account = db.Accounts.FirstOrDefault(x => x.Email.Equals(email));
                if(account != null)
                {
                    string oop = RandomString(5);
                    account.OOP = oop;
                    db.Entry(account).State = EntityState.Modified;
                    db.SaveChanges();
                    //SendMail(oop, "aa");
                }
            }
        }

        public bool CheckOOP(string oop,string email)
        {
            if (!String.IsNullOrWhiteSpace(oop))
            {
                var account = db.Accounts.FirstOrDefault(x => x.Email.Equals(email) && x.OOP.Equals(oop));
                if(account != null)
                {
                    account.OOP = String.Empty;
                    account.LastLogin = DateTime.Now;
                    db.Entry(account).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private void SendMail(string oop,string email)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.googlemail.com";
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("nhatnk01081993@gmail.com", "niemtin0108");
            string message = "Your CODE : " + oop;
            client.Send("nhatnk01081993@gmail.com","nguyenkhanhnhat0108@gmail.com","CODE OOP",message);
        }
        private string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);
            Random _random = new Random();

            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}