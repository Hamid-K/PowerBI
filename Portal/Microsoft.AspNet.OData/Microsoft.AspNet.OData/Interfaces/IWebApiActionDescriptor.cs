using System;
using System.Collections.Generic;

namespace Microsoft.AspNet.OData.Interfaces
{
	// Token: 0x02000059 RID: 89
	internal interface IWebApiActionDescriptor
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600027A RID: 634
		string ControllerName { get; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600027B RID: 635
		string ActionName { get; }

		// Token: 0x0600027C RID: 636
		IEnumerable<T> GetCustomAttributes<T>(bool inherit) where T : Attribute;

		// Token: 0x0600027D RID: 637
		bool IsHttpMethodSupported(ODataRequestMethod method);
	}
}
