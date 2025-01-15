using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x0200003D RID: 61
	internal sealed class DataTransformTableSchema
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x00005B4E File Offset: 0x00003D4E
		internal DataTransformTableSchema(IList<DataTransformColumn> columns)
		{
			this._columns = columns;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00005B5D File Offset: 0x00003D5D
		internal IList<DataTransformColumn> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x0400010F RID: 271
		private readonly IList<DataTransformColumn> _columns;
	}
}
