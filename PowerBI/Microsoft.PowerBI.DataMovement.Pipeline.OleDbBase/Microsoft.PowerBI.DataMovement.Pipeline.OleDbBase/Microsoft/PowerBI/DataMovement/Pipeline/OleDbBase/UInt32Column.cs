using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200001D RID: 29
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class UInt32Column : Bit32Column
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x00003DA6 File Offset: 0x00001FA6
		internal UInt32Column(int maxRowCount)
			: base(DBTYPE.UI4, maxRowCount)
		{
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00003DB1 File Offset: 0x00001FB1
		public override ColumnType Type
		{
			get
			{
				return ColumnType.UInt32;
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003DB4 File Offset: 0x00001FB4
		public override void AddValue(object value)
		{
			base.Add32((uint)value);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003DC2 File Offset: 0x00001FC2
		public override bool TryAddValue(object value)
		{
			if (value is uint)
			{
				base.Add32((uint)value);
				return true;
			}
			return false;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003DDB File Offset: 0x00001FDB
		public override object GetObject(int row)
		{
			return base.Get32(row);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003DEC File Offset: 0x00001FEC
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			uint num = base.Get32(row);
			if (binding.DestType == DBTYPE.UI4)
			{
				destLength = DbLength.Four;
				*(int*)destValue = (int)num;
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
				*(double*)destValue = num;
				return DBSTATUS.S_OK;
			}
			return base.GetValue(row, dataConvert, binding, destValue, out destLength);
		}
	}
}
