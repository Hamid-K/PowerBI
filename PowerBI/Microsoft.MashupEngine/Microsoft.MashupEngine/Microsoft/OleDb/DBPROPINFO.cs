using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EE6 RID: 7910
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBPROPINFO
	{
		// Token: 0x04006417 RID: 25623
		public unsafe char* description;

		// Token: 0x04006418 RID: 25624
		public DBPROPID dwPropertyID;

		// Token: 0x04006419 RID: 25625
		public DBPROPFLAGS dwFlags;

		// Token: 0x0400641A RID: 25626
		public VARTYPE vtType;

		// Token: 0x0400641B RID: 25627
		public VARIANT vValues;
	}
}
