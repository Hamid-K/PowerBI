using System;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200040B RID: 1035
	internal sealed class IntBox
	{
		// Token: 0x0600310D RID: 12557 RVA: 0x0009C685 File Offset: 0x0009A885
		internal IntBox(int val)
		{
			this.Value = val;
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x0600310E RID: 12558 RVA: 0x0009C694 File Offset: 0x0009A894
		// (set) Token: 0x0600310F RID: 12559 RVA: 0x0009C69C File Offset: 0x0009A89C
		internal int Value { get; set; }
	}
}
