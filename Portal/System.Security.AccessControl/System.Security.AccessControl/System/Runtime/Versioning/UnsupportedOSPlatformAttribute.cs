using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	internal sealed class UnsupportedOSPlatformAttribute : OSPlatformAttribute
	{
		// Token: 0x06000041 RID: 65 RVA: 0x000024CD File Offset: 0x000006CD
		public UnsupportedOSPlatformAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
