using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000EE RID: 238
	internal class DataIntersectionBuilder<TParent> : BuilderBase<DataIntersection, TParent>, IWithDataShape<DataIntersectionBuilder<TParent>>, ICalculationContainer<DataIntersectionBuilder<TParent>>
	{
		// Token: 0x06000698 RID: 1688 RVA: 0x0000E47B File Offset: 0x0000C67B
		internal DataIntersectionBuilder(TParent parent, DataIntersection activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0000E488 File Offset: 0x0000C688
		public DataIntersectionBuilder<TParent> WithCalculation(string identifier, Expression value, bool suppressJoinPredicate = false, bool? respectInstanceFilters = null, string nativeReferenceName = null, bool isContextOnly = false)
		{
			base.ActiveObject.Calculations = base.AddCalculation(base.ActiveObject.Calculations, identifier, value, suppressJoinPredicate, respectInstanceFilters, nativeReferenceName, isContextOnly);
			return this;
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0000E4BC File Offset: 0x0000C6BC
		public DataIntersectionBuilder<TParent> WithVisualCalculation(string identifier, string nativeReferenceName, string vcExpressionNode)
		{
			return this.WithCalculation(identifier, vcExpressionNode.VisualCalculation(), false, null, nativeReferenceName, false);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0000E4EC File Offset: 0x0000C6EC
		public DataShapeBuilder<DataIntersectionBuilder<TParent>> WithDataShape(string identifier, string dataSourceId = null, bool filterEmptyGroups = true, Candidate<bool> contextOnly = null, bool independent = false, DataShapeUsage usage = DataShapeUsage.Query)
		{
			return base.AddDataShape<DataIntersectionBuilder<TParent>>(this, this.GetDataShapesList(), identifier, dataSourceId, filterEmptyGroups, contextOnly, independent, usage);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0000E50F File Offset: 0x0000C70F
		public DataShapeBuilder<DataIntersectionBuilder<TParent>> WithDataShape(DataShape dataShape)
		{
			return base.AddDataShape<DataIntersectionBuilder<TParent>>(this, this.GetDataShapesList(), dataShape);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0000E520 File Offset: 0x0000C720
		protected List<DataShape> GetDataShapesList()
		{
			return base.ActiveObject.DataShapes = base.ActiveObject.DataShapes ?? new List<DataShape>();
		}
	}
}
