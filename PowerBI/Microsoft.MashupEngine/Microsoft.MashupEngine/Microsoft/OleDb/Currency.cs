using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E89 RID: 7817
	public struct Currency
	{
		// Token: 0x0600C144 RID: 49476 RVA: 0x0026DD15 File Offset: 0x0026BF15
		public Currency(decimal value)
		{
			this.value = value;
		}

		// Token: 0x17002F37 RID: 12087
		// (get) Token: 0x0600C145 RID: 49477 RVA: 0x0026DD1E File Offset: 0x0026BF1E
		public decimal Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0400618E RID: 24974
		private decimal value;
	}
}
