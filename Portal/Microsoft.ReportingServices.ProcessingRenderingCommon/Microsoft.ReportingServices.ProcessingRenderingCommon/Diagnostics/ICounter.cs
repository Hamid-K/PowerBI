using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000098 RID: 152
	internal interface ICounter : IDisposable
	{
		// Token: 0x060004C0 RID: 1216
		void Increment();

		// Token: 0x060004C1 RID: 1217
		void IncrementBy(long val);

		// Token: 0x060004C2 RID: 1218
		void Decrement();

		// Token: 0x060004C3 RID: 1219
		void DecrementBy(long val);
	}
}
