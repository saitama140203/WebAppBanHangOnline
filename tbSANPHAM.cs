//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAppBanHangOnlineNhomNBTPQ
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbSANPHAM
    {
        public tbSANPHAM()
        {
            this.tbCHITIETDONHANGs = new HashSet<tbCHITIETDONHANG>();
            this.tbHINHANHs = new HashSet<tbHINHANH>();
            this.tbREVIEWs = new HashSet<tbREVIEW>();
        }
    
        public int MASANPHAM { get; set; }
        public string TENSANPHAM { get; set; }
        public string SIZE { get; set; }
        public string MAU { get; set; }
        public Nullable<decimal> DONGIA { get; set; }
        public Nullable<decimal> SOLUONG { get; set; }
        public string MOTA { get; set; }
        public Nullable<int> MADANHMUC { get; set; }
    
        public virtual ICollection<tbCHITIETDONHANG> tbCHITIETDONHANGs { get; set; }
        public virtual tbDANHMUC tbDANHMUC { get; set; }
        public virtual ICollection<tbHINHANH> tbHINHANHs { get; set; }
        public virtual ICollection<tbREVIEW> tbREVIEWs { get; set; }
    }
}