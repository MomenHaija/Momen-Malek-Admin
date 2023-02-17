//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace University_Registration.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Subject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subject()
        {
            this.Sections = new HashSet<Section>();
            this.SubjectRegistrations = new HashSet<SubjectRegistration>();
        }
    
        public int Subject_ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> Major_ID { get; set; }
        public Nullable<int> NumHour { get; set; }
        public Nullable<bool> SubjectView { get; set; }
    
        public virtual Major Major { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Section> Sections { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubjectRegistration> SubjectRegistrations { get; set; }
    }
}
