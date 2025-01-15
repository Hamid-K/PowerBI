using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001FE RID: 510
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	internal sealed class AspRequiredAttributeAttribute : Attribute
	{
		// Token: 0x06001488 RID: 5256 RVA: 0x00036B8B File Offset: 0x00034D8B
		public AspRequiredAttributeAttribute([NotNull] string attribute)
		{
			this.Attribute = attribute;
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x00036B9A File Offset: 0x00034D9A
		// (set) Token: 0x0600148A RID: 5258 RVA: 0x00036BA2 File Offset: 0x00034DA2
		[NotNull]
		public string Attribute { get; private set; }
	}
}
