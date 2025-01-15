using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000069 RID: 105
	[CLSCompliant(false)]
	public enum DBASYNCHPHASE : uint
	{
		// Token: 0x0400029E RID: 670
		INITIALIZATION,
		// Token: 0x0400029F RID: 671
		POPULATION,
		// Token: 0x040002A0 RID: 672
		COMPLETE,
		// Token: 0x040002A1 RID: 673
		CANCELED
	}
}
