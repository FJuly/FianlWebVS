//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MODEL
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_Permission
    {
        public T_Permission()
        {
            this.T_RolePermission = new HashSet<T_RolePermission>();
        }
    
        public int PerId { get; set; }
        public string PerName { get; set; }
        public Nullable<int> PerParent { get; set; }
        public string PerAreaName { get; set; }
        public string PerController { get; set; }
        public string PerActionName { get; set; }
        public Nullable<int> PerFormMethod { get; set; }
        public bool PerIsDel { get; set; }
        public bool PerIsShow { get; set; }
        public Nullable<System.DateTime> PerAddTime { get; set; }
        public string PerIco { get; set; }
        public bool IsCommon { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<int> bpx { get; set; }
        public Nullable<int> bpy { get; set; }
    
        public virtual ICollection<T_RolePermission> T_RolePermission { get; set; }
    }
}
