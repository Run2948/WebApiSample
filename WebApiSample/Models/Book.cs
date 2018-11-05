using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiSample.Models
{
    public class Book
    {
        /// <summary>
        /// 图书编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 图书名称
        /// </summary>
        [MaxLength(5)]
        public string Name { get; set; }
        /// <summary>
        /// 图书价格
        /// </summary>
        public double Price { get; set; }

    }
}