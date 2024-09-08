using System.Net;

namespace Torty.Web.Apps.ObsNightBotOverlay.Infrastructure.Exceptions
{
    public class HttpException : Exception
    {
        public readonly HttpStatusCode ResponseCode;
        public new readonly string Message;

        public HttpException(HttpStatusCode responseCode, string message = null)
        {
            ResponseCode = responseCode;
            _SetMessageOrDefault(ref Message, message);
        }

        private void _SetMessageOrDefault(ref string message, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                message = value;
            }
            else
            {
                string statusName = Enum.GetName(typeof(HttpStatusCode), ResponseCode)!;
                message = string.Join(
                    string.Empty,
                    statusName.ToCharArray().Select(@char => char.IsUpper(@char) ? $" {@char}" : @char.ToString())
                ).Trim();
            }
        }
    }
}
