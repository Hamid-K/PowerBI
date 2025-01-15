using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000005 RID: 5
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	internal sealed class TargetPlatformAttribute : OSPlatformAttribute
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00002BF3 File Offset: 0x00000DF3
		public TargetPlatformAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
