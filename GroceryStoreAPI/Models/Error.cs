using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Models
{
    public sealed class Error
    {
        public Error(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            Message = message;
        }

        public Error(string message, int statusCode)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            StatusCode = statusCode;

            Message = message;
        }

        public int StatusCode { get; private set; }

        public string Message { get; private set; }

        public List<Link> Links { get; }

        public override string ToString()
        {
            return Message;
        }
    }
}
