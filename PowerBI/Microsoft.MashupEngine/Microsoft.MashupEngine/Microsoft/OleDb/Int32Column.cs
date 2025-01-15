using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E66 RID: 7782
	internal class Int32Column : Bit32Column
	{
		// Token: 0x0600BFA3 RID: 49059 RVA: 0x0026ACDA File Offset: 0x00268EDA
		public Int32Column(int maxRowCount)
			: base(DBTYPE.I4, maxRowCount)
		{
		}

		// Token: 0x17002F01 RID: 12033
		// (get) Token: 0x0600BFA4 RID: 49060 RVA: 0x0000240C File Offset: 0x0000060C
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Int32;
			}
		}

		// Token: 0x0600BFA5 RID: 49061 RVA: 0x0026ACE4 File Offset: 0x00268EE4
		public void AddValue(int value)
		{
			base.Add32(value);
		}

		// Token: 0x0600BFA6 RID: 49062 RVA: 0x0026ACED File Offset: 0x00268EED
		public override void AddValue(object value)
		{
			this.AddValue((int)value);
		}

		// Token: 0x0600BFA7 RID: 49063 RVA: 0x0026ACFB File Offset: 0x00268EFB
		public override void AddInt32(int value)
		{
			this.AddValue(value);
		}

		// Token: 0x0600BFA8 RID: 49064 RVA: 0x0026AD04 File Offset: 0x00268F04
		public override bool TryAddValue(object value)
		{
			if (value is int)
			{
				this.AddValue((int)value);
				return true;
			}
			return false;
		}

		// Token: 0x0600BFA9 RID: 49065 RVA: 0x0026AD1D File Offset: 0x00268F1D
		public override int GetInt32(int row)
		{
			return (int)base.Get32(row);
		}

		// Token: 0x0600BFAA RID: 49066 RVA: 0x0026AD26 File Offset: 0x00268F26
		public override object GetObject(int row)
		{
			return this.GetInt32(row);
		}

		// Token: 0x0600BFAB RID: 49067 RVA: 0x0026AD34 File Offset: 0x00268F34
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
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
			return dataConvert.DataConvert(num, binding, destValue, out destLength);
		}
	}
}
