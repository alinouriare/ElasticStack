using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Shared.Masstransit
{
    public interface IEventDispatcher
    {
        void Dispatch<T>(params T[] events);
    }
}
