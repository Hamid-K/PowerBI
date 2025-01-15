using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Aggregation
{
	// Token: 0x020002B0 RID: 688
	internal sealed class GroupByToken : ApplyTransformationToken
	{
		// Token: 0x060017AB RID: 6059 RVA: 0x00050DB9 File Offset: 0x0004EFB9
		public GroupByToken(IEnumerable<EndPathToken> properties, ApplyTransformationToken child)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<EndPathToken>>(properties, "properties");
			this.properties = properties;
			this.child = child;
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x00050DDA File Offset: 0x0004EFDA
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.AggregateGroupBy;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x00050DDE File Offset: 0x0004EFDE
		public IEnumerable<EndPathToken> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x00050DE6 File Offset: 0x0004EFE6
		public ApplyTransformationToken Child
		{
			get
			{
				return this.child;
			}
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x00050DEE File Offset: 0x0004EFEE
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000A31 RID: 2609
		private readonly IEnumerable<EndPathToken> properties;

		// Token: 0x04000A32 RID: 2610
		private readonly ApplyTransformationToken child;
	}
}
