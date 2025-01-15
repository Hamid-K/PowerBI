using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000B6 RID: 182
	public class RowNumberColumn : Column
	{
		// Token: 0x06000B3F RID: 2879 RVA: 0x0005C46D File Offset: 0x0005A66D
		internal RowNumberColumn()
		{
			this.OnAfterConstructor();
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0005C47B File Offset: 0x0005A67B
		internal RowNumberColumn(IEqualityComparer<string> comparer)
			: base(comparer)
		{
			this.OnAfterConstructor();
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0005C48A File Offset: 0x0005A68A
		private void OnAfterConstructor()
		{
			this.body.Type = ColumnType.RowNumber;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0005C498 File Offset: 0x0005A698
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new RowNumberColumn();
		}
	}
}
