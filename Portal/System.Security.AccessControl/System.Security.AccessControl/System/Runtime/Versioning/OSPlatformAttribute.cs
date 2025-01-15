using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000004 RID: 4
	internal abstract class OSPlatformAttribute : Attribute
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000024A4 File Offset: 0x000006A4
		private protected OSPlatformAttribute(string platformName)
		{
			this.PlatformName = platformName;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000024B3 File Offset: 0x000006B3
		public string PlatformName { get; }
	}
}
