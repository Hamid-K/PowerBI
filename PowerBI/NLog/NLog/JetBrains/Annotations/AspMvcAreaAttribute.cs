using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001DE RID: 478
	[AttributeUsage(AttributeTargets.Parameter)]
	internal sealed class AspMvcAreaAttribute : Attribute
	{
		// Token: 0x06001457 RID: 5207 RVA: 0x000369C3 File Offset: 0x00034BC3
		public AspMvcAreaAttribute()
		{
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x000369CB File Offset: 0x00034BCB
		public AspMvcAreaAttribute([NotNull] string anonymousProperty)
		{
			this.AnonymousProperty = anonymousProperty;
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x000369DA File Offset: 0x00034BDA
		// (set) Token: 0x0600145A RID: 5210 RVA: 0x000369E2 File Offset: 0x00034BE2
		[CanBeNull]
		public string AnonymousProperty { get; private set; }
	}
}
