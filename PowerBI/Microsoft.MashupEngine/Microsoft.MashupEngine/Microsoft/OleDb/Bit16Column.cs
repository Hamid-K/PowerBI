using System;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E5D RID: 7773
	internal abstract class Bit16Column : Column
	{
		// Token: 0x0600BF48 RID: 48968 RVA: 0x0026A599 File Offset: 0x00268799
		public Bit16Column(DBTYPE type, int maxRowCount)
		{
			this.type = type;
			this.values = new ushort[maxRowCount];
		}

		// Token: 0x17002EF8 RID: 12024
		// (get) Token: 0x0600BF49 RID: 48969 RVA: 0x0026A5B4 File Offset: 0x002687B4
		public override int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x0600BF4A RID: 48970 RVA: 0x0026A5BC File Offset: 0x002687BC
		public override void Clear()
		{
			this.rowCount = 0;
		}

		// Token: 0x0600BF4B RID: 48971 RVA: 0x0026A5C5 File Offset: 0x002687C5
		protected void Add16(short value)
		{
			this.Add16((ushort)value);
		}

		// Token: 0x0600BF4C RID: 48972 RVA: 0x0026A5D0 File Offset: 0x002687D0
		protected void Add16(ushort value)
		{
			ushort[] array = this.values;
			int num = this.rowCount;
			this.rowCount = num + 1;
			array[num] = value;
		}

		// Token: 0x0600BF4D RID: 48973 RVA: 0x0026A5F6 File Offset: 0x002687F6
		public unsafe sealed override void AddValue(DBTYPE type, void* value, int length)
		{
			this.Add16(*(ushort*)value);
		}

		// Token: 0x0600BF4E RID: 48974 RVA: 0x0026A600 File Offset: 0x00268800
		public override void AddNull()
		{
			this.Add16(0);
		}

		// Token: 0x0600BF4F RID: 48975 RVA: 0x0026A609 File Offset: 0x00268809
		protected ushort Get16(int row)
		{
			return this.values[row];
		}

		// Token: 0x0600BF50 RID: 48976 RVA: 0x00002105 File Offset: 0x00000305
		public override bool IsNull(int row)
		{
			return false;
		}

		// Token: 0x0600BF51 RID: 48977 RVA: 0x0026A613 File Offset: 0x00268813
		public override void Serialize(PageWriter writer)
		{
			writer.WriteInt32(this.rowCount);
			writer.WriteArray(this.values, 0, this.rowCount);
		}

		// Token: 0x0600BF52 RID: 48978 RVA: 0x0026A634 File Offset: 0x00268834
		public override void Deserialize(PageReader reader)
		{
			this.rowCount = reader.ReadInt32();
			reader.ReadArray(this.values, 0, this.rowCount);
		}

		// Token: 0x04006133 RID: 24883
		private readonly DBTYPE type;

		// Token: 0x04006134 RID: 24884
		private readonly ushort[] values;

		// Token: 0x04006135 RID: 24885
		private int rowCount;
	}
}
