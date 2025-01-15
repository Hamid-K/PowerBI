using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x0200025E RID: 606
	public interface IODataResponseMessage
	{
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060012E4 RID: 4836
		IEnumerable<KeyValuePair<string, string>> Headers { get; }

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060012E5 RID: 4837
		// (set) Token: 0x060012E6 RID: 4838
		int StatusCode { get; set; }

		// Token: 0x060012E7 RID: 4839
		string GetHeader(string headerName);

		// Token: 0x060012E8 RID: 4840
		void SetHeader(string headerName, string headerValue);

		// Token: 0x060012E9 RID: 4841
		Stream GetStream();
	}
}
