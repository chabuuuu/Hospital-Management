//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LTTQ_DoAn
{
    using System;
    using System.Collections.Generic;
    
    public partial class LICHKHAM
    {
        public int MALICHKHAM { get; set; }
        public string SUB_ID { get; set; }
        public Nullable<int> MABACSI { get; set; }
        public Nullable<int> MABENHNHAN { get; set; }
        public Nullable<int> MAPHONG { get; set; }
        public Nullable<int> MADICHVU { get; set; }
        public Nullable<System.DateTime> NGAYKHAM { get; set; }
        public Nullable<System.DateTime> NGAYLENLICH { get; set; }
    
        public virtual BENHNHAN BENHNHAN { get; set; }
        public virtual DICHVU DICHVU { get; set; }
        public virtual YSI YSI { get; set; }
        public virtual PHONG PHONG { get; set; }
    }
}
