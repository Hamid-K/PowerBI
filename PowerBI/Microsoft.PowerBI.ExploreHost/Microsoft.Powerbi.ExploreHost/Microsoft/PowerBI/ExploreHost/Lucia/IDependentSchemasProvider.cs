using System;
using System.Collections.Generic;
using Microsoft.PowerBI.Lucia.Interpret;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000065 RID: 101
	public interface IDependentSchemasProvider
	{
		// Token: 0x060002CB RID: 715
		void UpdateReportMetadata(ReportMetadata reportMetadata);

		// Token: 0x060002CC RID: 716
		List<DependentSchema> GetDependentSchemas();
	}
}
