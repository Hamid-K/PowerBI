using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200001B RID: 27
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class UInt16Column : Bit16Column
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00003C27 File Offset: 0x00001E27
		internal UInt16Column(int maxRowCount)
			: base(DBTYPE.UI2, maxRowCount)
		{
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003C32 File Offset: 0x00001E32
		public override ColumnType Type
		{
			get
			{
				return ColumnType.UInt16;
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003C35 File Offset: 0x00001E35
		public override void AddValue(object value)
		{
			base.Add16((ushort)value);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003C43 File Offset: 0x00001E43
		public override bool TryAddValue(object value)
		{
			if (value is ushort)
			{
				base.Add16((ushort)value);
				return true;
			}
			return false;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003C5C File Offset: 0x00001E5C
		public override object GetObject(int row)
		{
			return base.Get16(row);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003C6C File Offset: 0x00001E6C
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			ushort num = base.Get16(row);
			if (binding.DestType == DBTYPE.UI2)
			{
				destLength = DbLength.Two;
				*(short*)destValue = (short)num;
				return DBSTATUS.S_OK;
			}
			if (binding.DestType == DBTYPE.I8)
			{
				destLength = DbLength.Eight;
				*(long*)destValue = (long)((ulong)num);
				return DBSTATUS.S_OK;
			}
			if (binding.DestType == DBTYPE.R8)
			{
				destLength = DbLength.Eight;
				*(double*)destValue = (double)num;
				return DBSTATUS.S_OK;
			}
			return base.GetValue(row, dataConvert, binding, destValue, out destLength);
		}
	}
}
