using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200056A RID: 1386
	[AttributeUsage(AttributeTargets.Parameter)]
	public sealed class AspMvcAreaAttribute : PathReferenceAttribute
	{
		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06002A4A RID: 10826 RVA: 0x00098322 File Offset: 0x00096522
		// (set) Token: 0x06002A4B RID: 10827 RVA: 0x0009832A File Offset: 0x0009652A
		[UsedImplicitly]
		public string AnonymousProperty { get; private set; }

		// Token: 0x06002A4C RID: 10828 RVA: 0x00098333 File Offset: 0x00096533
		[UsedImplicitly]
		public AspMvcAreaAttribute()
		{
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x0009833B File Offset: 0x0009653B
		public AspMvcAreaAttribute(string anonymousProperty)
		{
			this.AnonymousProperty = anonymousProperty;
		}
	}
}
