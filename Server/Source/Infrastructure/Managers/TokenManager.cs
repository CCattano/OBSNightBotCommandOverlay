using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using Torty.Web.Apps.ObsNightBotOverlay.Infrastructure.Utils;

namespace Torty.Web.Apps.ObsNightBotOverlay.Infrastructure.Managers
{
    public static class TokenManager
    {
        private record TokenHeader(string Algorithm = "SHA256", string Type = "JWT");

        private record TokenBody(string UserId, DateTime ExpireDate);

        // TODO: move this to cookie management file once created
        public const string UserDataJwtCookieName = "onboudt";

        /// <summary>
        /// Create a signed JWT containing the <paramref name="userId"/> provided
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GenerateJwtForUser(string userId)
        {
            //Setup Header
            TokenHeader header = new();
            string jwtHeader = JsonSerializer.Serialize(header);
            byte[] headerBytes = Encoding.UTF8.GetBytes(jwtHeader);
            string encodedHeader = Base64UrlEncoder.Encode(headerBytes);

            //Setup data body
            TokenBody body = new(userId, DateTime.UtcNow.AddDays(7));
            string jwtBody = JsonSerializer.Serialize(body);
            byte[] bodyBytes = Encoding.UTF8.GetBytes(jwtBody);
            string encodedBody = Base64UrlEncoder.Encode(bodyBytes);

            string jwt = $"{encodedHeader}.{encodedBody}";

            string jwtSignature = GetTokenSignature(jwt);
            string encodedSignature = Base64UrlEncoder.Encode(jwtSignature);

            string signedJwt = $"{jwt}.{encodedSignature}";

            return Base64UrlEncoder.Encode(signedJwt);
        }

        // TODO: Use this logic from a [ParamDecorator] that can inject this value automagically
        /// <summary>
        /// Take the <paramref name="jwt"/> provided and extract the userId from it
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        public static string GetUserIdFromToken(string jwt)
        {
            //Token anatomy is "encodedHeader.encodedBody.encodedSignature"
            string decodedJwt = Base64UrlEncoder.Decode(jwt);
            string encodedBody = decodedJwt.Split('.')[1];
            string decodedBody = Base64UrlEncoder.Decode(encodedBody);
            TokenBody tokenBody = JsonSerializer.Deserialize<TokenBody>(decodedBody);
            return tokenBody.UserId;
        }

        // TODO: Use this logic in a middleware responsible for managing the HttpCookie the token is stored in
        /// <summary>
        /// Take the <paramref name="jwt"/> provided and verify its signature is valid for the content the JWT contains
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        public static bool TokenIsValid(string jwt)
        {
            //Token anatomy is "encodedHeader.encodedBody.encodedSignature"
            string decodedJwt = Base64UrlEncoder.Decode(jwt);
            string[] encodedTokenParts = decodedJwt.Split('.');
            string encodedToken = $"{encodedTokenParts[0]}.{encodedTokenParts[1]}";
            string encodedSignature = encodedTokenParts[2];

            string decodedSignature = Base64UrlEncoder.Decode(encodedSignature);

            string recreatedSignature = GetTokenSignature(encodedToken);

            bool isValidSignature = recreatedSignature.Equals(decodedSignature);

            if (!isValidSignature)
            {
                // Don't parse/decode the body if we don't have to
                // Leave as soon as we can
                return false;
            }

            string decodedBody = Base64UrlEncoder.Decode(encodedTokenParts[1]);
            TokenBody tokenBody = JsonSerializer.Deserialize<TokenBody>(decodedBody);

            bool isNotExpired = DateTime.UtcNow < tokenBody.ExpireDate;
            return isNotExpired;
        }

        private static string GetTokenSignature(string token)
        {
            string signingSecret = Environment.GetEnvironmentVariable(SystemConstants.EnvironmentVariables.ONBO_USH)!;
            string defaultShaVector =
                Environment.GetEnvironmentVariable(SystemConstants.EnvironmentVariables.ONBO_SIV)!;
            string jwtSignature = string.Empty;
            foreach (string value in new[] { token, signingSecret })
            {
                jwtSignature += value;
                string hash = EncryptionUtil.SHA256Hash(jwtSignature, defaultShaVector);
                jwtSignature = hash;
            }

            return jwtSignature;
        }
    }
}