using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AryanITC.Core.FilePath
{
   public class FilePath
    {
        #region UserAvatar Path

        //we use these two methods for Showing image
        public static readonly string UserAvatarImage = "/Images/AvatarImage/Origin/";
        public static readonly string UserAvatarThumbImage = "/Images/AvatarImage/Thumb/";

        //we use these two methods for saving image
        public static readonly string UserAvatarServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/AvatarImage/Origin/");
        public static readonly string UserAvatarThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/AvatarImage/Thumb/");

        #endregion

        #region SiteSetting Path

        public static readonly string SiteSettingLogoServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/SiteLogo/");
        public static readonly string SiteSettingLogo = Path.Combine(Directory.GetCurrentDirectory(), "/Images/SiteLogo/");
        #endregion

        #region AboutUs Path

        //This is for show on Admin -Origin & Thumb

        public static readonly string AboutUsImage = "/Images/AboutUs/Origin/";
        public static readonly string UAboutUsThumbImage = "/Images/AboutUs/Thumb/";

        //For Our Upload _Origin & Thumb
        public static readonly string AboutUsServer =Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/AboutUs/Origin/");
        public static readonly string AboutUsThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/AboutUs/Thumb/");

        #endregion
    }
}
