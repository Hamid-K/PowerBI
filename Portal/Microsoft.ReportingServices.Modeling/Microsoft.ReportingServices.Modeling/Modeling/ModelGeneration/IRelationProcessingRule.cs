using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000F3 RID: 243
	internal interface IRelationProcessingRule
	{
		// Token: 0x06000C57 RID: 3159
		RuleProcessResult Process(DsvRelation relation, ExistingRelationBindingInfo existingInfo);
	}
}
