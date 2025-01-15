using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000663 RID: 1635
	internal class PairObj<T, U>
	{
		// Token: 0x06005AB4 RID: 23220 RVA: 0x0017554E File Offset: 0x0017374E
		internal PairObj(T first, U second)
		{
			this.First = first;
			this.Second = second;
		}

		// Token: 0x04002F29 RID: 12073
		internal T First;

		// Token: 0x04002F2A RID: 12074
		internal U Second;
	}
}
