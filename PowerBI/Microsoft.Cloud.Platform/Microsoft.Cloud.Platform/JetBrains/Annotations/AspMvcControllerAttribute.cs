using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200056B RID: 1387
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter)]
	public sealed class AspMvcControllerAttribute : Attribute
	{
		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06002A4E RID: 10830 RVA: 0x0009834A File Offset: 0x0009654A
		// (set) Token: 0x06002A4F RID: 10831 RVA: 0x00098352 File Offset: 0x00096552
		[UsedImplicitly]
		public string AnonymousProperty { get; private set; }

		// Token: 0x06002A50 RID: 10832 RVA: 0x00012024 File Offset: 0x00010224
		public AspMvcControllerAttribute()
		{
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x0009835B File Offset: 0x0009655B
		public AspMvcControllerAttribute(string anonymousProperty)
		{
			this.AnonymousProperty = anonymousProperty;
		}
	}
}
