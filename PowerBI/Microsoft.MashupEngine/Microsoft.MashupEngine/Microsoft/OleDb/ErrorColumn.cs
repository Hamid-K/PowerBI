using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001E6C RID: 7788
	internal class ErrorColumn : Bit32Column
	{
		// Token: 0x0600BFD7 RID: 49111 RVA: 0x0026B0E5 File Offset: 0x002692E5
		public ErrorColumn(int maxRowCount)
			: base(DBTYPE.ERROR, maxRowCount)
		{
		}

		// Token: 0x17002F07 RID: 12039
		// (get) Token: 0x0600BFD8 RID: 49112 RVA: 0x000E78B2 File Offset: 0x000E5AB2
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Error;
			}
		}

		// Token: 0x0600BFD9 RID: 49113 RVA: 0x0026B0F0 File Offset: 0x002692F0
		public override void AddValue(object value)
		{
			base.Add32(((ErrorWrapper)value).ErrorCode);
		}

		// Token: 0x0600BFDA RID: 49114 RVA: 0x0026B103 File Offset: 0x00269303
		public override bool TryAddValue(object value)
		{
			if (value is ErrorWrapper)
			{
				base.Add32(((ErrorWrapper)value).ErrorCode);
				return true;
			}
			return false;
		}

		// Token: 0x0600BFDB RID: 49115 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override object GetObject(int row)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600BFDC RID: 49116 RVA: 0x0026B124 File Offset: 0x00269324
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			uint num = base.Get32(row);
			if (binding.DestType == DBTYPE.ERROR)
			{
				destLength = DbLength.Four;
				*(int*)destValue = (int)num;
				return DBSTATUS.S_OK;
			}
			ErrorWrapper errorWrapper = new ErrorWrapper((int)num);
			return dataConvert.DataConvert(errorWrapper, binding, destValue, out destLength);
		}
	}
}
