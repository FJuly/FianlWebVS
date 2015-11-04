using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  
using System.Web.Mvc;

namespace MVC.Helper.HtmlHelpers
{
    //扩展方法
   public  static class HtmlHelpers
    {
       public static string Truncate(this HtmlHelper helper, string input, int length)
       {
           if (input.Length <= length)
           {
               return input;
           }
           else
           {
               return input.Substring(0,length)+"...";
           }
       }
    }
}
