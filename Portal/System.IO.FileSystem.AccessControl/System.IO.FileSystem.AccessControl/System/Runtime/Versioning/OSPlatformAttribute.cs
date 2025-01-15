using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000014 RID: 20
	internal abstract class OSPlatformAttribute : Attribute
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002663 File Offset: 0x00000863
		private protected OSPlatformAttribute(string platformName)
		{
			this.PlatformName = platformName;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002672 File Offset: 0x00000872
		public string PlatformName { get; }
	}
}
