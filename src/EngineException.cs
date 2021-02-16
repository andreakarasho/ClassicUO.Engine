using System;
using System.Collections.Generic;
using System.Text;

namespace ClassicUO.Engine
{
    class EngineException : Exception
    {
        public EngineException(string msg) : base(msg)
        {

        }
    }
}
