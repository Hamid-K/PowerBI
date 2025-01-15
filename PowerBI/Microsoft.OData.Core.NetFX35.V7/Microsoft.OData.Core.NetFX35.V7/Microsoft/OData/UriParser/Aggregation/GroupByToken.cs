using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001CD RID: 461
	public sealed class GroupByToken : ApplyTransformationToken
	{
		// Token: 0x060011EE RID: 4590 RVA: 0x0003209A File Offset: 0x0003029A
		public GroupByToken(IEnumerable<EndPathToken> properties, ApplyTransformationToken child)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<EndPathToken>>(properties, "properties");
			this.properties = properties;
			this.child = child;
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x000304EE File Offset: 0x0002E6EE
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.AggregateGroupBy;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x000320BC File Offset: 0x000302BC
		public IEnumerable<EndPathToken> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x000320C4 File Offset: 0x000302C4
		public ApplyTransformationToken Child
		{
			get
			{
				return this.child;
			}
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x000320CC File Offset: 0x000302CC
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400091C RID: 2332
		private readonly IEnumerable<EndPathToken> properties;

		// Token: 0x0400091D RID: 2333
		private readonly ApplyTransformationToken child;
	}
}
