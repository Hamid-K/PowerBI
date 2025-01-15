using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000081 RID: 129
	internal sealed class RdmQueryMeasureExpression : IRdmQueryExpression
	{
		// Token: 0x06000283 RID: 643 RVA: 0x0000C52C File Offset: 0x0000A72C
		internal RdmQueryMeasureExpression(string measure, string target)
		{
			this._measure = measure;
			this._target = target;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000C542 File Offset: 0x0000A742
		internal string Measure
		{
			get
			{
				return this._measure;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000C54A File Offset: 0x0000A74A
		public string Target
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000C552 File Offset: 0x0000A752
		public void FindFormulaComponents(FormulaParserContext context)
		{
			context.PropertyName = this._measure;
			context.EdmReferenceKind = new FormulaEdmReferenceKind?(FormulaEdmReferenceKind.MeasureProperty);
			context.EntityName = this._target;
		}

		// Token: 0x0400019D RID: 413
		private readonly string _measure;

		// Token: 0x0400019E RID: 414
		private readonly string _target;
	}
}
