using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000F8 RID: 248
	internal sealed class ContextFilterConditionBuilder<TParent> : BuilderBase<ContextFilterCondition, TParent>, IWithDataShape<ContextFilterConditionBuilder<TParent>>
	{
		// Token: 0x060006C9 RID: 1737 RVA: 0x0000E9F3 File Offset: 0x0000CBF3
		internal ContextFilterConditionBuilder(TParent parent, ContextFilterCondition activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0000E9FD File Offset: 0x0000CBFD
		public DataShapeBuilder<ContextFilterConditionBuilder<TParent>> WithDataShape(string identifier, string dataSourceId = null, bool filterEmptyGroups = true, Candidate<bool> contextOnly = null, bool independent = false, DataShapeUsage usage = DataShapeUsage.Query)
		{
			base.ActiveObject.DataShape = BuilderBase<ContextFilterCondition>.CreateDataShape(identifier, dataSourceId, filterEmptyGroups, contextOnly, independent, usage);
			return new DataShapeBuilder<ContextFilterConditionBuilder<TParent>>(this, base.ActiveObject.DataShape);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0000EA29 File Offset: 0x0000CC29
		public DataShapeBuilder<ContextFilterConditionBuilder<TParent>> WithDataShape(DataShape dataShape)
		{
			base.ActiveObject.DataShape = dataShape;
			return new DataShapeBuilder<ContextFilterConditionBuilder<TParent>>(this, base.ActiveObject.DataShape);
		}
	}
}
