using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200027E RID: 638
	public sealed class PreviewItemResponseShape
	{
		// Token: 0x06001657 RID: 5719 RVA: 0x0003DD4B File Offset: 0x0003CD4B
		public PreviewItemResponseShape()
		{
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0003DD53 File Offset: 0x0003CD53
		public PreviewItemResponseShape(PreviewItemBaseShape baseShape, ExtendedPropertyDefinition[] additionalProperties)
		{
			this.BaseShape = baseShape;
			this.AdditionalProperties = additionalProperties;
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x0003DD69 File Offset: 0x0003CD69
		// (set) Token: 0x0600165A RID: 5722 RVA: 0x0003DD71 File Offset: 0x0003CD71
		public PreviewItemBaseShape BaseShape { get; set; }

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x0003DD7A File Offset: 0x0003CD7A
		// (set) Token: 0x0600165C RID: 5724 RVA: 0x0003DD82 File Offset: 0x0003CD82
		public ExtendedPropertyDefinition[] AdditionalProperties { get; set; }
	}
}
