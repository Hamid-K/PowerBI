using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	internal sealed class SupportedOSPlatformGuardAttribute : OSPlatformAttribute
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002302 File Offset: 0x00000502
		public SupportedOSPlatformGuardAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
