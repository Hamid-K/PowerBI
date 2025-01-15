using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000017 RID: 23
	internal sealed class SortKeyVisitorContext
	{
		// Token: 0x060000DC RID: 220 RVA: 0x000055BA File Offset: 0x000037BA
		internal SortKeyVisitorContext(bool hasNestedGroups, IReadOnlyList<ProjectedDsqExpression> groupMeasures)
		{
			this._hasNestedGroups = hasNestedGroups;
			this._groupMeasures = groupMeasures;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000055D0 File Offset: 0x000037D0
		public bool HasNestedGroups
		{
			get
			{
				return this._hasNestedGroups;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000055D8 File Offset: 0x000037D8
		public IReadOnlyList<ProjectedDsqExpression> GroupMeasures
		{
			get
			{
				return this._groupMeasures;
			}
		}

		// Token: 0x04000071 RID: 113
		private readonly bool _hasNestedGroups;

		// Token: 0x04000072 RID: 114
		private readonly IReadOnlyList<ProjectedDsqExpression> _groupMeasures;
	}
}
