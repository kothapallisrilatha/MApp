using System;
using System.Threading.Tasks;
using mapp.api.Models;
using Microsoft.EntityFrameworkCore;

namespace mapp.api.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context )
        {
            _context=context;
        }
        public async Task<Users> LogIn(string userName, string password)
        {
            var user=await _context.User.FirstOrDefaultAsync(x=>x.Username==userName);
            if(user==null)
            return null;

            if(!VerifyPasswordHas(password,user.PasswordHash,user.PasswordSalt))
            return null;

            return user;
            
        }

        private bool VerifyPasswordHas(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }
        }

        public async Task<Users> Register(Users users, string password)
        {
            byte[] passwordHash,passwordSalt;
            CreatePasswordHash(password,out passwordHash,out passwordSalt);
            users.PasswordHash=passwordHash;
            users.PasswordSalt=passwordSalt;
            await _context.User.AddAsync(users);
            await _context.SaveChangesAsync();
            return users;

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt){

            using(var hmac=new System.Security.Cryptography.HMACSHA512()){
                passwordSalt=hmac.Key;
                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            };
        }

        public async Task<bool> UserExists(string userName)
        {
            if(await _context.User.AnyAsync(x=>x.Username==userName))
            return true;

            return false;
        }
    }
}