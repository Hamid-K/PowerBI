using System;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E78 RID: 7800
	internal class NullColumn : Column
	{
		// Token: 0x0600C042 RID: 49218 RVA: 0x0026BACA File Offset: 0x00269CCA
		public NullColumn(int rowCount = 0)
		{
			this.rowCount = rowCount;
		}

		// Token: 0x17002F16 RID: 12054
		// (get) Token: 0x0600C043 RID: 49219 RVA: 0x00243C87 File Offset: 0x00241E87
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Null;
			}
		}

		// Token: 0x17002F17 RID: 12055
		// (get) Token: 0x0600C044 RID: 49220 RVA: 0x0026BAD9 File Offset: 0x00269CD9
		public override int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x0600C045 RID: 49221 RVA: 0x0026BAE1 File Offset: 0x00269CE1
		public override void AddValue(object value)
		{
			this.AddNull();
		}

		// Token: 0x0600C046 RID: 49222 RVA: 0x00002105 File Offset: 0x00000305
		public override bool TryAddValue(object value)
		{
			return false;
		}

		// Token: 0x0600C047 RID: 49223 RVA: 0x0026BAE9 File Offset: 0x00269CE9
		public override void AddNull()
		{
			this.rowCount++;
		}

		// Token: 0x0600C048 RID: 49224 RVA: 0x0026BAE1 File Offset: 0x00269CE1
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			this.AddNull();
		}

		// Token: 0x0600C049 RID: 49225 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsNull(int row)
		{
			return true;
		}

		// Token: 0x0600C04A RID: 49226 RVA: 0x0026BAF9 File Offset: 0x00269CF9
		public override void Clear()
		{
			this.rowCount = 0;
		}

		// Token: 0x0600C04B RID: 49227 RVA: 0x001D17D8 File Offset: 0x001CF9D8
		public override object GetObject(int row)
		{
			return DBNull.Value;
		}

		// Token: 0x0600C04C RID: 49228 RVA: 0x0026BB02 File Offset: 0x00269D02
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			destLength = DbLength.Zero;
			return DBSTATUS.S_ISNULL;
		}

		// Token: 0x0600C04D RID: 49229 RVA: 0x0026BB11 File Offset: 0x00269D11
		public override void Serialize(PageWriter writer)
		{
			writer.WriteInt32(this.rowCount);
		}

		// Token: 0x0600C04E RID: 49230 RVA: 0x0026BB1F File Offset: 0x00269D1F
		public override void Deserialize(PageReader reader)
		{
			this.rowCount = reader.ReadInt32();
		}

		// Token: 0x04006143 RID: 24899
		private int rowCount;
	}
}
