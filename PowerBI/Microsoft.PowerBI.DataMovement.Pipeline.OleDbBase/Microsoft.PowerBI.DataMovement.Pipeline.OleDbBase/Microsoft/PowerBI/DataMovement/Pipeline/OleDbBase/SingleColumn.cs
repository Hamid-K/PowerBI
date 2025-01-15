using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000020 RID: 32
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class SingleColumn : Bit32Column
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00003EF3 File Offset: 0x000020F3
		internal SingleColumn(int maxRowCount)
			: base(DBTYPE.R4, maxRowCount)
		{
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00003EFD File Offset: 0x000020FD
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Single;
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003F01 File Offset: 0x00002101
		public override void AddValue(object value)
		{
			base.Add32((float)value);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003F0F File Offset: 0x0000210F
		public override bool TryAddValue(object value)
		{
			if (value is float)
			{
				base.Add32((float)value);
				return true;
			}
			return false;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00003F28 File Offset: 0x00002128
		public unsafe override float GetFloat(int row)
		{
			uint num = base.Get32(row);
			return *(float*)(&num);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003F41 File Offset: 0x00002141
		public override object GetObject(int row)
		{
			return this.GetFloat(row);
		}
	}
}
