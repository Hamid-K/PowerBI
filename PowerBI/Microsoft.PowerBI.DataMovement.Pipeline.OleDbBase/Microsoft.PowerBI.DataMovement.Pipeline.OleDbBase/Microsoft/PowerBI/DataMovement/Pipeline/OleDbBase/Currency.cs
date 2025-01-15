using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000037 RID: 55
	public struct Currency
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x00006124 File Offset: 0x00004324
		public Currency(decimal value)
		{
			this.value = value;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000612D File Offset: 0x0000432D
		public decimal Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0400007C RID: 124
		private readonly decimal value;
	}
}
