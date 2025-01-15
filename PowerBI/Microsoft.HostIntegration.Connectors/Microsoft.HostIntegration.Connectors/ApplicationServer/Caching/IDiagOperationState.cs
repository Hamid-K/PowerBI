using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001B1 RID: 433
	internal interface IDiagOperationState
	{
		// Token: 0x06000E11 RID: 3601
		void AddEvent(List<DiagEvent> events);

		// Token: 0x06000E12 RID: 3602
		void AddEvent(DiagEvent ev);

		// Token: 0x06000E13 RID: 3603
		void Merge(DiagOperationState states);
	}
}
