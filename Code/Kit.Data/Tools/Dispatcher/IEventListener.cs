using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kit.Data.Tools.Dispatcher
{
    public interface IEventListener
    {
        Task HandleEvent();
    }
}
