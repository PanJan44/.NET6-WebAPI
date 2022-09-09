using System;

namespace web_api_net5.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException(string msg):base(msg)
        {
            
        }
    }
}
