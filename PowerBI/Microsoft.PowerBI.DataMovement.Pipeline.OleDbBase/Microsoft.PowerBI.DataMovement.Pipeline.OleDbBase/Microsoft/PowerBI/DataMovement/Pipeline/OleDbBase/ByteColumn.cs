using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000018 RID: 24
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class ByteColumn : Bit8Column
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x000039E4 File Offset: 0x00001BE4
		internal ByteColumn(int maxRowCount)
			: base(DBTYPE.UI1, maxRowCount)
		{
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000039EF File Offset: 0x00001BEF
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Byte;
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000039F2 File Offset: 0x00001BF2
		public override void AddValue(object value)
		{
			base.Add8((byte)value);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003A00 File Offset: 0x00001C00
		public override bool TryAddValue(object value)
		{
			if (value is byte)
			{
				base.Add8((byte)value);
				return true;
			}
			return false;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003A19 File Offset: 0x00001C19
		public override byte GetByte(int row)
		{
			return base.Get8(row);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003A22 File Offset: 0x00001C22
		public override object GetObject(int row)
		{
			return this.GetByte(row);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003A30 File Offset: 0x00001C30
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
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
			return base.GetValue(row, dataConvert, binding, destValue, out destLength);
		}
	}
}
