using System;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E5B RID: 7771
	internal class NullableColumn : DelegatingColumn
	{
		// Token: 0x0600BF31 RID: 48945 RVA: 0x0026A311 File Offset: 0x00268511
		public NullableColumn(Column column, int maxRowCount)
			: base(column)
		{
			this.values = new ulong[(maxRowCount + 63) / 64];
		}

		// Token: 0x17002EF5 RID: 12021
		// (get) Token: 0x0600BF32 RID: 48946 RVA: 0x0026A32C File Offset: 0x0026852C
		public override int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x17002EF6 RID: 12022
		// (get) Token: 0x0600BF33 RID: 48947 RVA: 0x0026A334 File Offset: 0x00268534
		private int ValueCount
		{
			get
			{
				return (this.rowCount + 63) / 64;
			}
		}

		// Token: 0x0600BF34 RID: 48948 RVA: 0x0026A342 File Offset: 0x00268542
		public override void Clear()
		{
			if (this.hasNull)
			{
				Array.Clear(this.values, 0, this.ValueCount);
				this.hasNull = false;
			}
			this.rowCount = 0;
			base.Clear();
		}

		// Token: 0x0600BF35 RID: 48949 RVA: 0x0026A374 File Offset: 0x00268574
		public override void AddNull()
		{
			base.AddNull();
			int num = this.rowCount / 64;
			ulong num2 = 1UL << this.rowCount % 64;
			this.values[num] |= num2;
			this.rowCount++;
			this.hasNull = true;
		}

		// Token: 0x0600BF36 RID: 48950 RVA: 0x0026A3C7 File Offset: 0x002685C7
		protected override void AddNotNull()
		{
			this.rowCount++;
		}

		// Token: 0x0600BF37 RID: 48951 RVA: 0x0026A3D8 File Offset: 0x002685D8
		public override bool IsNull(int row)
		{
			if (this.hasNull)
			{
				int num = row / 64;
				ulong num2 = 1UL << row % 64;
				return (this.values[num] & num2) > 0UL;
			}
			return false;
		}

		// Token: 0x0600BF38 RID: 48952 RVA: 0x0026A40D File Offset: 0x0026860D
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			if (this.IsNull(row))
			{
				destLength = DbLength.Zero;
				return DBSTATUS.S_ISNULL;
			}
			return base.GetValue(row, dataConvert, binding, destValue, out destLength);
		}

		// Token: 0x0600BF39 RID: 48953 RVA: 0x0026A433 File Offset: 0x00268633
		public override string GetString(int row)
		{
			if (this.IsNull(row))
			{
				return null;
			}
			return base.GetString(row);
		}

		// Token: 0x0600BF3A RID: 48954 RVA: 0x0026A447 File Offset: 0x00268647
		public override object GetObject(int row)
		{
			if (this.IsNull(row))
			{
				return DBNull.Value;
			}
			return base.GetObject(row);
		}

		// Token: 0x0600BF3B RID: 48955 RVA: 0x0026A45F File Offset: 0x0026865F
		public override void Serialize(PageWriter writer)
		{
			writer.WriteInt32(this.rowCount);
			if (this.hasNull)
			{
				writer.WriteInt32(1);
				writer.WriteArray(this.values, 0, this.ValueCount);
			}
			else
			{
				writer.WriteInt32(0);
			}
			base.Serialize(writer);
		}

		// Token: 0x0600BF3C RID: 48956 RVA: 0x0026A49F File Offset: 0x0026869F
		public override void Deserialize(PageReader reader)
		{
			this.rowCount = reader.ReadInt32();
			this.hasNull = reader.ReadInt32() == 1;
			if (this.hasNull)
			{
				reader.ReadArray(this.values, 0, this.ValueCount);
			}
			base.Deserialize(reader);
		}

		// Token: 0x0400612C RID: 24876
		private const int BitsPerValue = 64;

		// Token: 0x0400612D RID: 24877
		private readonly ulong[] values;

		// Token: 0x0400612E RID: 24878
		private int rowCount;

		// Token: 0x0400612F RID: 24879
		private bool hasNull;
	}
}
