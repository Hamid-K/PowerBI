using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x0200000C RID: 12
	internal class ColumnProvider
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00003A94 File Offset: 0x00001C94
		public ColumnProvider()
		{
			this.columns = new List<MdxColumn>();
			this.columnIndices = new Dictionary<string, int>();
			this.fieldIndices = new Dictionary<string, int>();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003AC0 File Offset: 0x00001CC0
		internal ColumnProvider(IEnumerable<MdxColumn> columnList)
			: this()
		{
			foreach (MdxColumn mdxColumn in columnList)
			{
				this.Add(mdxColumn);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003B10 File Offset: 0x00001D10
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00003B18 File Offset: 0x00001D18
		public string Table { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003B21 File Offset: 0x00001D21
		public List<MdxColumn> Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x1700003F RID: 63
		public MdxColumn this[int index]
		{
			get
			{
				if (this.IsValidIndex(index))
				{
					return this.columns[index];
				}
				throw new IndexOutOfRangeException(Resources.BadIndex(index, this.columns.Count));
			}
			set
			{
				if (this.IsValidIndex(index))
				{
					this.columns[index] = value;
				}
				throw new IndexOutOfRangeException(Resources.BadIndex(index, this.columns.Count));
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003BA3 File Offset: 0x00001DA3
		public int ColumnCount
		{
			get
			{
				return this.columns.Count;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003BB0 File Offset: 0x00001DB0
		private HashSet<int> DimensionIndices
		{
			get
			{
				if (this.dimensionIndices == null)
				{
					this.dimensionIndices = new HashSet<int>(from c in this.columns
						where !c.IsKeyFigure
						select c.ColumnOrdinal);
				}
				return this.dimensionIndices;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003C24 File Offset: 0x00001E24
		public virtual int Add(MdxColumn column)
		{
			int count = this.columns.Count;
			column.ColumnOrdinal = count;
			this.columnIndices[column.ColumnName] = count;
			if (!string.IsNullOrEmpty(column.FieldName))
			{
				this.fieldIndices[column.FieldName] = count;
			}
			this.columns.Add(column);
			this.dimensionIndices = null;
			return count;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003C8C File Offset: 0x00001E8C
		public object[] StartBuildingRecord()
		{
			object[] array = new object[this.ColumnCount];
			for (int i = 0; i < this.ColumnCount; i++)
			{
				array[i] = (this.DimensionIndices.Contains(i) ? string.Empty : DBNull.Value);
			}
			return array;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003CD4 File Offset: 0x00001ED4
		public virtual void FinishBuildingRecord(object[] row)
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003CD6 File Offset: 0x00001ED6
		public bool IsValidIndex(int index)
		{
			return index >= 0 && index < this.columns.Count;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003CEC File Offset: 0x00001EEC
		public bool ContainsColumn(string columnName)
		{
			return this.columnIndices.ContainsKey(columnName);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003CFA File Offset: 0x00001EFA
		public bool TryGetColumnIndex(string columnName, out int idx)
		{
			return this.columnIndices.TryGetValue(columnName, out idx);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003D09 File Offset: 0x00001F09
		public bool TryGetFieldIndex(string fieldName, out int idx)
		{
			return this.fieldIndices.TryGetValue(fieldName, out idx);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003D18 File Offset: 0x00001F18
		public bool TryGetColumnName(int i, out string columnName)
		{
			if (this.IsValidIndex(i))
			{
				columnName = this.columns[i].ColumnName;
				return true;
			}
			columnName = null;
			return false;
		}

		// Token: 0x04000021 RID: 33
		protected readonly List<MdxColumn> columns;

		// Token: 0x04000022 RID: 34
		private readonly Dictionary<string, int> columnIndices;

		// Token: 0x04000023 RID: 35
		private readonly Dictionary<string, int> fieldIndices;

		// Token: 0x04000024 RID: 36
		private HashSet<int> dimensionIndices;
	}
}
