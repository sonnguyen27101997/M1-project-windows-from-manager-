//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace M1.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserLog
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> TimeLogin { get; set; }
        public Nullable<System.DateTime> TimeLogout { get; set; }
        public Nullable<bool> TypeError { get; set; }
        public string Describ { get; set; }
    }
}
