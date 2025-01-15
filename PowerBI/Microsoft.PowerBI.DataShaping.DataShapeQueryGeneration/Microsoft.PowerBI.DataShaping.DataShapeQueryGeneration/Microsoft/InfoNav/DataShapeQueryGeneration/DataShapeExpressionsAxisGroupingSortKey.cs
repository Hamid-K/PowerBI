using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000011 RID: 17
	internal class DataShapeExpressionsAxisGroupingSortKey
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00004B55 File Offset: 0x00002D55
		internal DataShapeExpressionsAxisGroupingSortKey(string calcId, SortDirection sortDirection, IConceptualProperty sourceField, int? selectIndex)
		{
			this.Calc = calcId;
			this.SortDirection = sortDirection;
			this.SourceField = sourceField;
			this.Select = selectIndex;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00004B7A File Offset: 0x00002D7A
		internal int? Select { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004B82 File Offset: 0x00002D82
		internal string Calc { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004B8A File Offset: 0x00002D8A
		internal IConceptualProperty SourceField { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004B92 File Offset: 0x00002D92
		internal SortDirection SortDirection { get; }
	}
}
