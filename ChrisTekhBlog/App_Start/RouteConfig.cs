﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ChrisTekhBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                name: "Action",
                url: "{action}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                "Category",
                "Category/{category}",
                new { controller = "Home", action = "Category" }
            );
            routes.MapRoute(
                "Tag",
                "Tag/{tag}",
                new { controller = "Home", action = "Tag" }
            );
            routes.MapRoute(
                "Post",
                "Archive/{year}/{month}/{title}",
                new { controller = "Home", action = "Post" }
            );
           
        }
    }
}