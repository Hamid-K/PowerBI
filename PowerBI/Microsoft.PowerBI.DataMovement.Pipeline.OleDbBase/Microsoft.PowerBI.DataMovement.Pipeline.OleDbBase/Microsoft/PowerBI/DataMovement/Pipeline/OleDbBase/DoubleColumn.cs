using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000021 RID: 33
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class DoubleColumn : Bit64Column
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00003F4F File Offset: 0x0000214F
		internal DoubleColumn(int maxRowCount)
			: base(DBTYPE.R8, maxRowCount)
		{
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00003F59 File Offset: 0x00002159
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Double;
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00003F5D File Offset: 0x0000215D
		public override void AddValue(object value)
		{
			base.Add64((double)value);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00003F6B File Offset: 0x0000216B
		public override bool TryAddValue(object value)
		{
			if (value is double)
			{
				base.Add64((double)value);
				return true;
			}
			return false;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003F84 File Offset: 0x00002184
		public unsafe override double GetDouble(int row)
		{
			ulong num = base.Get64(row);
			return *(double*)(&num);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00003F9D File Offset: 0x0000219D
		public override object GetObject(int row)
		{
			return this.GetDouble(row);
		}
	}
}
