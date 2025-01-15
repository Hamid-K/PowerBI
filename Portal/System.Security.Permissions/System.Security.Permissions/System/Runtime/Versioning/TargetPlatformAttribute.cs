using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000005 RID: 5
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	internal sealed class TargetPlatformAttribute : OSPlatformAttribute
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000022E7 File Offset: 0x000004E7
		public TargetPlatformAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
