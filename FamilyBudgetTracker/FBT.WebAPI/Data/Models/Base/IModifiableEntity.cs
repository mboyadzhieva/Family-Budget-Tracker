namespace FBT.WebAPI.Data.Models.Base
{
    using System;

    public interface IModifiableEntity
    {
        DateTime? ModifiedOn { get; set; }
    }
}
