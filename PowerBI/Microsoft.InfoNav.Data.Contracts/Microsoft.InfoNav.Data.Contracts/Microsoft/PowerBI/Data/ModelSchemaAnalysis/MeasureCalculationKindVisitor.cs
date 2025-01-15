using System;

namespace Microsoft.PowerBI.Data.ModelSchemaAnalysis
{
	// Token: 0x0200000C RID: 12
	public abstract class MeasureCalculationKindVisitor
	{
		// Token: 0x0600001C RID: 28
		public abstract void Visit(AdditiveMeasureCalculationKind calcKind);

		// Token: 0x0600001D RID: 29
		public abstract void Visit(RatioMeasureCalculationKind calcKind);

		// Token: 0x0600001E RID: 30
		public abstract void Visit(UnknownMeasureCalculationKind calcKind);

		// Token: 0x0600001F RID: 31
		public abstract void Visit(ConstantMeasureCalculationKind calcKind);
	}
}
