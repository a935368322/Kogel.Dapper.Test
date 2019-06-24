
/*
 * 本文件由根据实体插件自动生成，请勿更改
 * =========================== */

using System;
using Kogel.Dapper.Extension.Attributes;
namespace Kogel.Data.Model
{
    public class users
    {

        /// <summary>
        /// 用户id
        /// </summary>    
        [Identity]
        [Display(Name = "用户id")]
        public int id { get; set; }

        /// <summary>
        /// code
        /// </summary>    
        [StringLength(50)]
        [Display(Name = "code")]
        public string code { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>    
        [StringLength(50)]
        [Display(Name = "用户名称")]
        public string name { get; set; }

        /// <summary>
        /// 创建方式(1自定义角色 2通用角色)
        /// </summary>    
        [Display(Name = "创建方式(1自定义角色 2通用角色)")]
        public int createWay { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>    
        [Display(Name = "创建时间")]
        public DateTime createDate { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>    
        [StringLength(50)]
        [Display(Name = "创建人")]
        public string createUsers { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>    
        [Display(Name = "角色id")]
        public int roleId { get; set; }
    }
    [Display(Rename ="users")]
    public class userssss
    {
        /// <summary>
        /// 用户id
        /// </summary>    
        [Identity]
        [Display(Name = "用户id")]
        public int id { get; set; }

        /// <summary>
        /// code
        /// </summary>    
        [StringLength(50)]
        [Display(Name = "code")]
        public string code { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>    
        [StringLength(50)]
        [Display(Name = "用户名称")]
        public string name { get; set; }

        /// <summary>
        /// 创建方式(1自定义角色 2通用角色)
        /// </summary>    
        [Display(Name = "创建方式(1自定义角色 2通用角色)")]
        public int createWay { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>    
        [Display(Name = "创建时间")]
        public DateTime createDate { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>    
        [StringLength(50)]
        [Display(Name = "创建人")]
        public string createUsers { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>    
        [Display(Name = "角色id")]
        public int roleId { get; set; }
    }
}
