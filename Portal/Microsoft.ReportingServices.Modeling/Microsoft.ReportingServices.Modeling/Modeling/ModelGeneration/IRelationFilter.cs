using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000E3 RID: 227
	internal interface IRelationFilter
	{
		// Token: 0x06000BE2 RID: 3042
		bool IsMatch(DsvRelation relation);
	}
}
