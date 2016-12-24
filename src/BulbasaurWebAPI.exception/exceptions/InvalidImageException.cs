using System;

namespace BulbasaurWebAPI.exception.exceptions
{
    public class InvalidImageException : Exception
    {
        public InvalidImageException()
        {
            
        }

        public InvalidImageException(string message) : base(message)
        {
            
        }
        
    }
}