using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulbasaurWebAPI.bl.exceptions
{
    public class MessageAndMediaEmptyException : Exception
    {
        public MessageAndMediaEmptyException()
        {

        }

        public MessageAndMediaEmptyException(string message) : base(message)
        {

        }

    }
}
