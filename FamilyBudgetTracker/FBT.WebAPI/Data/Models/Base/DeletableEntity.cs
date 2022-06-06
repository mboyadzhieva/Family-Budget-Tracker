namespace FBT.WebAPI.Data.Models.Base
{
    using System;

    public abstract class DeletableEntity
    {
        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
