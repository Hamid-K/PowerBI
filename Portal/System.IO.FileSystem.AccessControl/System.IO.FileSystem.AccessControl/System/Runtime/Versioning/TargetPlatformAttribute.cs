using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000015 RID: 21
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	internal sealed class TargetPlatformAttribute : OSPlatformAttribute
	{
		// Token: 0x0600004D RID: 77 RVA: 0x0000267A File Offset: 0x0000087A
		public TargetPlatformAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
