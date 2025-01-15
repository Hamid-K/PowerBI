using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000E0 RID: 224
	internal sealed class GroupDetail : INamedProjection, INamedItem
	{
		// Token: 0x06000DCD RID: 3533 RVA: 0x00023537 File Offset: 0x00021737
		internal GroupDetail(QueryExpression expression, string name, bool isProjected = true, bool calculateInMeasureContext = false)
		{
			this._expression = ArgumentValidation.CheckNotNull<QueryExpression>(expression, "expression");
			this._name = ArgumentValidation.CheckNotNullOrEmpty(name, "name");
			this._isProjected = isProjected;
			this._calculateInMeasureContext = calculateInMeasureContext;
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x00023570 File Offset: 0x00021770
		public QueryExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00023578 File Offset: 0x00021778
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x00023580 File Offset: 0x00021780
		public bool IsProjected
		{
			get
			{
				return this._isProjected;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x00023588 File Offset: 0x00021788
		public bool CalculateInMeasureContext
		{
			get
			{
				return this._calculateInMeasureContext;
			}
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00023590 File Offset: 0x00021790
		internal GroupDetail OmitProjection()
		{
			return this.ChangeProjection(false);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00023599 File Offset: 0x00021799
		internal GroupDetail ProjectDetail()
		{
			return this.ChangeProjection(true);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x000235A2 File Offset: 0x000217A2
		private GroupDetail ChangeProjection(bool isProjected)
		{
			return new GroupDetail(this._expression, this._name, isProjected, this._calculateInMeasureContext);
		}

		// Token: 0x040009A8 RID: 2472
		private readonly QueryExpression _expression;

		// Token: 0x040009A9 RID: 2473
		private readonly string _name;

		// Token: 0x040009AA RID: 2474
		private readonly bool _isProjected;

		// Token: 0x040009AB RID: 2475
		private readonly bool _calculateInMeasureContext;
	}
}
