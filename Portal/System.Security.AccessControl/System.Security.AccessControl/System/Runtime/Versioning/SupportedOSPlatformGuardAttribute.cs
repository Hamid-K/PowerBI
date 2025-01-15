using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	internal sealed class SupportedOSPlatformGuardAttribute : OSPlatformAttribute
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000024D6 File Offset: 0x000006D6
		public SupportedOSPlatformGuardAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
