namespace ProfileViewer.Application.Authorization
{
    public static class AppPermissions
    {
        public const string GROUP = "ProfileViewer";

        public static class UserPermissions
        {
            public const string USERS = GROUP + ".Users";
            public const string DELETE = USERS + ".Delete";
            public const string EDIT = USERS + ".Edit";
        }
    }
}
