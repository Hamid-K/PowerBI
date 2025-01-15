using System;
using System.DirectoryServices;
using System.Globalization;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002EA RID: 746
	public static class WebsiteQueryServices
	{
		// Token: 0x060013DA RID: 5082 RVA: 0x00044D6C File Offset: 0x00042F6C
		public static string GetWebsiteIdByWebsiteName(string websiteName)
		{
			if (string.IsNullOrEmpty(websiteName))
			{
				string text = string.Format(CultureInfo.CurrentCulture, "'{0}' may not be empty or null", new object[] { "websiteName" });
				throw new ArgumentNullException("websiteName", text);
			}
			using (DirectoryEntry directoryEntry = new DirectoryEntry("IIS://localhost/w3svc"))
			{
				foreach (object obj in directoryEntry.Children)
				{
					DirectoryEntry directoryEntry2 = (DirectoryEntry)obj;
					if (directoryEntry2.SchemaClassName.Equals("IIsWebServer", StringComparison.Ordinal) && websiteName.Equals(directoryEntry2.Properties["ServerComment"].Value.ToString(), StringComparison.Ordinal))
					{
						return directoryEntry2.Name;
					}
				}
				throw new WebsiteNotFoundException(websiteName);
			}
			string text2;
			return text2;
		}
	}
}
