using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000012 RID: 18
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class Bit8Column : Column
	{
		// Token: 0x0600007D RID: 125 RVA: 0x0000338F File Offset: 0x0000158F
		protected Bit8Column(DBTYPE type, int maxRowCount)
		{
			this.type = type;
			this.values = new byte[maxRowCount];
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000033AA File Offset: 0x000015AA
		private static DBLENGTH Length
		{
			get
			{
				return DbLength.One;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000033B1 File Offset: 0x000015B1
		public override int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000033B9 File Offset: 0x000015B9
		public override void Clear()
		{
			this.rowCount = 0;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000033C2 File Offset: 0x000015C2
		protected void Add8(sbyte value)
		{
			this.Add8((byte)value);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000033CC File Offset: 0x000015CC
		protected void Add8(byte value)
		{
			byte[] array = this.values;
			int num = this.rowCount;
			this.rowCount = num + 1;
			array[num] = value;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000033F2 File Offset: 0x000015F2
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe sealed override void AddValue(DBTYPE type, void* value, int length)
		{
			this.Add8(*(byte*)value);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000033FC File Offset: 0x000015FC
		public override void AddNull()
		{
			this.Add8(0);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003405 File Offset: 0x00001605
		protected byte Get8(int row)
		{
			return this.values[row];
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003410 File Offset: 0x00001610
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			byte b = this.values[row];
			if (binding.DestType == this.type)
			{
				destLength = Bit8Column.Length;
				*destValue = b;
				return DBSTATUS.S_OK;
			}
			DBSTATUS dbstatus;
			dataConvert.DataConvert(this.type, binding.DestType, Bit8Column.Length, out destLength, (void*)(&b), (void*)destValue, binding.DestMaxLength, DBSTATUS.S_OK, out dbstatus, binding.Precision, binding.Scale, DBDATACONVERT.DEFAULT);
			return dbstatus;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000347B File Offset: 0x0000167B
		public override bool IsNull(int row)
		{
			return false;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000347E File Offset: 0x0000167E
		public override void Serialize(PageWriter writer)
		{
			writer.WriteInt32(this.rowCount);
			writer.WriteArray(this.values, 0, this.rowCount);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000349F File Offset: 0x0000169F
		public override void Deserialize(PageReader reader)
		{
			this.rowCount = reader.ReadInt32();
			reader.ReadArray(this.values, 0, this.rowCount);
		}

		// Token: 0x04000030 RID: 48
		private readonly DBTYPE type;

		// Token: 0x04000031 RID: 49
		private readonly byte[] values;

		// Token: 0x04000032 RID: 50
		private int rowCount;
	}
}
