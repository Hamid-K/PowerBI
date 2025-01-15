using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x02000124 RID: 292
	internal sealed class WritableColumnGroupingAnnotation : ColumnGroupingAnnotation
	{
		// Token: 0x060007A4 RID: 1956 RVA: 0x0000FE70 File Offset: 0x0000E070
		internal WritableColumnGroupingAnnotation()
			: base(new List<IConceptualColumn>())
		{
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0000FE7D File Offset: 0x0000E07D
		public void AddColumn(IConceptualColumn column)
		{
			((List<IConceptualColumn>)base.ColumnsWithThisAsIdentity).Add(column);
		}
	}
}
