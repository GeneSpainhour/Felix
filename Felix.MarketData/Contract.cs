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
    
    public partial class Contract
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contract()
        {
            this.Bars = new HashSet<Bar>();
        }
    
        public int ContractId { get; set; }
        public int MarketId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public System.DateTime BeginDate { get; set; }
        public System.DateTime EndDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bar> Bars { get; set; }
        public virtual Market Market { get; set; }
    }
}