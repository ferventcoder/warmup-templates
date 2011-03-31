namespace __NAME__.infrastructure.context.information
{
    using System;
    using System.Web;

    public sealed class UserInformation
    {
        public static string get_identity()
        {
            string identity_of_user = Environment.UserName;

            if (HttpContext.Current !=null && HttpContext.Current.User != null)
            {
                identity_of_user = HttpContext.Current.User.Identity.Name;                  
            }

            return identity_of_user;
        }
    }
}