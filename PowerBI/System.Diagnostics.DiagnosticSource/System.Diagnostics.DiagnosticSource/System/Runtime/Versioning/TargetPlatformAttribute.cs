using System;

namespace System.Runtime.Versioning
{
	// Token: 0x0200000F RID: 15
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	internal sealed class TargetPlatformAttribute : OSPlatformAttribute
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002B0B File Offset: 0x00000D0B
		public TargetPlatformAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
