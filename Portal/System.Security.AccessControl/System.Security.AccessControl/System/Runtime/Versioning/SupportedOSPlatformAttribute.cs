using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	internal sealed class SupportedOSPlatformAttribute : OSPlatformAttribute
	{
		// Token: 0x06000040 RID: 64 RVA: 0x000024C4 File Offset: 0x000006C4
		public SupportedOSPlatformAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
