using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.QueryExecution;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Analytics
{
	// Token: 0x020000B5 RID: 181
	internal sealed class MappingEnumerableToResultSetAdapter : EnumerableToResultSetAdapter
	{
		// Token: 0x060004A7 RID: 1191 RVA: 0x0000E0D8 File Offset: 0x0000C2D8
		internal MappingEnumerableToResultSetAdapter(IEnumerable<IDataRow> input, ResultSetSchema schema, IReadOnlyList<int> rowColumnMapping)
			: base(input, schema)
		{
			this._rowColumnMapping = rowColumnMapping;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000E0EC File Offset: 0x0000C2EC
		public override IDataRow ReadRow()
		{
			IDataRow dataRow = base.ReadRow();
			if (dataRow == null)
			{
				return null;
			}
			return new MappingDataRow(dataRow, this._rowColumnMapping);
		}

		// Token: 0x0400025F RID: 607
		private readonly IReadOnlyList<int> _rowColumnMapping;
	}
}
