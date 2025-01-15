using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000848 RID: 2120
	internal interface IDecumulator<T> : IEnumerator<T>, IDisposable, IEnumerator
	{
		// Token: 0x0600766B RID: 30315
		void RemoveCurrent();
	}
}
