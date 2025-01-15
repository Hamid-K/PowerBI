using System;
using System.Collections.Generic;
using Owin;

namespace Microsoft.Owin.StaticFiles
{
	// Token: 0x0200000F RID: 15
	public static class SendFileExtensions
	{
		// Token: 0x0600003E RID: 62 RVA: 0x000028E2 File Offset: 0x00000AE2
		public static IAppBuilder UseSendFileFallback(this IAppBuilder builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			if (SendFileExtensions.IsSendFileSupported(builder.Properties))
			{
				return builder;
			}
			SendFileExtensions.SetSendFileCapability(builder.Properties);
			return builder.Use(new object[0]);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002918 File Offset: 0x00000B18
		private static bool IsSendFileSupported(IDictionary<string, object> properties)
		{
			object obj;
			if (properties.TryGetValue("server.Capabilities", out obj))
			{
				IDictionary<string, object> capabilities = (IDictionary<string, object>)obj;
				if (capabilities.TryGetValue("sendfile.Version", out obj) && "1.0".Equals((string)obj, StringComparison.Ordinal))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002960 File Offset: 0x00000B60
		private static void SetSendFileCapability(IDictionary<string, object> properties)
		{
			object obj;
			if (properties.TryGetValue("server.Capabilities", out obj))
			{
				IDictionary<string, object> capabilities = (IDictionary<string, object>)obj;
				capabilities["sendfile.Version"] = "1.0";
			}
		}
	}
}
