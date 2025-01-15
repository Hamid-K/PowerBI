using System;
using System.Collections.Generic;
using Microsoft.InfoNav;

namespace Microsoft.PowerBI.Data.ModelSchemaAnalysis
{
	// Token: 0x02000005 RID: 5
	public abstract class MeasureCalculationKind
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		internal MeasureCalculationKind(IEnumerable<IConceptualEntity> variesBy)
		{
			this.VariesBy = variesBy.AsReadOnlyList<IConceptualEntity>();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002074 File Offset: 0x00000274
		public IReadOnlyList<IConceptualEntity> VariesBy { get; }

		// Token: 0x06000005 RID: 5
		public abstract void Accept(MeasureCalculationKindVisitor visitor);
	}
}
