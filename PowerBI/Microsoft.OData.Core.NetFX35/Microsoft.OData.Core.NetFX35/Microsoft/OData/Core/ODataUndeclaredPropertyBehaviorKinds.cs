using System;

namespace Microsoft.OData.Core
{
	// Token: 0x020001A1 RID: 417
	[Flags]
	public enum ODataUndeclaredPropertyBehaviorKinds
	{
		// Token: 0x040006D7 RID: 1751
		None = 0,
		// Token: 0x040006D8 RID: 1752
		IgnoreUndeclaredValueProperty = 1,
		// Token: 0x040006D9 RID: 1753
		ReportUndeclaredLinkProperty = 2
	}
}
