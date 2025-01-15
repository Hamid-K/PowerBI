using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	internal sealed class SupportedOSPlatformGuardAttribute : OSPlatformAttribute
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002B26 File Offset: 0x00000D26
		public SupportedOSPlatformGuardAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
