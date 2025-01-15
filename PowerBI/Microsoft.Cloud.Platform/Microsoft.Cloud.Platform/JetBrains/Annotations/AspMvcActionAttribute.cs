using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000569 RID: 1385
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter)]
	public sealed class AspMvcActionAttribute : Attribute
	{
		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06002A46 RID: 10822 RVA: 0x00098302 File Offset: 0x00096502
		// (set) Token: 0x06002A47 RID: 10823 RVA: 0x0009830A File Offset: 0x0009650A
		[UsedImplicitly]
		public string AnonymousProperty { get; private set; }

		// Token: 0x06002A48 RID: 10824 RVA: 0x00012024 File Offset: 0x00010224
		public AspMvcActionAttribute()
		{
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x00098313 File Offset: 0x00096513
		public AspMvcActionAttribute(string anonymousProperty)
		{
			this.AnonymousProperty = anonymousProperty;
		}
	}
}
