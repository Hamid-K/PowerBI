using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200018D RID: 397
	internal sealed class QueryGroupByExpression : QueryExpression
	{
		// Token: 0x06001549 RID: 5449 RVA: 0x0003BD35 File Offset: 0x00039F35
		internal QueryGroupByExpression(ConceptualResultType conceptualResultType, QueryGroupExpressionBinding input, IEnumerable<IGroupItem> groupItems, IEnumerable<KeyValuePair<string, QueryExpression>> aggregates)
			: base(conceptualResultType)
		{
			this._input = ArgumentValidation.CheckNotNull<QueryGroupExpressionBinding>(input, "input");
			this._groupItems = groupItems.ToReadOnlyCollection<IGroupItem>();
			this._aggregates = aggregates.ToReadOnlyCollection<KeyValuePair<string, QueryExpression>>();
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x0003BD68 File Offset: 0x00039F68
		public QueryGroupExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x0003BD70 File Offset: 0x00039F70
		public ReadOnlyCollection<IGroupItem> GroupItems
		{
			get
			{
				return this._groupItems;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x0003BD78 File Offset: 0x00039F78
		public ReadOnlyCollection<KeyValuePair<string, QueryExpression>> Aggregates
		{
			get
			{
				return this._aggregates;
			}
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0003BD80 File Offset: 0x00039F80
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0003BD94 File Offset: 0x00039F94
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryGroupByExpression queryGroupByExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryGroupByExpression>(this, other, out flag, out queryGroupByExpression))
			{
				return flag;
			}
			return this.Input.Equals(queryGroupByExpression.Input) && this.GroupItems.SequenceEqual(queryGroupByExpression.GroupItems) && this.Aggregates.SequenceEqual(queryGroupByExpression.Aggregates);
		}

		// Token: 0x04000B5E RID: 2910
		private readonly QueryGroupExpressionBinding _input;

		// Token: 0x04000B5F RID: 2911
		private readonly ReadOnlyCollection<IGroupItem> _groupItems;

		// Token: 0x04000B60 RID: 2912
		private readonly ReadOnlyCollection<KeyValuePair<string, QueryExpression>> _aggregates;
	}
}
