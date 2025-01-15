using System;

namespace Microsoft.Data.OData
{
	// Token: 0x020001D1 RID: 465
	[Flags]
	public enum ODataUndeclaredPropertyBehaviorKinds
	{
		// Token: 0x040004BF RID: 1215
		None = 0,
		// Token: 0x040004C0 RID: 1216
		IgnoreUndeclaredValueProperty = 1,
		// Token: 0x040004C1 RID: 1217
		ReportUndeclaredLinkProperty = 2,
		// Token: 0x040004C2 RID: 1218
		SupportUndeclaredValueProperty = 4
	}
}
