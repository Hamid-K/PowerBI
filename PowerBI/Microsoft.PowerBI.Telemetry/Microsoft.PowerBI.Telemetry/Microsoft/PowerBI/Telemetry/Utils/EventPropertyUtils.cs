using System;

namespace Microsoft.PowerBI.Telemetry.Utils
{
	// Token: 0x0200003C RID: 60
	public static class EventPropertyUtils
	{
		// Token: 0x0600018C RID: 396 RVA: 0x000059C2 File Offset: 0x00003BC2
		public static string GetOSVersion()
		{
			return Environment.OSVersion.VersionString;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000059CE File Offset: 0x00003BCE
		public static string GetOSBitness()
		{
			if (!Environment.Is64BitOperatingSystem)
			{
				return "32-bit";
			}
			return "64-bit";
		}
	}
}
