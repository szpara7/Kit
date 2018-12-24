using System;
using System.Collections.Generic;
using System.Text;

namespace Kit.Data.DatabaseLogic
{
    public interface IChangeInfo
    {
        int CreatedBy { get; set; }
        int? ModifiedBy { get; set; }
        int? DeletedBy { get; set; }

        DateTime DateCreated { get; set; }
        DateTime? DateModified { get; set; }
        DateTime? DateDeleted { get; set; }
    }
}
