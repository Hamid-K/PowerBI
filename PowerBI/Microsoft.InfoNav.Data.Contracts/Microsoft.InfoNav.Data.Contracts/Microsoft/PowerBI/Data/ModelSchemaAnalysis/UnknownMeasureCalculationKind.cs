using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.PowerBI.Data.ModelSchemaAnalysis
{
	// Token: 0x02000008 RID: 8
	[ImmutableObject(true)]
	public sealed class UnknownMeasureCalculationKind : MeasureCalculationKind
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000020EC File Offset: 0x000002EC
		public UnknownMeasureCalculationKind(IEnumerable<IConceptualEntity> variesBy)
			: base(variesBy)
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000020F5 File Offset: 0x000002F5
		public override void Accept(MeasureCalculationKindVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0400002D RID: 45
		public static readonly UnknownMeasureCalculationKind DefaultInstance = new UnknownMeasureCalculationKind(null);
	}
}
