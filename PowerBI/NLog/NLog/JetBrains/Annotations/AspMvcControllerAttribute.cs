using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001DF RID: 479
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter)]
	internal sealed class AspMvcControllerAttribute : Attribute
	{
		// Token: 0x0600145B RID: 5211 RVA: 0x000369EB File Offset: 0x00034BEB
		public AspMvcControllerAttribute()
		{
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x000369F3 File Offset: 0x00034BF3
		public AspMvcControllerAttribute([NotNull] string anonymousProperty)
		{
			this.AnonymousProperty = anonymousProperty;
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x00036A02 File Offset: 0x00034C02
		// (set) Token: 0x0600145E RID: 5214 RVA: 0x00036A0A File Offset: 0x00034C0A
		[CanBeNull]
		public string AnonymousProperty { get; private set; }
	}
}
