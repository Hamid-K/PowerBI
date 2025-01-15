using System;

namespace Microsoft.PowerBI.Packaging.Host
{
	// Token: 0x02000092 RID: 146
	public static class HostSetup
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000A9DC File Offset: 0x00008BDC
		public static HostApplication Application
		{
			get
			{
				return HostSetup.CurrentApplication;
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000A9E3 File Offset: 0x00008BE3
		public static void SetHostApplication(HostApplication application)
		{
			HostSetup.CurrentApplication = application;
		}

		// Token: 0x0400022D RID: 557
		private static HostApplication CurrentApplication;
	}
}
