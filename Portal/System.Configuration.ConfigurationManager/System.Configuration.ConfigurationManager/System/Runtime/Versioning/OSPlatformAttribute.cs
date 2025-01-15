using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000004 RID: 4
	internal abstract class OSPlatformAttribute : Attribute
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00002BDC File Offset: 0x00000DDC
		private protected OSPlatformAttribute(string platformName)
		{
			this.PlatformName = platformName;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00002BEB File Offset: 0x00000DEB
		public string PlatformName { get; }
	}
}
