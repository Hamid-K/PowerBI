using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	internal sealed class UnsupportedOSPlatformGuardAttribute : OSPlatformAttribute
	{
		// Token: 0x06000043 RID: 67 RVA: 0x000024DF File Offset: 0x000006DF
		public UnsupportedOSPlatformGuardAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
