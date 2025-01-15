using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003B9 RID: 953
	internal struct Pair<T, U>
	{
		// Token: 0x060026B3 RID: 9907 RVA: 0x000B973F File Offset: 0x000B793F
		internal Pair(T first, U second)
		{
			this.First = first;
			this.Second = second;
		}

		// Token: 0x04001656 RID: 5718
		internal T First;

		// Token: 0x04001657 RID: 5719
		internal U Second;
	}
}
