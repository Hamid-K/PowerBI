using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x02000123 RID: 291
	public class ColumnGroupingAnnotation
	{
		// Token: 0x060007A0 RID: 1952 RVA: 0x0000FDC1 File Offset: 0x0000DFC1
		public ColumnGroupingAnnotation(IReadOnlyList<IConceptualColumn> columnsWithThisAsIdentity)
		{
			this.ColumnsWithThisAsIdentity = columnsWithThisAsIdentity;
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0000FDD0 File Offset: 0x0000DFD0
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x0000FDD8 File Offset: 0x0000DFD8
		public IReadOnlyList<IConceptualColumn> ColumnsWithThisAsIdentity { get; private set; }

		// Token: 0x060007A3 RID: 1955 RVA: 0x0000FDE4 File Offset: 0x0000DFE4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("\t{0}:", "ColumnsWithThisAsIdentity");
			if (!this.ColumnsWithThisAsIdentity.IsNullOrEmpty<IConceptualColumn>())
			{
				stringBuilder.AppendMany(this.ColumnsWithThisAsIdentity.OrderBy((IConceptualColumn f) => f.Entity.Name + "." + f.Name), ",", (IConceptualColumn prop) => prop.Entity.Name + "." + prop.Name);
			}
			return stringBuilder.ToString();
		}
	}
}
