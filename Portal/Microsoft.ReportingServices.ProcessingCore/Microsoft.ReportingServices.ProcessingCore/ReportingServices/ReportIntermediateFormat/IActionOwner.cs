using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004D8 RID: 1240
	internal interface IActionOwner
	{
		// Token: 0x17001AA0 RID: 6816
		// (get) Token: 0x06003EA5 RID: 16037
		Action Action { get; }

		// Token: 0x17001AA1 RID: 6817
		// (get) Token: 0x06003EA6 RID: 16038
		// (set) Token: 0x06003EA7 RID: 16039
		List<string> FieldsUsedInValueExpression { get; set; }
	}
}
