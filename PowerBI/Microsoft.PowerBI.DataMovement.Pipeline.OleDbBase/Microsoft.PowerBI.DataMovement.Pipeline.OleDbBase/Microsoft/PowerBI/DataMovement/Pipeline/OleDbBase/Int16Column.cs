using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200001A RID: 26
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class Int16Column : Bit16Column
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00003B64 File Offset: 0x00001D64
		internal Int16Column(int maxRowCount)
			: base(DBTYPE.I2, maxRowCount)
		{
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003B6E File Offset: 0x00001D6E
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Int16;
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003B71 File Offset: 0x00001D71
		public override void AddValue(object value)
		{
			base.Add16((short)value);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003B7F File Offset: 0x00001D7F
		public override bool TryAddValue(object value)
		{
			if (value is short)
			{
				base.Add16((short)value);
				return true;
			}
			return false;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003B98 File Offset: 0x00001D98
		public override short GetInt16(int row)
		{
			return (short)base.Get16(row);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003BA2 File Offset: 0x00001DA2
		public override object GetObject(int row)
		{
			return this.GetInt16(row);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003BB0 File Offset: 0x00001DB0
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			short num = (short)base.Get16(row);
			if (binding.DestType == DBTYPE.I2)
			{
				destLength = DbLength.Two;
				*(short*)destValue = num;
				return DBSTATUS.S_OK;
			}
			if (binding.DestType == DBTYPE.I8)
			{
				destLength = DbLength.Eight;
				*(long*)destValue = (long)num;
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
