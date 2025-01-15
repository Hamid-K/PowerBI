using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	internal sealed class UnsupportedOSPlatformGuardAttribute : OSPlatformAttribute
	{
		// Token: 0x0600001C RID: 28 RVA: 0x0000230B File Offset: 0x0000050B
		public UnsupportedOSPlatformGuardAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
