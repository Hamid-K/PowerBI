using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000014 RID: 20
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class Bit32Column : Column
	{
		// Token: 0x06000097 RID: 151 RVA: 0x000035F4 File Offset: 0x000017F4
		protected Bit32Column(DBTYPE type, int maxRowCount)
		{
			this.type = type;
			this.values = new uint[maxRowCount];
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000360F File Offset: 0x0000180F
		private static DBLENGTH Length
		{
			get
			{
				return DbLength.Four;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003616 File Offset: 0x00001816
		public override int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000361E File Offset: 0x0000181E
		public override void Clear()
		{
			this.rowCount = 0;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003627 File Offset: 0x00001827
		protected void Add32(int value)
		{
			this.Add32((uint)value);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003630 File Offset: 0x00001830
		protected unsafe void Add32(float value)
		{
			this.Add32(*(uint*)(&value));
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000363C File Offset: 0x0000183C
		protected void Add32(uint value)
		{
			uint[] array = this.values;
			int num = this.rowCount;
			this.rowCount = num + 1;
			array[num] = value;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003662 File Offset: 0x00001862
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe sealed override void AddValue(DBTYPE type, void* value, int length)
		{
			this.Add32(*(uint*)value);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000366C File Offset: 0x0000186C
		public override void AddNull()
		{
			this.Add32(0);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003675 File Offset: 0x00001875
		protected uint Get32(int row)
		{
			return this.values[row];
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003680 File Offset: 0x00001880
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			uint num = this.values[row];
			if (binding.DestType == this.type)
			{
				destLength = Bit32Column.Length;
				*(int*)destValue = (int)num;
				return DBSTATUS.S_OK;
			}
			DBSTATUS dbstatus;
			dataConvert.DataConvert(this.type, binding.DestType, Bit32Column.Length, out destLength, (void*)(&num), (void*)destValue, binding.DestMaxLength, DBSTATUS.S_OK, out dbstatus, binding.Precision, binding.Scale, DBDATACONVERT.DEFAULT);
			return dbstatus;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000036EB File Offset: 0x000018EB
		public override bool IsNull(int row)
		{
			return false;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000036EE File Offset: 0x000018EE
		public override void Serialize(PageWriter writer)
		{
			writer.WriteInt32(this.rowCount);
			writer.WriteArray(this.values, 0, this.rowCount);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000370F File Offset: 0x0000190F
		public override void Deserialize(PageReader reader)
		{
			this.rowCount = reader.ReadInt32();
			reader.ReadArray(this.values, 0, this.rowCount);
		}

		// Token: 0x04000036 RID: 54
		private readonly DBTYPE type;

		// Token: 0x04000037 RID: 55
		private readonly uint[] values;

		// Token: 0x04000038 RID: 56
		private int rowCount;
	}
}
