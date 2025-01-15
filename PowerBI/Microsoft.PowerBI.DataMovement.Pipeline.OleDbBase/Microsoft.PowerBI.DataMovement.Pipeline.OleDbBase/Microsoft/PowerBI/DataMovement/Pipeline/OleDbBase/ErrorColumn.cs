using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000022 RID: 34
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class ErrorColumn : Bit32Column
	{
		// Token: 0x06000105 RID: 261 RVA: 0x00003FAB File Offset: 0x000021AB
		internal ErrorColumn(int maxRowCount)
			: base(DBTYPE.ERROR, maxRowCount)
		{
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00003FB6 File Offset: 0x000021B6
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Error;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003FBA File Offset: 0x000021BA
		public override void AddValue(object value)
		{
			base.Add32(((ErrorWrapper)value).ErrorCode);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00003FCD File Offset: 0x000021CD
		public override bool TryAddValue(object value)
		{
			if (value is ErrorWrapper)
			{
				base.Add32(((ErrorWrapper)value).ErrorCode);
				return true;
			}
			return false;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00003FEB File Offset: 0x000021EB
		public override object GetObject(int row)
		{
			throw new InvalidOperationException();
		}
	}
}
