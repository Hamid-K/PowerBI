using System;
using System.Collections.Generic;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000141 RID: 321
	public sealed class SimpleSchema : ISchema
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00022C1A File Offset: 0x00020E1A
		public int ColumnCount
		{
			get
			{
				return this._types.Length;
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00022C24 File Offset: 0x00020E24
		public SimpleSchema(IExceptionContext ectx, params KeyValuePair<string, ColumnType>[] columns)
		{
			this._ectx = ectx;
			Contracts.CheckNonEmpty<KeyValuePair<string, ColumnType>>(this._ectx, columns, "columns");
			this._names = new string[columns.Length];
			this._types = new ColumnType[columns.Length];
			this._columnNameMap = new Dictionary<string, int>();
			for (int i = 0; i < columns.Length; i++)
			{
				this._names[i] = columns[i].Key;
				this._types[i] = columns[i].Value;
				if (this._columnNameMap.ContainsKey(columns[i].Key))
				{
					throw Contracts.ExceptParam(ectx, "Duplicate column name: '{0}'", columns[i].Key);
				}
				this._columnNameMap[columns[i].Key] = i;
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00022CF6 File Offset: 0x00020EF6
		public bool TryGetColumnIndex(string name, out int col)
		{
			return this._columnNameMap.TryGetValue(name, out col);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00022D05 File Offset: 0x00020F05
		public string GetColumnName(int col)
		{
			Contracts.CheckParam(this._ectx, 0 <= col && col < this.ColumnCount, "col");
			return this._names[col];
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00022D2F File Offset: 0x00020F2F
		public ColumnType GetColumnType(int col)
		{
			Contracts.CheckParam(this._ectx, 0 <= col && col < this.ColumnCount, "col");
			return this._types[col];
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00022E40 File Offset: 0x00021040
		public IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int col)
		{
			Contracts.CheckParam(this._ectx, 0 <= col && col < this.ColumnCount, "col");
			yield break;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00022E64 File Offset: 0x00021064
		public ColumnType GetMetadataTypeOrNull(string kind, int col)
		{
			Contracts.CheckParam(this._ectx, 0 <= col && col < this.ColumnCount, "col");
			return null;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00022E87 File Offset: 0x00021087
		public void GetMetadata<TValue>(string kind, int col, ref TValue value)
		{
			Contracts.CheckParam(this._ectx, 0 <= col && col < this.ColumnCount, "col");
			throw MetadataUtils.ExceptGetMetadata(this._ectx);
		}

		// Token: 0x0400034E RID: 846
		private readonly IExceptionContext _ectx;

		// Token: 0x0400034F RID: 847
		private readonly string[] _names;

		// Token: 0x04000350 RID: 848
		private readonly ColumnType[] _types;

		// Token: 0x04000351 RID: 849
		private readonly Dictionary<string, int> _columnNameMap;
	}
}
