using System;
using System.ComponentModel;

namespace Microsoft.PowerBI.Data.ModelSchemaAnalysis
{
	// Token: 0x02000009 RID: 9
	[ImmutableObject(true)]
	public sealed class ConstantMeasureCalculationKind : MeasureCalculationKind
	{
		// Token: 0x06000011 RID: 17 RVA: 0x0000210B File Offset: 0x0000030B
		private ConstantMeasureCalculationKind()
			: base(null)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002114 File Offset: 0x00000314
		public override void Accept(MeasureCalculationKindVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0400002E RID: 46
		public static readonly ConstantMeasureCalculationKind Instance = new ConstantMeasureCalculationKind();
	}
}
