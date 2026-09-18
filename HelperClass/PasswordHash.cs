using Microsoft.AspNetCore.Identity;

namespace CMC.TS.FT.Api.HelperClass
{
    public static class PasswordHash
    {
        private readonly static PasswordHasher<object> passwordHash = new();
        public static string Hash(string password)
        {
            return passwordHash.HashPassword(null, password);
        }
        public static bool Verify(string hashedPassword, string providedPassword)
        {
            PasswordVerificationResult result= passwordHash.VerifyHashedPassword(null, hashedPassword, providedPassword);
            if (result == PasswordVerificationResult.Failed) 
            {
                return false;
            }
            return true;
        }
    }
}
