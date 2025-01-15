using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E62 RID: 7778
	internal class ByteColumn : Bit8Column
	{
		// Token: 0x0600BF81 RID: 49025 RVA: 0x0026A99E File Offset: 0x00268B9E
		public ByteColumn(int maxRowCount)
			: base(DBTYPE.UI1, maxRowCount)
		{
		}

		// Token: 0x17002EFD RID: 12029
		// (get) Token: 0x0600BF82 RID: 49026 RVA: 0x00075E2C File Offset: 0x0007402C
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Byte;
			}
		}

		// Token: 0x0600BF83 RID: 49027 RVA: 0x0026A9A9 File Offset: 0x00268BA9
		public void AddValue(byte value)
		{
			base.Add8(value);
		}

		// Token: 0x0600BF84 RID: 49028 RVA: 0x0026A9B2 File Offset: 0x00268BB2
		public override void AddValue(object value)
		{
			this.AddValue((byte)value);
		}

		// Token: 0x0600BF85 RID: 49029 RVA: 0x0026A9C0 File Offset: 0x00268BC0
		public override void AddByte(byte value)
		{
			this.AddValue(value);
		}

		// Token: 0x0600BF86 RID: 49030 RVA: 0x0026A9C9 File Offset: 0x00268BC9
		public override bool TryAddValue(object value)
		{
			if (value is byte)
			{
				this.AddValue((byte)value);
				return true;
			}
			return false;
		}

		// Token: 0x0600BF87 RID: 49031 RVA: 0x0026A9E2 File Offset: 0x00268BE2
		public override byte GetByte(int row)
		{
			return base.Get8(row);
		}

		// Token: 0x0600BF88 RID: 49032 RVA: 0x0026A9EB File Offset: 0x00268BEB
		public override object GetObject(int row)
		{
			return this.GetByte(row);
		}

		// Token: 0x0600BF89 RID: 49033 RVA: 0x0026A9FC File Offset: 0x00268BFC
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			byte b = base.Get8(row);
			if (binding.DestType == DBTYPE.UI1)
			{
				destLength = DbLength.One;
				*destValue = b;
				return DBSTATUS.S_OK;
			}
			if (binding.DestType == DBTYPE.I8)
			{
				destLength = DbLength.Eight;
				*(long*)destValue = (long)((ulong)b);
				return DBSTATUS.S_OK;
			}
			if (binding.DestType == DBTYPE.R8)
			{
				destLength = DbLength.Eight;
				*(double*)destValue = (double)b;
				return DBSTATUS.S_OK;
			}
			return dataConvert.DataConvert(b, binding, destValue, out destLength);
		}
	}
}
