using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000F1 RID: 241
	internal interface ITableProcessingRule
	{
		// Token: 0x06000C55 RID: 3157
		RuleProcessResult Process(DsvTable table, ExistingTableBindingInfo existingInfo);
	}
}
