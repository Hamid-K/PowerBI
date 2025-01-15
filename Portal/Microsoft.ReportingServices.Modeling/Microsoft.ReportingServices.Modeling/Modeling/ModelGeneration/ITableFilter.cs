using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000E1 RID: 225
	internal interface ITableFilter
	{
		// Token: 0x06000BE0 RID: 3040
		bool IsMatch(DsvTable table);
	}
}
