using System;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E5E RID: 7774
	internal abstract class Bit32Column : Column
	{
		// Token: 0x0600BF53 RID: 48979 RVA: 0x0026A655 File Offset: 0x00268855
		public Bit32Column(DBTYPE type, int maxRowCount)
		{
			this.type = type;
			this.values = new uint[maxRowCount];
		}

		// Token: 0x17002EF9 RID: 12025
		// (get) Token: 0x0600BF54 RID: 48980 RVA: 0x0026A670 File Offset: 0x00268870
		public override int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x0600BF55 RID: 48981 RVA: 0x0026A678 File Offset: 0x00268878
		public override void Clear()
		{
			this.rowCount = 0;
		}

		// Token: 0x0600BF56 RID: 48982 RVA: 0x0026A681 File Offset: 0x00268881
		protected void Add32(int value)
		{
			this.Add32((uint)value);
		}

		// Token: 0x0600BF57 RID: 48983 RVA: 0x0026A68A File Offset: 0x0026888A
		protected unsafe void Add32(float value)
		{
			this.Add32(*(uint*)(&value));
		}

		// Token: 0x0600BF58 RID: 48984 RVA: 0x0026A698 File Offset: 0x00268898
		protected void Add32(uint value)
		{
			uint[] array = this.values;
			int num = this.rowCount;
			this.rowCount = num + 1;
			array[num] = value;
		}

		// Token: 0x0600BF59 RID: 48985 RVA: 0x0026A6BE File Offset: 0x002688BE
		public unsafe sealed override void AddValue(DBTYPE type, void* value, int length)
		{
			this.Add32(*(uint*)value);
		}

		// Token: 0x0600BF5A RID: 48986 RVA: 0x0026A6C8 File Offset: 0x002688C8
		public override void AddNull()
		{
			this.Add32(0);
		}

		// Token: 0x0600BF5B RID: 48987 RVA: 0x0026A6D1 File Offset: 0x002688D1
		protected uint Get32(int row)
		{
			return this.values[row];
		}

		// Token: 0x0600BF5C RID: 48988 RVA: 0x00002105 File Offset: 0x00000305
		public override bool IsNull(int row)
		{
			return false;
		}

		// Token: 0x0600BF5D RID: 48989 RVA: 0x0026A6DB File Offset: 0x002688DB
		public override void Serialize(PageWriter writer)
		{
			writer.WriteInt32(this.rowCount);
			writer.WriteArray(this.values, 0, this.rowCount);
		}

		// Token: 0x0600BF5E RID: 48990 RVA: 0x0026A6FC File Offset: 0x002688FC
		public override void Deserialize(PageReader reader)
		{
			this.rowCount = reader.ReadInt32();
			reader.ReadArray(this.values, 0, this.rowCount);
		}

		// Token: 0x04006136 RID: 24886
		private readonly DBTYPE type;

		// Token: 0x04006137 RID: 24887
		private readonly uint[] values;

		// Token: 0x04006138 RID: 24888
		private int rowCount;
	}
}
