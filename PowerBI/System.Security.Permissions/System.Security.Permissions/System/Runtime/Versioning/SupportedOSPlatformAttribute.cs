using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	internal sealed class SupportedOSPlatformAttribute : OSPlatformAttribute
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000022F0 File Offset: 0x000004F0
		public SupportedOSPlatformAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
