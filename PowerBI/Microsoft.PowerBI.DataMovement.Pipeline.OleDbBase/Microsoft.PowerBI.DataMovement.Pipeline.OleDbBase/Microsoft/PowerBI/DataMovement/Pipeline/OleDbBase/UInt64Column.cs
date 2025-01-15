using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200001F RID: 31
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class UInt64Column : Bit64Column
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00003EB0 File Offset: 0x000020B0
		internal UInt64Column(int maxRowCount)
			: base(DBTYPE.UI8, maxRowCount)
		{
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00003EBB File Offset: 0x000020BB
		public override ColumnType Type
		{
			get
			{
				return ColumnType.UInt64;
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003EBE File Offset: 0x000020BE
		public override void AddValue(object value)
		{
			base.Add64((ulong)value);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003ECC File Offset: 0x000020CC
		public override bool TryAddValue(object value)
		{
			if (value is ulong)
			{
				base.Add64((ulong)value);
				return true;
			}
			return false;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003EE5 File Offset: 0x000020E5
		public override object GetObject(int row)
		{
			return base.Get64(row);
		}
	}
}
