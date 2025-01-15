using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.PowerBI.Data.ModelSchemaAnalysis
{
	// Token: 0x02000006 RID: 6
	[ImmutableObject(true)]
	public sealed class AdditiveMeasureCalculationKind : MeasureCalculationKind
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000207C File Offset: 0x0000027C
		public AdditiveMeasureCalculationKind(IReadOnlyList<IConceptualEntity> distributiveBy)
			: base(distributiveBy)
		{
			this.DistributiveBy = distributiveBy;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000208C File Offset: 0x0000028C
		public IReadOnlyList<IConceptualEntity> DistributiveBy { get; }

		// Token: 0x06000008 RID: 8 RVA: 0x00002094 File Offset: 0x00000294
		public override void Accept(MeasureCalculationKindVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
