using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E6A RID: 7786
	internal class SingleColumn : Bit32Column
	{
		// Token: 0x0600BFC5 RID: 49093 RVA: 0x0026AF8D File Offset: 0x0026918D
		public SingleColumn(int maxRowCount)
			: base(DBTYPE.R4, maxRowCount)
		{
		}

		// Token: 0x17002F05 RID: 12037
		// (get) Token: 0x0600BFC6 RID: 49094 RVA: 0x00142610 File Offset: 0x00140810
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Single;
			}
		}

		// Token: 0x0600BFC7 RID: 49095 RVA: 0x0026AF97 File Offset: 0x00269197
		public void AddValue(float value)
		{
			base.Add32(value);
		}

		// Token: 0x0600BFC8 RID: 49096 RVA: 0x0026AFA0 File Offset: 0x002691A0
		public override void AddValue(object value)
		{
			this.AddValue((float)value);
		}

		// Token: 0x0600BFC9 RID: 49097 RVA: 0x0026AFAE File Offset: 0x002691AE
		public override void AddFloat(float value)
		{
			this.AddValue(value);
		}

		// Token: 0x0600BFCA RID: 49098 RVA: 0x0026AFB7 File Offset: 0x002691B7
		public override bool TryAddValue(object value)
		{
			if (value is float)
			{
				this.AddValue((float)value);
				return true;
			}
			return false;
		}

		// Token: 0x0600BFCB RID: 49099 RVA: 0x0026AFD0 File Offset: 0x002691D0
		public unsafe override float GetFloat(int row)
		{
			uint num = base.Get32(row);
			return *(float*)(&num);
		}

		// Token: 0x0600BFCC RID: 49100 RVA: 0x0026AFE9 File Offset: 0x002691E9
		public override object GetObject(int row)
		{
			return this.GetFloat(row);
		}

		// Token: 0x0600BFCD RID: 49101 RVA: 0x0026AFF8 File Offset: 0x002691F8
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			uint num = base.Get32(row);
			float num2 = *(float*)(&num);
			if (binding.DestType == DBTYPE.R4)
			{
				destLength = DbLength.Four;
				*(float*)destValue = num2;
				return DBSTATUS.S_OK;
			}
			return dataConvert.DataConvert(num2, binding, destValue, out destLength);
		}
	}
}
