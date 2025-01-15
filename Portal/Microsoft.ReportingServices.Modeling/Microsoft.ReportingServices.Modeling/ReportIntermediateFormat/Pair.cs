using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000012 RID: 18
	internal struct Pair<T, U>
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00002E12 File Offset: 0x00001012
		internal Pair(T first, U second)
		{
			this.First = first;
			this.Second = second;
		}

		// Token: 0x04000006 RID: 6
		internal T First;

		// Token: 0x04000007 RID: 7
		internal U Second;
	}
}
