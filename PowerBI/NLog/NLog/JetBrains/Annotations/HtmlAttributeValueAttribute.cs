using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001EC RID: 492
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
	internal sealed class HtmlAttributeValueAttribute : Attribute
	{
		// Token: 0x0600146E RID: 5230 RVA: 0x00036A93 File Offset: 0x00034C93
		public HtmlAttributeValueAttribute([NotNull] string name)
		{
			this.Name = name;
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x00036AA2 File Offset: 0x00034CA2
		// (set) Token: 0x06001470 RID: 5232 RVA: 0x00036AAA File Offset: 0x00034CAA
		[NotNull]
		public string Name { get; private set; }
	}
}
