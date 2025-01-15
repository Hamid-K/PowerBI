using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000019 RID: 25
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class SByteColumn : Bit8Column
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00003AA7 File Offset: 0x00001CA7
		internal SByteColumn(int maxRowCount)
			: base(DBTYPE.I1, maxRowCount)
		{
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00003AB2 File Offset: 0x00001CB2
		public override ColumnType Type
		{
			get
			{
				return ColumnType.SByte;
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003AB5 File Offset: 0x00001CB5
		public override void AddValue(object value)
		{
			base.Add8((sbyte)value);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003AC3 File Offset: 0x00001CC3
		public override bool TryAddValue(object value)
		{
			if (value is sbyte)
			{
				base.Add8((sbyte)value);
				return true;
			}
			return false;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003ADC File Offset: 0x00001CDC
		public override object GetObject(int row)
		{
			return (sbyte)base.Get8(row);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003AEC File Offset: 0x00001CEC
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
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
			return base.GetValue(row, dataConvert, binding, destValue, out destLength);
		}
	}
}
