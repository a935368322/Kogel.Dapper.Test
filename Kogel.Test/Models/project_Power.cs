
/*
 * 本文件由根据实体插件自动生成，请勿更改
 * =========================== */

using System;
using Kogel.Dapper.Extension.Attributes;

namespace Kogel.Data.Model
{
    public class project_Power
    {
        
        /// <summary>
        /// 权限id
        /// </summary>    
        [Identity]
        [Display(Name="权限id")]
        public int id{ get; set; }
        
        /// <summary>
        /// 权限名称
        /// </summary>    
        [StringLength(50)]
        [Display(Name="权限名称")]
        public string name{ get; set; }
        
        /// <summary>
        /// 父级权限id,0为最顶部权限
        /// </summary>    
        [Display(Name="父级权限id,0为最顶部权限")]
        public int parentId{ get; set; }
        
        /// <summary>
        /// 权限描述
        /// </summary>    
        [StringLength(500)]
        [Display(Name="权限描述")]
        public string description{ get; set; }
        
        /// <summary>
        /// 是否启用
        /// </summary>    
        [Display(Name="是否启用")]
        public bool enabled{ get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>    
        [Display(Name="创建时间")]
        public DateTime createDate{ get; set; }
        
        /// <summary>
        /// 创建人
        /// </summary>    
        [StringLength(50)]
        [Display(Name="创建人")]
        public string createUsers{ get; set; }
        
        /// <summary>
        /// projectId
        /// </summary>    
        [Display(Name="projectId")]
        public int projectId{ get; set; }
    }
}
