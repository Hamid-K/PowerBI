using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000F2 RID: 242
	internal interface IColumnProcessingRule
	{
		// Token: 0x06000C56 RID: 3158
		RuleProcessResult Process(DsvColumn column, ExistingColumnBindingInfo existingInfo);
	}
}
