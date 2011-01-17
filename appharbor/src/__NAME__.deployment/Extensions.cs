namespace __NAME__.deployment
{
    using System;
    using dropkick;
    using dropkick.Configuration.Dsl;
    using dropkick.Configuration.Dsl.Files;

    public static class Extensions
    {
        public static bool EqualsIgnoreCase(this string s, string other)
        {
            return s.Equals(other, StringComparison.InvariantCultureIgnoreCase);
        }

        public static void CopyEnvironmentSpecificFile(this ProtoServer s, string destination, string environment, string filename)
        {
            s.CopyFile(@".\..\EnvironmentFiles\{0}\{0}.{1}".FormatWith(environment, filename))
                .ToDirectory(destination);

            s.RenameFile(@"{0}\{1}.{2}".FormatWith(destination, environment, filename))
                .To(filename);
        }
    }
}
