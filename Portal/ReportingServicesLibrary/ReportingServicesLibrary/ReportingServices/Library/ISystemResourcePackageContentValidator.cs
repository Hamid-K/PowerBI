using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000251 RID: 593
	internal interface ISystemResourcePackageContentValidator
	{
		// Token: 0x060015BC RID: 5564
		bool Validate(IEnumerable<SystemResourceContentItem> contentItems);
	}
}
