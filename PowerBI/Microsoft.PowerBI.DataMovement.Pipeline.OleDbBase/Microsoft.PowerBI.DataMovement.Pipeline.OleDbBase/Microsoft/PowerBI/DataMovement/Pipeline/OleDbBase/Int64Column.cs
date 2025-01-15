using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200001E RID: 30
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class Int64Column : Bit64Column
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00003E64 File Offset: 0x00002064
		internal Int64Column(int maxRowCount)
			: base(DBTYPE.I8, maxRowCount)
		{
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00003E6F File Offset: 0x0000206F
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Int64;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003E72 File Offset: 0x00002072
		public override void AddValue(object value)
		{
			base.Add64((long)value);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003E80 File Offset: 0x00002080
		public override bool TryAddValue(object value)
		{
			if (value is long)
			{
				base.Add64((long)value);
				return true;
			}
			return false;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003E99 File Offset: 0x00002099
		public override long GetInt64(int row)
		{
			return (long)base.Get64(row);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003EA2 File Offset: 0x000020A2
		public override object GetObject(int row)
		{
			return this.GetInt64(row);
		}
	}
}
