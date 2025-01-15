using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000003 RID: 3
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	internal sealed class TargetPlatformAttribute : OSPlatformAttribute
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		public TargetPlatformAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
