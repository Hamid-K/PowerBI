using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	internal sealed class SupportedOSPlatformGuardAttribute : OSPlatformAttribute
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00002C0E File Offset: 0x00000E0E
		public SupportedOSPlatformGuardAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
