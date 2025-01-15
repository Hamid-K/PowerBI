using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000005 RID: 5
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, AllowMultiple = true, Inherited = false)]
	internal sealed class UnsupportedOSPlatformAttribute : OSPlatformAttribute
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002079 File Offset: 0x00000279
		public UnsupportedOSPlatformAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
