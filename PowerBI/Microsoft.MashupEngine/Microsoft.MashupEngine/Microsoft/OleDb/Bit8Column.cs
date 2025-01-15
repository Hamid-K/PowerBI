using System;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E5C RID: 7772
	internal abstract class Bit8Column : Column
	{
		// Token: 0x0600BF3D RID: 48957 RVA: 0x0026A4DE File Offset: 0x002686DE
		public Bit8Column(DBTYPE type, int maxRowCount)
		{
			this.type = type;
			this.values = new byte[maxRowCount];
		}

		// Token: 0x17002EF7 RID: 12023
		// (get) Token: 0x0600BF3E RID: 48958 RVA: 0x0026A4F9 File Offset: 0x002686F9
		public override int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x0600BF3F RID: 48959 RVA: 0x0026A501 File Offset: 0x00268701
		public override void Clear()
		{
			this.rowCount = 0;
		}

		// Token: 0x0600BF40 RID: 48960 RVA: 0x0026A50A File Offset: 0x0026870A
		protected void Add8(sbyte value)
		{
			this.Add8((byte)value);
		}

		// Token: 0x0600BF41 RID: 48961 RVA: 0x0026A514 File Offset: 0x00268714
		protected void Add8(byte value)
		{
			byte[] array = this.values;
			int num = this.rowCount;
			this.rowCount = num + 1;
			array[num] = value;
		}

		// Token: 0x0600BF42 RID: 48962 RVA: 0x0026A53A File Offset: 0x0026873A
		public unsafe sealed override void AddValue(DBTYPE type, void* value, int length)
		{
			this.Add8(*(byte*)value);
		}

		// Token: 0x0600BF43 RID: 48963 RVA: 0x0026A544 File Offset: 0x00268744
		public override void AddNull()
		{
			this.Add8(0);
		}

		// Token: 0x0600BF44 RID: 48964 RVA: 0x0026A54D File Offset: 0x0026874D
		protected byte Get8(int row)
		{
			return this.values[row];
		}

		// Token: 0x0600BF45 RID: 48965 RVA: 0x00002105 File Offset: 0x00000305
		public override bool IsNull(int row)
		{
			return false;
		}

		// Token: 0x0600BF46 RID: 48966 RVA: 0x0026A557 File Offset: 0x00268757
		public override void Serialize(PageWriter writer)
		{
			writer.WriteInt32(this.rowCount);
			writer.WriteArray(this.values, 0, this.rowCount);
		}

		// Token: 0x0600BF47 RID: 48967 RVA: 0x0026A578 File Offset: 0x00268778
		public override void Deserialize(PageReader reader)
		{
			this.rowCount = reader.ReadInt32();
			reader.ReadArray(this.values, 0, this.rowCount);
		}

		// Token: 0x04006130 RID: 24880
		private readonly DBTYPE type;

		// Token: 0x04006131 RID: 24881
		private readonly byte[] values;

		// Token: 0x04006132 RID: 24882
		private int rowCount;
	}
}
