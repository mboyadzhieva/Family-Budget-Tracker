namespace FBT.WebAPI.Data.Models.Base
{
    using System;

    public abstract class Entity
    {
        public DateTime? ModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
