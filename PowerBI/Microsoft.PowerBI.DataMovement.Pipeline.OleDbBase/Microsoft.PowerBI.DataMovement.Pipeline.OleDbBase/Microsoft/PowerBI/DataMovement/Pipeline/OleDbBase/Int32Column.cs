using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200001C RID: 28
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class Int32Column : Bit32Column
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x00003CE3 File Offset: 0x00001EE3
		internal Int32Column(int maxRowCount)
			: base(DBTYPE.I4, maxRowCount)
		{
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00003CED File Offset: 0x00001EED
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Int32;
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003CF0 File Offset: 0x00001EF0
		public override void AddValue(object value)
		{
			base.Add32((int)value);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003CFE File Offset: 0x00001EFE
		public override bool TryAddValue(object value)
		{
			if (value is int)
			{
				base.Add32((int)value);
				return true;
			}
			return false;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003D17 File Offset: 0x00001F17
		public override int GetInt32(int row)
		{
			return (int)base.Get32(row);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003D20 File Offset: 0x00001F20
		public override object GetObject(int row)
		{
			return this.GetInt32(row);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003D30 File Offset: 0x00001F30
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			int num = (int)base.Get32(row);
			if (binding.DestType == DBTYPE.I4)
			{
				destLength = DbLength.Four;
				*(int*)destValue = num;
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
