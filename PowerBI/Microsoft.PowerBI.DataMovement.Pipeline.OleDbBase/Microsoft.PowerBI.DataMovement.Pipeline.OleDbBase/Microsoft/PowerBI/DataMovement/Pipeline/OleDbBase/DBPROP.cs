using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200009A RID: 154
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBPROP
	{
		// Token: 0x040002D2 RID: 722
		public DBPROPID PropertyID;

		// Token: 0x040002D3 RID: 723
		public DBPROPOPTIONS Options;

		// Token: 0x040002D4 RID: 724
		public DBPROPSTATUS Status;

		// Token: 0x040002D5 RID: 725
		public DBID ColId;

		// Token: 0x040002D6 RID: 726
		public VARIANT Variant;
	}
}
