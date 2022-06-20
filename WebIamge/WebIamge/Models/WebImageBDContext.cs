using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebIamge.Models
{
    public class WebImageBDContext:DbContext
    {
        public WebImageBDContext() : base("name=ChuoiKN") { }
        public DbSet<Manage> Manages { get; set; }
    }
}