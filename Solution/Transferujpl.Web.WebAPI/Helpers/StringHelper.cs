using System.Collections.Specialized;

namespace Transferujpl.Web.WebAPI.Helpers
{
    public static class StringHelper
    {
        public static NameValueCollection ToNameValueCollection(this string input)
        {
            var nameValueCollection = new NameValueCollection();
            string[] querySegments = input.Split('&');
            foreach (string segment in querySegments)
            {
                string[] parts = segment.Split('=');
                if (parts.Length > 0)
                {
                    string key = parts[0].Trim(new char[] { '?', ' ' });
                    string val = System.Uri.UnescapeDataString(parts[1].Trim());

                    nameValueCollection.Add(key, val);
                }
            }

            return nameValueCollection;
        }
    }
}
