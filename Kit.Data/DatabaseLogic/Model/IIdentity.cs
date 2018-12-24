using System;
using System.Collections.Generic;
using System.Text;

namespace Kit.Data.DatabaseLogic
{
    public interface IIdentity 
    {
        int Id { get; set; }
        Guid PublicId { get; set; }        
    }
}
