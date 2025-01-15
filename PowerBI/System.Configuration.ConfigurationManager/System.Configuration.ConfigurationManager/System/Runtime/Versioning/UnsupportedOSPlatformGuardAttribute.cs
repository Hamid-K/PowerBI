using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	internal sealed class UnsupportedOSPlatformGuardAttribute : OSPlatformAttribute
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00002C17 File Offset: 0x00000E17
		public UnsupportedOSPlatformGuardAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
