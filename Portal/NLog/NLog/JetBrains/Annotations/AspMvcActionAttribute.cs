using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001DD RID: 477
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter)]
	internal sealed class AspMvcActionAttribute : Attribute
	{
		// Token: 0x06001453 RID: 5203 RVA: 0x0003699B File Offset: 0x00034B9B
		public AspMvcActionAttribute()
		{
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x000369A3 File Offset: 0x00034BA3
		public AspMvcActionAttribute([NotNull] string anonymousProperty)
		{
			this.AnonymousProperty = anonymousProperty;
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x000369B2 File Offset: 0x00034BB2
		// (set) Token: 0x06001456 RID: 5206 RVA: 0x000369BA File Offset: 0x00034BBA
		[CanBeNull]
		public string AnonymousProperty { get; private set; }
	}
}
