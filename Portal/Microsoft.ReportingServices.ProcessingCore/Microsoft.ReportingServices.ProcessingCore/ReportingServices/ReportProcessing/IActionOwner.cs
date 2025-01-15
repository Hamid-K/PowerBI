using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006C8 RID: 1736
	internal interface IActionOwner
	{
		// Token: 0x170020A2 RID: 8354
		// (get) Token: 0x06005D09 RID: 23817
		Action Action { get; }

		// Token: 0x170020A3 RID: 8355
		// (get) Token: 0x06005D0A RID: 23818
		// (set) Token: 0x06005D0B RID: 23819
		List<string> FieldsUsedInValueExpression { get; set; }
	}
}
