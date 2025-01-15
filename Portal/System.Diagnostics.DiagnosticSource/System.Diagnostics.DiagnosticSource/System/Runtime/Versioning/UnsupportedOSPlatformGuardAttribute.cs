using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000013 RID: 19
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	internal sealed class UnsupportedOSPlatformGuardAttribute : OSPlatformAttribute
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002B2F File Offset: 0x00000D2F
		public UnsupportedOSPlatformGuardAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
