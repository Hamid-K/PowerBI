using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E79 RID: 7801
	internal class UnsupportedTypeColumn : NullColumn
	{
		// Token: 0x17002F18 RID: 12056
		// (get) Token: 0x0600C04F RID: 49231 RVA: 0x0012AF09 File Offset: 0x00129109
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Unsupported;
			}
		}

		// Token: 0x0600C050 RID: 49232 RVA: 0x0026BB2D File Offset: 0x00269D2D
		public UnsupportedTypeColumn()
			: base(0)
		{
		}
	}
}
