using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001EB RID: 491
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
	internal sealed class HtmlElementAttributesAttribute : Attribute
	{
		// Token: 0x0600146A RID: 5226 RVA: 0x00036A6B File Offset: 0x00034C6B
		public HtmlElementAttributesAttribute()
		{
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x00036A73 File Offset: 0x00034C73
		public HtmlElementAttributesAttribute([NotNull] string name)
		{
			this.Name = name;
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x00036A82 File Offset: 0x00034C82
		// (set) Token: 0x0600146D RID: 5229 RVA: 0x00036A8A File Offset: 0x00034C8A
		[CanBeNull]
		public string Name { get; private set; }
	}
}
