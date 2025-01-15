using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Portal.Interfaces.Models
{
	// Token: 0x020000A0 RID: 160
	public interface IDataSetSchema
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600052E RID: 1326
		// (set) Token: 0x0600052F RID: 1327
		string Name { get; set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000530 RID: 1328
		// (set) Token: 0x06000531 RID: 1329
		IEnumerable<IDataSetField> Fields { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000532 RID: 1330
		// (set) Token: 0x06000533 RID: 1331
		IEnumerable<IDataSetParameter> Parameters { get; set; }
	}
}
