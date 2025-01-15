using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001FD RID: 509
	public sealed class GroupByToken : ApplyTransformationToken
	{
		// Token: 0x06001697 RID: 5783 RVA: 0x0003F36F File Offset: 0x0003D56F
		public GroupByToken(IEnumerable<EndPathToken> properties, ApplyTransformationToken child)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<EndPathToken>>(properties, "properties");
			this.properties = properties;
			this.child = child;
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x00025E8A File Offset: 0x0002408A
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.AggregateGroupBy;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x0003F391 File Offset: 0x0003D591
		public IEnumerable<EndPathToken> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x0003F399 File Offset: 0x0003D599
		public ApplyTransformationToken Child
		{
			get
			{
				return this.child;
			}
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0003F3A1 File Offset: 0x0003D5A1
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000A30 RID: 2608
		private readonly IEnumerable<EndPathToken> properties;

		// Token: 0x04000A31 RID: 2609
		private readonly ApplyTransformationToken child;
	}
}
