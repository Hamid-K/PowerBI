using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000015 RID: 21
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class Bit64Column : Column
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00003730 File Offset: 0x00001930
		protected Bit64Column(DBTYPE type, int maxRowCount)
		{
			this.type = type;
			this.values = new ulong[maxRowCount];
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000374B File Offset: 0x0000194B
		private static DBLENGTH Length
		{
			get
			{
				return DbLength.Eight;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003752 File Offset: 0x00001952
		public override int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000375A File Offset: 0x0000195A
		public override void Clear()
		{
			this.rowCount = 0;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003763 File Offset: 0x00001963
		protected void Add64(long value)
		{
			this.Add64((ulong)value);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000376C File Offset: 0x0000196C
		protected unsafe void Add64(double value)
		{
			this.Add64((ulong)(*(long*)(&value)));
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003778 File Offset: 0x00001978
		protected void Add64(ulong value)
		{
			ulong[] array = this.values;
			int num = this.rowCount;
			this.rowCount = num + 1;
			array[num] = value;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000379E File Offset: 0x0000199E
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe sealed override void AddValue(DBTYPE type, void* value, int length)
		{
			this.Add64((ulong)(*(long*)value));
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000037A8 File Offset: 0x000019A8
		public override void AddNull()
		{
			this.Add64(0L);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000037B2 File Offset: 0x000019B2
		protected ulong Get64(int row)
		{
			return this.values[row];
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000037BC File Offset: 0x000019BC
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			ulong num = this.values[row];
			if (binding.DestType == this.type)
			{
				destLength = Bit64Column.Length;
				*(long*)destValue = (long)num;
				return DBSTATUS.S_OK;
			}
			DBSTATUS dbstatus;
			dataConvert.DataConvert(this.type, binding.DestType, Bit64Column.Length, out destLength, (void*)(&num), (void*)destValue, binding.DestMaxLength, DBSTATUS.S_OK, out dbstatus, binding.Precision, binding.Scale, DBDATACONVERT.DEFAULT);
			return dbstatus;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003827 File Offset: 0x00001A27
		public override bool IsNull(int row)
		{
			return false;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000382A File Offset: 0x00001A2A
		public override void Serialize(PageWriter writer)
		{
			writer.WriteInt32(this.rowCount);
			writer.WriteArray(this.values, 0, this.rowCount);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000384B File Offset: 0x00001A4B
		public override void Deserialize(PageReader reader)
		{
			this.rowCount = reader.ReadInt32();
			reader.ReadArray(this.values, 0, this.rowCount);
		}

		// Token: 0x04000039 RID: 57
		private readonly DBTYPE type;

		// Token: 0x0400003A RID: 58
		private readonly ulong[] values;

		// Token: 0x0400003B RID: 59
		private int rowCount;
	}
}
