using System;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000176 RID: 374
	[AttributeUsage(AttributeTargets.All)]
	internal class PreserveAttribute : Attribute
	{
		// Token: 0x040006C6 RID: 1734
		public bool Conditional;

		// Token: 0x040006C7 RID: 1735
		public bool AllMembers;
	}
}
