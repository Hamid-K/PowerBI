using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001B2 RID: 434
	internal interface IVerifier
	{
		// Token: 0x06000E14 RID: 3604
		List<DiagRuleViolation> Validate(DiagOperationState opState);
	}
}
