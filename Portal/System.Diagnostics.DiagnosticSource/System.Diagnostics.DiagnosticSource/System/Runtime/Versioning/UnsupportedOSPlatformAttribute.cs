using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000011 RID: 17
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	internal sealed class UnsupportedOSPlatformAttribute : OSPlatformAttribute
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002B1D File Offset: 0x00000D1D
		public UnsupportedOSPlatformAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
