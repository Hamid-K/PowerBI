using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x0200025D RID: 605
	public interface IODataRequestMessage
	{
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060012DC RID: 4828
		IEnumerable<KeyValuePair<string, string>> Headers { get; }

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060012DD RID: 4829
		// (set) Token: 0x060012DE RID: 4830
		Uri Url { get; set; }

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060012DF RID: 4831
		// (set) Token: 0x060012E0 RID: 4832
		string Method { get; set; }

		// Token: 0x060012E1 RID: 4833
		string GetHeader(string headerName);

		// Token: 0x060012E2 RID: 4834
		void SetHeader(string headerName, string headerValue);

		// Token: 0x060012E3 RID: 4835
		Stream GetStream();
	}
}
