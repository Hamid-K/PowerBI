using System;
using System.ComponentModel;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000164 RID: 356
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Logging is now specified per ClientApplication.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration. ", true)]
	public sealed class Logger
	{
		// Token: 0x17000390 RID: 912
		// (set) Token: 0x0600117A RID: 4474 RVA: 0x0003BE3F File Offset: 0x0003A03F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Logging is now specified per ClientApplication.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration. ", true)]
		public static LogCallback LogCallback
		{
			set
			{
				throw new NotImplementedException("Logging is now specified per ClientApplication.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration. ");
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x0003BE4B File Offset: 0x0003A04B
		// (set) Token: 0x0600117C RID: 4476 RVA: 0x0003BE57 File Offset: 0x0003A057
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Logging is now specified per ClientApplication.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration. ", true)]
		public static LogLevel Level
		{
			get
			{
				throw new NotImplementedException("Logging is now specified per ClientApplication.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration. ");
			}
			set
			{
				throw new NotImplementedException("Logging is now specified per ClientApplication.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration. ");
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x0003BE63 File Offset: 0x0003A063
		// (set) Token: 0x0600117E RID: 4478 RVA: 0x0003BE6A File Offset: 0x0003A06A
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Logging is now specified per ClientApplication.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration. ", true)]
		public static bool PiiLoggingEnabled { get; set; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x0003BE72 File Offset: 0x0003A072
		// (set) Token: 0x06001180 RID: 4480 RVA: 0x0003BE79 File Offset: 0x0003A079
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Logging is now specified per ClientApplication.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration. ", true)]
		public static bool DefaultLoggingEnabled { get; set; }
	}
}
