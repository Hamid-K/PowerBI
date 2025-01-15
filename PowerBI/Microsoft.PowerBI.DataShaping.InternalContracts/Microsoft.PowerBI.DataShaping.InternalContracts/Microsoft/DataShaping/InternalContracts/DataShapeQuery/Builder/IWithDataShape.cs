using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x02000106 RID: 262
	internal interface IWithDataShape<TParent>
	{
		// Token: 0x0600070E RID: 1806
		DataShapeBuilder<TParent> WithDataShape(string identifier, string dataSourceId = null, bool filterEmptyGroups = true, Candidate<bool> contextOnly = null, bool independent = false, DataShapeUsage usage = DataShapeUsage.Query);

		// Token: 0x0600070F RID: 1807
		DataShapeBuilder<TParent> WithDataShape(DataShape dataShape);
	}
}
