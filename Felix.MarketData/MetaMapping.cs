//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Felix.MarketData
{
    using System;
    using System.Collections.Generic;
    
    public partial class MetaMapping
    {
        public int MetaMappingId { get; set; }
        public int MarketId { get; set; }
        public string Property { get; set; }
        public Nullable<int> Value { get; set; }
        public Nullable<double> DValue { get; set; }
    
        public virtual Market Market { get; set; }
    }
}