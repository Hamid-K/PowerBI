using System;
using System.Collections.Generic;

namespace Microsoft.OData.Client
{
	// Token: 0x02000053 RID: 83
	public class InvokeResponse : OperationResponse
	{
		// Token: 0x06000298 RID: 664 RVA: 0x0000A106 File Offset: 0x00008306
		public InvokeResponse(Dictionary<string, string> headers)
			: base(new HeaderCollection(headers))
		{
		}
	}
}
