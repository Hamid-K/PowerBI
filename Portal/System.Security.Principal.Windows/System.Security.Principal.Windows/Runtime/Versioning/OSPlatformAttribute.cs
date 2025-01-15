using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000002 RID: 2
	internal abstract class OSPlatformAttribute : Attribute
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private protected OSPlatformAttribute(string platformName)
		{
			this.PlatformName = platformName;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000205F File Offset: 0x0000025F
		public string PlatformName { get; }
	}
}
