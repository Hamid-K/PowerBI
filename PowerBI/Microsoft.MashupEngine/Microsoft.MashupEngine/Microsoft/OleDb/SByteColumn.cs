using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E63 RID: 7779
	internal class SByteColumn : Bit8Column
	{
		// Token: 0x0600BF8A RID: 49034 RVA: 0x0026AA72 File Offset: 0x00268C72
		public SByteColumn(int maxRowCount)
			: base(DBTYPE.I1, maxRowCount)
		{
		}

		// Token: 0x17002EFE RID: 12030
		// (get) Token: 0x0600BF8B RID: 49035 RVA: 0x00002139 File Offset: 0x00000339
		public override ColumnType Type
		{
			get
			{
				return ColumnType.SByte;
			}
		}

		// Token: 0x0600BF8C RID: 49036 RVA: 0x0026AA7D File Offset: 0x00268C7D
		public void AddValue(sbyte value)
		{
			base.Add8(value);
		}

		// Token: 0x0600BF8D RID: 49037 RVA: 0x0026AA86 File Offset: 0x00268C86
		public override void AddValue(object value)
		{
			this.AddValue((sbyte)value);
		}

		// Token: 0x0600BF8E RID: 49038 RVA: 0x0026AA94 File Offset: 0x00268C94
		public override void AddSByte(sbyte value)
		{
			this.AddValue(value);
		}

		// Token: 0x0600BF8F RID: 49039 RVA: 0x0026AA9D File Offset: 0x00268C9D
		public override bool TryAddValue(object value)
		{
			if (value is sbyte)
			{
				this.AddValue((sbyte)value);
				return true;
			}
			return false;
		}

		// Token: 0x0600BF90 RID: 49040 RVA: 0x0026AAB6 File Offset: 0x00268CB6
		public override object GetObject(int row)
		{
			return (sbyte)base.Get8(row);
		}

		// Token: 0x0600BF91 RID: 49041 RVA: 0x0026AAC8 File Offset: 0x00268CC8
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			sbyte b = (sbyte)base.Get8(row);
			if (binding.DestType == DBTYPE.I1)
			{
				destLength = DbLength.One;
				*destValue = (byte)b;
				return DBSTATUS.S_OK;
			}
			if (binding.DestType == DBTYPE.I8)
			{
				destLength = DbLength.Eight;
				*(long*)destValue = (long)b;
				return DBSTATUS.S_OK;
			}
			if (binding.DestType == DBTYPE.R8)
			{
				destLength = DbLength.Eight;
				*(double*)destValue = (double)b;
				return DBSTATUS.S_OK;
			}
			return dataConvert.DataConvert(b, binding, destValue, out destLength);
		}
	}
}
