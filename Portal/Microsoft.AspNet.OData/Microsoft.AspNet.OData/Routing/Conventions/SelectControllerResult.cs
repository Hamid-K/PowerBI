using System;
using System.Collections.Generic;

namespace Microsoft.AspNet.OData.Routing.Conventions
{
	// Token: 0x0200009E RID: 158
	public class SelectControllerResult
	{
		// Token: 0x06000555 RID: 1365 RVA: 0x000122BF File Offset: 0x000104BF
		public SelectControllerResult(string controllerName, IDictionary<string, object> values)
		{
			this.ControllerName = controllerName;
			this.Values = values;
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x000122D5 File Offset: 0x000104D5
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x000122DD File Offset: 0x000104DD
		public string ControllerName { get; private set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x000122E6 File Offset: 0x000104E6
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x000122EE File Offset: 0x000104EE
		public IDictionary<string, object> Values { get; private set; }
	}
}
