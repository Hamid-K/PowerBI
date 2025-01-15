using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Analytics
{
	// Token: 0x020000B8 RID: 184
	[ImmutableObject(true)]
	internal sealed class SchemaRow : ISchemaRow
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x0000E1B9 File Offset: 0x0000C3B9
		internal SchemaRow(IReadOnlyList<IColumn> columns)
		{
			this._list = new FunctionalList<IColumn>(columns);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000E1CD File Offset: 0x0000C3CD
		private SchemaRow(SchemaRow other, IReadOnlyList<IColumn> columns)
		{
			this._list = other._list.Append(columns);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000E1E7 File Offset: 0x0000C3E7
		public IColumn GetColumn(int index)
		{
			return this._list.GetItem(index);
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000E1F5 File Offset: 0x0000C3F5
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000E202 File Offset: 0x0000C402
		public ISchemaRow AddColumns(IReadOnlyList<IColumn> columns)
		{
			return new SchemaRow(this, columns);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000E20B File Offset: 0x0000C40B
		public IColumn CreateColumn(string name, DataType dataType, string role, ISortInformation sortInformation)
		{
			return new Column(name, dataType, role, sortInformation);
		}

		// Token: 0x04000265 RID: 613
		private readonly FunctionalList<IColumn> _list;
	}
}
