using System;
using System.Collections.Generic;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000115 RID: 277
	public sealed class GroupByToken : ApplyTransformationToken
	{
		// Token: 0x06000BBF RID: 3007 RVA: 0x0002C8A9 File Offset: 0x0002AAA9
		public GroupByToken(IEnumerable<EndPathToken> properties, ApplyTransformationToken child)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<EndPathToken>>(properties, "properties");
			this.properties = properties;
			this.child = child;
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0002C8CB File Offset: 0x0002AACB
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.AggregateGroupBy;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0002C8CF File Offset: 0x0002AACF
		public IEnumerable<EndPathToken> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0002C8D7 File Offset: 0x0002AAD7
		public ApplyTransformationToken Child
		{
			get
			{
				return this.child;
			}
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002C8DF File Offset: 0x0002AADF
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000650 RID: 1616
		private readonly IEnumerable<EndPathToken> properties;

		// Token: 0x04000651 RID: 1617
		private readonly ApplyTransformationToken child;
	}
}
