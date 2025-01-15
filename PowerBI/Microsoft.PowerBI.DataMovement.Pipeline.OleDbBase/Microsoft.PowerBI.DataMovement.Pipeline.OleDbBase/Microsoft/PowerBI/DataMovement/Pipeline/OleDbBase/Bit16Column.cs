using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000013 RID: 19
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class Bit16Column : Column
	{
		// Token: 0x0600008A RID: 138 RVA: 0x000034C0 File Offset: 0x000016C0
		protected Bit16Column(DBTYPE type, int maxRowCount)
		{
			this.type = type;
			this.values = new ushort[maxRowCount];
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000034DB File Offset: 0x000016DB
		private static DBLENGTH Length
		{
			get
			{
				return DbLength.Two;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000034E2 File Offset: 0x000016E2
		public override int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000034EA File Offset: 0x000016EA
		public override void Clear()
		{
			this.rowCount = 0;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000034F3 File Offset: 0x000016F3
		protected void Add16(short value)
		{
			this.Add16((ushort)value);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003500 File Offset: 0x00001700
		protected void Add16(ushort value)
		{
			ushort[] array = this.values;
			int num = this.rowCount;
			this.rowCount = num + 1;
			array[num] = value;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003526 File Offset: 0x00001726
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe sealed override void AddValue(DBTYPE type, void* value, int length)
		{
			this.Add16(*(ushort*)value);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003530 File Offset: 0x00001730
		public override void AddNull()
		{
			this.Add16(0);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003539 File Offset: 0x00001739
		protected ushort Get16(int row)
		{
			return this.values[row];
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003544 File Offset: 0x00001744
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			ushort num = this.values[row];
			if (binding.DestType == this.type)
			{
				destLength = Bit16Column.Length;
				*(short*)destValue = (short)num;
				return DBSTATUS.S_OK;
			}
			DBSTATUS dbstatus;
			dataConvert.DataConvert(this.type, binding.DestType, Bit16Column.Length, out destLength, (void*)(&num), (void*)destValue, binding.DestMaxLength, DBSTATUS.S_OK, out dbstatus, binding.Precision, binding.Scale, DBDATACONVERT.DEFAULT);
			return dbstatus;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000035AF File Offset: 0x000017AF
		public override bool IsNull(int row)
		{
			return false;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000035B2 File Offset: 0x000017B2
		public override void Serialize(PageWriter writer)
		{
			writer.WriteInt32(this.rowCount);
			writer.WriteArray(this.values, 0, this.rowCount);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000035D3 File Offset: 0x000017D3
		public override void Deserialize(PageReader reader)
		{
			this.rowCount = reader.ReadInt32();
			reader.ReadArray(this.values, 0, this.rowCount);
		}

		// Token: 0x04000033 RID: 51
		private readonly DBTYPE type;

		// Token: 0x04000034 RID: 52
		private readonly ushort[] values;

		// Token: 0x04000035 RID: 53
		private int rowCount;
	}
}
