
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Sem3Project.Models
{

using System;
    using System.Collections.Generic;
    
public partial class Card
{

    public int CardId { get; set; }

    public Nullable<int> ProductId { get; set; }

    public Nullable<int> ProductNumber { get; set; }

    public string CustomerId { get; set; }



    public virtual Customer Customer { get; set; }

    public virtual Product Product { get; set; }

}

}
