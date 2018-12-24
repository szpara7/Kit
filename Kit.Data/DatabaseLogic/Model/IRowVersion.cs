using System;
using System.Collections.Generic;
using System.Text;

namespace Kit.Data.DatabaseLogic
{
    public interface IRowVersion
    {
        byte[] TimeStamp { get; set; }
    }
}
