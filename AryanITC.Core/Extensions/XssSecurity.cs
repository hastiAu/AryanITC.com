﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ganss.XSS;

namespace AryanITC.Core.Extensions
{
   public static class XssSecurity
    {
        public static string SanitizeText(this string text)
            {
                var htmlSanitizer = new HtmlSanitizer();

                htmlSanitizer.KeepChildNodes = true;

                htmlSanitizer.AllowDataAttributes = true;

                return htmlSanitizer.Sanitize(text);
            }
        
    }
}
