using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E61 RID: 7777
	internal class BooleanColumn : Bit16Column
	{
		// Token: 0x0600BF79 RID: 49017 RVA: 0x0026A8D6 File Offset: 0x00268AD6
		public BooleanColumn(int maxRowCount)
			: base(DBTYPE.BOOL, maxRowCount)
		{
		}

		// Token: 0x17002EFC RID: 12028
		// (get) Token: 0x0600BF7A RID: 49018 RVA: 0x00002105 File Offset: 0x00000305
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Boolean;
			}
		}

		// Token: 0x0600BF7B RID: 49019 RVA: 0x0026A8E1 File Offset: 0x00268AE1
		public override void AddValue(object value)
		{
			this.AddBoolean((bool)value);
		}

		// Token: 0x0600BF7C RID: 49020 RVA: 0x0026A8F0 File Offset: 0x00268AF0
		public override void AddBoolean(bool value)
		{
			VARIANT_BOOL variant_BOOL = (value ? VARIANT_BOOL.TRUE : VARIANT_BOOL.FALSE);
			base.Add16((ushort)variant_BOOL);
		}

		// Token: 0x0600BF7D RID: 49021 RVA: 0x0026A910 File Offset: 0x00268B10
		public override bool TryAddValue(object value)
		{
			if (value is bool)
			{
				VARIANT_BOOL variant_BOOL = (((bool)value) ? VARIANT_BOOL.TRUE : VARIANT_BOOL.FALSE);
				base.Add16((ushort)variant_BOOL);
				return true;
			}
			return false;
		}

		// Token: 0x0600BF7E RID: 49022 RVA: 0x0026A940 File Offset: 0x00268B40
		public override bool GetBoolean(int row)
		{
			return base.Get16(row) > 0;
		}

		// Token: 0x0600BF7F RID: 49023 RVA: 0x0026A94C File Offset: 0x00268B4C
		public override object GetObject(int row)
		{
			return this.GetBoolean(row);
		}

		// Token: 0x0600BF80 RID: 49024 RVA: 0x0026A95C File Offset: 0x00268B5C
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			ushort num = base.Get16(row);
			if (binding.DestType == DBTYPE.BOOL)
			{
				destLength = DbLength.Two;
				*(short*)destValue = (short)num;
				return DBSTATUS.S_OK;
			}
			bool flag = num > 0;
			return dataConvert.DataConvert(flag, binding, destValue, out destLength);
		}
	}
}
