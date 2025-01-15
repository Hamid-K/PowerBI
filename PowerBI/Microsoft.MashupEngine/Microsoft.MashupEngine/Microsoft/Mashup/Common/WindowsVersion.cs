using System;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C38 RID: 7224
	public static class WindowsVersion
	{
		// Token: 0x0600B46C RID: 46188 RVA: 0x00249761 File Offset: 0x00247961
		public static bool IsAtLeast(Version version)
		{
			return WindowsVersion.Current >= version;
		}

		// Token: 0x04005BB8 RID: 23480
		public static readonly Version Windows7 = new Version(6, 1);

		// Token: 0x04005BB9 RID: 23481
		public static readonly Version Windows8 = new Version(6, 2);

		// Token: 0x04005BBA RID: 23482
		public static readonly Version Current = Environment.OSVersion.Version;
	}
}
