namespace __NAME__.infrastructure.extensions
{
    using System.IO;
    using System.Text;

    public static class ExtensionsToStream
    {
        public static byte[] read_to_end(this Stream stream)
        {
            using (var content = new MemoryStream())
            {
                var buffer = new byte[4096];

                int read = stream.Read(buffer, 0, 4096);
                while (read > 0)
                {
                    content.Write(buffer, 0, read);

                    read = stream.Read(buffer, 0, 4096);
                }

                return content.ToArray();
            }
        }

        public static string read_to_end_as_text(this Stream stream)
        {
            return Encoding.UTF8.GetString(stream.read_to_end());
        }
    }
}