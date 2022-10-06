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
        #region UserAvatar

        //we use these two methods for Showing image
        public static readonly string UserAvatarImage = "/Images/AvatarImage/Origin/";
        public static readonly string UserAvatarThumbImage = "/Images/AvatarImage/Thumb/";

        //we use these two methods for saving image
        public static readonly string UserAvatarServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/AvatarImage/Origin/");
        public static readonly string UserAvatarThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/AvatarImage/Thumb/");

        #endregion
    }
}
