using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000017 RID: 23
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class BooleanColumn : Bit16Column
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00003961 File Offset: 0x00001B61
		internal BooleanColumn(int maxRowCount)
			: base(DBTYPE.BOOL, maxRowCount)
		{
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000396C File Offset: 0x00001B6C
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Boolean;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003970 File Offset: 0x00001B70
		public override void AddValue(object value)
		{
			VARIANT_BOOL variant_BOOL = (((bool)value) ? VARIANT_BOOL.TRUE : VARIANT_BOOL.FALSE);
			base.Add16((ushort)variant_BOOL);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003998 File Offset: 0x00001B98
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

		// Token: 0x060000C5 RID: 197 RVA: 0x000039C8 File Offset: 0x00001BC8
		public override bool GetBoolean(int row)
		{
			return base.Get16(row) != 0;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000039D6 File Offset: 0x00001BD6
		public override object GetObject(int row)
		{
			return this.GetBoolean(row);
		}
	}
}
