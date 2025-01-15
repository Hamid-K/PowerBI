using System;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E5F RID: 7775
	internal abstract class Bit64Column : Column
	{
		// Token: 0x0600BF5F RID: 48991 RVA: 0x0026A71D File Offset: 0x0026891D
		public Bit64Column(DBTYPE type, int maxRowCount)
		{
			this.type = type;
			this.values = new ulong[maxRowCount];
		}

		// Token: 0x17002EFA RID: 12026
		// (get) Token: 0x0600BF60 RID: 48992 RVA: 0x0026A738 File Offset: 0x00268938
		public override int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x0600BF61 RID: 48993 RVA: 0x0026A740 File Offset: 0x00268940
		public override void Clear()
		{
			this.rowCount = 0;
		}

		// Token: 0x0600BF62 RID: 48994 RVA: 0x0026A749 File Offset: 0x00268949
		protected void Add64(long value)
		{
			this.Add64((ulong)value);
		}

		// Token: 0x0600BF63 RID: 48995 RVA: 0x0026A752 File Offset: 0x00268952
		protected unsafe void Add64(double value)
		{
			this.Add64((ulong)(*(long*)(&value)));
		}

		// Token: 0x0600BF64 RID: 48996 RVA: 0x0026A760 File Offset: 0x00268960
		protected void Add64(ulong value)
		{
			ulong[] array = this.values;
			int num = this.rowCount;
			this.rowCount = num + 1;
			array[num] = value;
		}

		// Token: 0x0600BF65 RID: 48997 RVA: 0x0026A786 File Offset: 0x00268986
		public unsafe sealed override void AddValue(DBTYPE type, void* value, int length)
		{
			this.Add64((ulong)(*(long*)value));
		}

		// Token: 0x0600BF66 RID: 48998 RVA: 0x0026A790 File Offset: 0x00268990
		public override void AddNull()
		{
			this.Add64(0L);
		}

		// Token: 0x0600BF67 RID: 48999 RVA: 0x0026A79A File Offset: 0x0026899A
		protected ulong Get64(int row)
		{
			return this.values[row];
		}

		// Token: 0x0600BF68 RID: 49000 RVA: 0x00002105 File Offset: 0x00000305
		public override bool IsNull(int row)
		{
			return false;
		}

		// Token: 0x0600BF69 RID: 49001 RVA: 0x0026A7A4 File Offset: 0x002689A4
		public override void Serialize(PageWriter writer)
		{
			writer.WriteInt32(this.rowCount);
			writer.WriteArray(this.values, 0, this.rowCount);
		}

		// Token: 0x0600BF6A RID: 49002 RVA: 0x0026A7C5 File Offset: 0x002689C5
		public override void Deserialize(PageReader reader)
		{
			this.rowCount = reader.ReadInt32();
			reader.ReadArray(this.values, 0, this.rowCount);
		}

		// Token: 0x04006139 RID: 24889
		private readonly DBTYPE type;

		// Token: 0x0400613A RID: 24890
		private readonly ulong[] values;

		// Token: 0x0400613B RID: 24891
		private int rowCount;
	}
}
