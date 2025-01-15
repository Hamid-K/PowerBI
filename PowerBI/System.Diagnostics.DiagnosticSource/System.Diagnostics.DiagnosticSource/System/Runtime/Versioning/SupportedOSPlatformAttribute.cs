using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000010 RID: 16
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	internal sealed class SupportedOSPlatformAttribute : OSPlatformAttribute
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002B14 File Offset: 0x00000D14
		public SupportedOSPlatformAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
