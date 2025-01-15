using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000004 RID: 4
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, AllowMultiple = true, Inherited = false)]
	internal sealed class SupportedOSPlatformAttribute : OSPlatformAttribute
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002070 File Offset: 0x00000270
		public SupportedOSPlatformAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
