﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_CURD_WOET.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}