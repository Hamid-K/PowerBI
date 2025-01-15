using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000005 RID: 5
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	internal sealed class TargetPlatformAttribute : OSPlatformAttribute
	{
		// Token: 0x0600003F RID: 63 RVA: 0x000024BB File Offset: 0x000006BB
		public TargetPlatformAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
