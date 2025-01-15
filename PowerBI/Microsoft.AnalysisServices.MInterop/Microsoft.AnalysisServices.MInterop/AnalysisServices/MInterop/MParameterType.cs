using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x0200002A RID: 42
	[ComVisible(true)]
	[Guid("9922D05C-85D7-429C-8372-6C0C7C81994A")]
	public enum MParameterType
	{
		// Token: 0x040000FF RID: 255
		UNKNOWN,
		// Token: 0x04000100 RID: 256
		TEXT,
		// Token: 0x04000101 RID: 257
		NUMBER,
		// Token: 0x04000102 RID: 258
		DATE,
		// Token: 0x04000103 RID: 259
		DATETIME,
		// Token: 0x04000104 RID: 260
		DATETIMEZONE,
		// Token: 0x04000105 RID: 261
		LOGICAL
	}
}
