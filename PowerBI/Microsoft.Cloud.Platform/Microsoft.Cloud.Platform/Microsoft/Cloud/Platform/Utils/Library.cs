using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200016F RID: 367
	public static class Library
	{
		// Token: 0x060009A8 RID: 2472 RVA: 0x0002135C File Offset: 0x0001F55C
		public static void Initialize()
		{
			Library.Initialize(CloudPlatformExecutionMode.HostEnvironment);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00021364 File Offset: 0x0001F564
		public static void Initialize(CloudPlatformExecutionMode cloudPlatformExecutionMode)
		{
			Library.s_mode = cloudPlatformExecutionMode;
			Anchor.Initialize();
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x00021371 File Offset: 0x0001F571
		public static CloudPlatformExecutionMode Mode
		{
			get
			{
				return Library.s_mode;
			}
		}

		// Token: 0x040003A6 RID: 934
		private static CloudPlatformExecutionMode s_mode = CloudPlatformExecutionMode.HostEnvironment;
	}
}
