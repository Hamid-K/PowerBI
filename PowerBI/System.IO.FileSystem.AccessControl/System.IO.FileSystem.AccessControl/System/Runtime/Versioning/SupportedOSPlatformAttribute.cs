using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000016 RID: 22
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, AllowMultiple = true, Inherited = false)]
	internal sealed class SupportedOSPlatformAttribute : OSPlatformAttribute
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002683 File Offset: 0x00000883
		public SupportedOSPlatformAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
