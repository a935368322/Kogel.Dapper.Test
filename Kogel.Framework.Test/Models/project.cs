
/*
 * 本文件由根据实体插件自动生成，请勿更改
 * =========================== */

using System;
using Kogel.Dapper.Extension.Attributes;

namespace Kogel.Data.Model
{
    public class project
    {
        /// <summary>
        /// 项目id
        /// </summary>    
        [Identity]
        [Display(Name="项目id")]
        public int id{ get; set; }
        
        /// <summary>
        /// 项目名称
        /// </summary>    
        [StringLength(50)]
        [Display(Name="项目名称")]
        public string name{ get; set; }
    }
}
