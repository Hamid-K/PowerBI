using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Library;

namespace Microsoft.ReportingServices.Portal.Services.SystemResources
{
	// Token: 0x0200002F RID: 47
	internal interface ISystemResourceProcessor
	{
		// Token: 0x06000216 RID: 534
		bool IsProcessed(SystemResource resource);

		// Token: 0x06000217 RID: 535
		void Process(SystemResource resource, IEnumerable<ISystemResourceManager> resourceManagers);

		// Token: 0x06000218 RID: 536
		bool TryLoadItem(SystemResource resource, string itemName, out byte[] bytes, out string contentType, out string filename);
	}
}
