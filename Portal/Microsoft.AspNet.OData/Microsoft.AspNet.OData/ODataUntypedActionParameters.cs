using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000044 RID: 68
	[NonValidatingParameterBinding]
	public class ODataUntypedActionParameters : Dictionary<string, object>
	{
		// Token: 0x06000198 RID: 408 RVA: 0x00007AA8 File Offset: 0x00005CA8
		public ODataUntypedActionParameters(IEdmAction action)
		{
			if (action == null)
			{
				throw Error.ArgumentNull("action");
			}
			this.Action = action;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00007AC5 File Offset: 0x00005CC5
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00007ACD File Offset: 0x00005CCD
		public IEdmAction Action { get; private set; }
	}
}
