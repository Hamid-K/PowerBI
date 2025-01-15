using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001EE RID: 494
	public sealed class EntitySetAggregateExpression : AggregateExpressionBase
	{
		// Token: 0x06001644 RID: 5700 RVA: 0x0003E37C File Offset: 0x0003C57C
		public EntitySetAggregateExpression(CollectionNavigationNode expression, IEnumerable<AggregateExpressionBase> children)
			: base(AggregateExpressionKind.EntitySetAggregate, expression.NavigationProperty.Name)
		{
			this.expression = expression;
			this.children = children;
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x0003E39E File Offset: 0x0003C59E
		public CollectionNavigationNode Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x0003E3A6 File Offset: 0x0003C5A6
		public IEnumerable<AggregateExpressionBase> Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x04000A09 RID: 2569
		private readonly CollectionNavigationNode expression;

		// Token: 0x04000A0A RID: 2570
		private readonly IEnumerable<AggregateExpressionBase> children;
	}
}
