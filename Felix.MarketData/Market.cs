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
    
    public partial class Market
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Market()
        {
            this.Contracts = new HashSet<Contract>();
            this.MetaMappings = new HashSet<MetaMapping>();
        }
    
        public int MarketId { get; set; }
        public string Name { get; set; }
        public string Exchange { get; set; }
        public string Symbol { get; set; }
        public decimal TickSize { get; set; }
        public string Months { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contracts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MetaMapping> MetaMappings { get; set; }
    }
}