
/*
 * 本文件由根据实体插件自动生成，请勿更改
 * =========================== */

using System;
using Kogel.Dapper.Extension.Attributes;

namespace Kogel.Data.Model
{
    public class role_Power
    {
        
        /// <summary>
        /// id
        /// </summary>    
[Identity]
        [Display(Name="id")]
        public int id{ get; set; }
        
        /// <summary>
        /// roleId
        /// </summary>    
        [Display(Name="roleId")]
        public int roleId{ get; set; }
        
        /// <summary>
        /// powerId
        /// </summary>    
        [Display(Name="powerId")]
        public int powerId{ get; set; }
        
        /// <summary>
        /// createDate
        /// </summary>    
        [Display(Name="createDate")]
        public DateTime createDate{ get; set; }
        
        /// <summary>
        /// createUsers
        /// </summary>    
        [StringLength(50)]
        [Display(Name="createUsers")]
        public string createUsers{ get; set; }
    }
}
