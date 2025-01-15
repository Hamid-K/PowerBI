using System;

namespace System.Runtime.Versioning
{
	// Token: 0x0200000E RID: 14
	internal abstract class OSPlatformAttribute : Attribute
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002AF4 File Offset: 0x00000CF4
		private protected OSPlatformAttribute(string platformName)
		{
			this.PlatformName = platformName;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002B03 File Offset: 0x00000D03
		public string PlatformName { get; }
	}
}
