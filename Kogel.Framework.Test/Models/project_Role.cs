
/*
 * 本文件由根据实体插件自动生成，请勿更改
 * =========================== */

using System;
using Kogel.Dapper.Extension.Attributes;
namespace Kogel.Data.Model
{
    public class project_Role
    {
        
        /// <summary>
        /// 角色id
        /// </summary>    
       [Identity]
        [Display(Name="角色id")]
        public int id{ get; set; }
        
        /// <summary>
        /// 角色名称
        /// </summary>    
        [StringLength(50)]
        [Display(Name="角色名称")]
        public string name{ get; set; }
        
        /// <summary>
        /// 角色父级id,0为最高等级角色
        /// </summary>    
        [Display(Name="角色父级id,0为最高等级角色")]
        public int parentId{ get; set; }
        
        /// <summary>
        /// 角色描述
        /// </summary>    
        [StringLength(500)]
        [Display(Name="角色描述")]
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
        /// 项目id
        /// </summary>    
        [Display(Name="项目id")]
        public int projectId{ get; set; }
    }
}
