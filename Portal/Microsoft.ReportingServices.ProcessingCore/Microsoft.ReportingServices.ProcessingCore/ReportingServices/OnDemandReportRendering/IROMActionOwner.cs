using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002C2 RID: 706
	internal interface IROMActionOwner
	{
		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06001ABC RID: 6844
		string UniqueName { get; }

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06001ABD RID: 6845
		List<string> FieldsUsedInValueExpression { get; }
	}
}
