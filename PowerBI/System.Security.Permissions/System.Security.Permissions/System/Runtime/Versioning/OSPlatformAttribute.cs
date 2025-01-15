using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000004 RID: 4
	internal abstract class OSPlatformAttribute : Attribute
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000022D0 File Offset: 0x000004D0
		private protected OSPlatformAttribute(string platformName)
		{
			this.PlatformName = platformName;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022DF File Offset: 0x000004DF
		public string PlatformName { get; }
	}
}
