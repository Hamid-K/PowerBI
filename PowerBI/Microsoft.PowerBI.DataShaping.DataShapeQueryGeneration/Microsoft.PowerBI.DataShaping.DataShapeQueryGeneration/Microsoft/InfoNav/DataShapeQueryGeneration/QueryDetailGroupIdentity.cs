using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200009F RID: 159
	internal sealed class QueryDetailGroupIdentity
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x00016C02 File Offset: 0x00014E02
		internal QueryDetailGroupIdentity(IConceptualEntity entity, ExpressionNode expression, int groupByIndex)
		{
			this._entity = entity;
			this._expression = expression;
			this._groupByIndex = groupByIndex;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00016C1F File Offset: 0x00014E1F
		internal IConceptualEntity Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00016C27 File Offset: 0x00014E27
		internal ExpressionNode Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00016C2F File Offset: 0x00014E2F
		internal int GroupByIndex
		{
			get
			{
				return this._groupByIndex;
			}
		}

		// Token: 0x04000332 RID: 818
		private readonly IConceptualEntity _entity;

		// Token: 0x04000333 RID: 819
		private readonly ExpressionNode _expression;

		// Token: 0x04000334 RID: 820
		private readonly int _groupByIndex;
	}
}
