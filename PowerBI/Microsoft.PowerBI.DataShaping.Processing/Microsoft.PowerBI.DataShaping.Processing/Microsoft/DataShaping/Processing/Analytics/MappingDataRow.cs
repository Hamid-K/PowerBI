using System;
using System.Collections.Generic;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Analytics
{
	// Token: 0x020000B4 RID: 180
	internal sealed class MappingDataRow : IDataRow
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x0000DFE2 File Offset: 0x0000C1E2
		internal MappingDataRow(IDataRow row, IReadOnlyList<int> columnMapping)
		{
			this._row = row;
			this._columpMapping = columnMapping;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000DFF8 File Offset: 0x0000C1F8
		public object GetObject(int index)
		{
			return this._row.GetObject(this.GetActualIndex(index));
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000E00C File Offset: 0x0000C20C
		public double? GetAsDouble(int index)
		{
			return this._row.GetAsDouble(this.GetActualIndex(index));
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000E020 File Offset: 0x0000C220
		public long? GetAsInt64(int index)
		{
			return this._row.GetAsInt64(this.GetActualIndex(index));
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000E034 File Offset: 0x0000C234
		private int GetActualIndex(int index)
		{
			return this._columpMapping[index];
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0000E042 File Offset: 0x0000C242
		public int Count
		{
			get
			{
				return this._columpMapping.Count;
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000E050 File Offset: 0x0000C250
		public IDataRow AddColumns(IReadOnlyList<object> columns)
		{
			int[] array = new int[this._columpMapping.Count + columns.Count];
			for (int i = 0; i < this._columpMapping.Count; i++)
			{
				array[i] = this._columpMapping[i];
			}
			int count = this._row.Count;
			for (int j = this._columpMapping.Count; j < array.Length; j++)
			{
				array[j] = count++;
			}
			return new MappingDataRow(this._row.AddColumns(columns), array);
		}

		// Token: 0x0400025D RID: 605
		private readonly IDataRow _row;

		// Token: 0x0400025E RID: 606
		private readonly IReadOnlyList<int> _columpMapping;
	}
}
