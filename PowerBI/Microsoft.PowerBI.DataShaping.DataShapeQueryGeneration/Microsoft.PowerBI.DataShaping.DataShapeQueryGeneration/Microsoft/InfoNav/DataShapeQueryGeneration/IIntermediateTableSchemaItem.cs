using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000CD RID: 205
	internal interface IIntermediateTableSchemaItem
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600075B RID: 1883
		string ValueCalculationId { get; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600075C RID: 1884
		string FormatString { get; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600075D RID: 1885
		IConceptualProperty LineageProperty { get; }

		// Token: 0x0600075E RID: 1886
		bool TryGetRelatedItem(IConceptualProperty lineageProperty, out IIntermediateTableSchemaItem item);
	}
}
