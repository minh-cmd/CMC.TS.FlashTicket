namespace CMC.TS.FT.Api.HelperClass
{
    public static class JwtExtract
    {
        public static Guid ExtractUserId(string? JwtUserId)
        {
            if (JwtUserId == null) throw new ArgumentNullException(nameof(JwtUserId));

            if(Guid.TryParse(JwtUserId, out Guid userId) == false)
            {
                throw new FormatException($"The provided JWT User ID '{JwtUserId}'' is not a valid Guid.");
            }
            return userId;
        }
    }
}
