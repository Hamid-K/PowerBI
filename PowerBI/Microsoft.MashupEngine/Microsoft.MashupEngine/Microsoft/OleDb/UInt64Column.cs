using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E69 RID: 7785
	internal class UInt64Column : Bit64Column
	{
		// Token: 0x0600BFBD RID: 49085 RVA: 0x0026AF05 File Offset: 0x00269105
		public UInt64Column(int maxRowCount)
			: base(DBTYPE.UI8, maxRowCount)
		{
		}

		// Token: 0x17002F04 RID: 12036
		// (get) Token: 0x0600BFBE RID: 49086 RVA: 0x000024ED File Offset: 0x000006ED
		public override ColumnType Type
		{
			get
			{
				return ColumnType.UInt64;
			}
		}

		// Token: 0x0600BFBF RID: 49087 RVA: 0x0026A749 File Offset: 0x00268949
		public void AddValue(ulong value)
		{
			base.Add64(value);
		}

		// Token: 0x0600BFC0 RID: 49088 RVA: 0x0026AF10 File Offset: 0x00269110
		public override void AddValue(object value)
		{
			this.AddValue((ulong)value);
		}

		// Token: 0x0600BFC1 RID: 49089 RVA: 0x0026AF1E File Offset: 0x0026911E
		public override void AddUInt64(ulong value)
		{
			this.AddValue(value);
		}

		// Token: 0x0600BFC2 RID: 49090 RVA: 0x0026AF27 File Offset: 0x00269127
		public override bool TryAddValue(object value)
		{
			if (value is ulong)
			{
				this.AddValue((ulong)value);
				return true;
			}
			return false;
		}

		// Token: 0x0600BFC3 RID: 49091 RVA: 0x0026AF40 File Offset: 0x00269140
		public override object GetObject(int row)
		{
			return base.Get64(row);
		}

		// Token: 0x0600BFC4 RID: 49092 RVA: 0x0026AF50 File Offset: 0x00269150
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			ulong num = base.Get64(row);
			if (binding.DestType == DBTYPE.UI8)
			{
				destLength = DbLength.Eight;
				*(long*)destValue = (long)num;
				return DBSTATUS.S_OK;
			}
			return dataConvert.DataConvert(num, binding, destValue, out destLength);
		}
	}
}
