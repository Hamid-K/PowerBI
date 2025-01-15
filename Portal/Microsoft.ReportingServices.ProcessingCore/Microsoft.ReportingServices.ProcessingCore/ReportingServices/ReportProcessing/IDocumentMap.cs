using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000644 RID: 1604
	public interface IDocumentMap : IEnumerator<OnDemandDocumentMapNode>, IDisposable, IEnumerator
	{
		// Token: 0x06005780 RID: 22400
		void Close();
	}
}
