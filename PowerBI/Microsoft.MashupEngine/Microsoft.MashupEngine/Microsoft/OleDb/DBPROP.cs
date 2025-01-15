using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EE2 RID: 7906
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBPROP
	{
		// Token: 0x04006407 RID: 25607
		public DBPROPID dwPropertyID;

		// Token: 0x04006408 RID: 25608
		public DBPROPOPTIONS dwOptions;

		// Token: 0x04006409 RID: 25609
		public DBPROPSTATUS dwStatus;

		// Token: 0x0400640A RID: 25610
		public DBID colid;

		// Token: 0x0400640B RID: 25611
		public VARIANT variant;
	}
}
