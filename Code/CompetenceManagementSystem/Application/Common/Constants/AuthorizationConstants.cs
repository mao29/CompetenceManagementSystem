using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Constants
{
    public static class AuthorizationConstants
    {
        public static class Roles
        {
            public const string ADMINISTRATORS = "Administrators";
        }

        public static class Users
        {
            public const string ADMIN = "admin";
            public const string DEMO = "demo";
        }

        // TODO: Don't use this in production
        public const string DEFAULT_PASSWORD = "P@ssw0rd";

    }
}
