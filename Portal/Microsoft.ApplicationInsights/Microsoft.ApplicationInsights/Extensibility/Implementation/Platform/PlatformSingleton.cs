using System;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Platform
{
	// Token: 0x020000AF RID: 175
	internal static class PlatformSingleton
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00015D98 File Offset: 0x00013F98
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x00015DAE File Offset: 0x00013FAE
		public static IPlatform Current
		{
			get
			{
				IPlatform platform;
				if ((platform = PlatformSingleton.current) == null)
				{
					platform = (PlatformSingleton.current = new PlatformImplementation());
				}
				return platform;
			}
			set
			{
				PlatformSingleton.current = value;
			}
		}

		// Token: 0x0400021B RID: 539
		private static IPlatform current;
	}
}
