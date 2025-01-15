using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x0200027A RID: 634
	internal sealed class InnerPathToken : PathToken
	{
		// Token: 0x0600160F RID: 5647 RVA: 0x0004C3FF File Offset: 0x0004A5FF
		public InnerPathToken(string identifier, QueryToken nextToken, IEnumerable<NamedValue> namedValues)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(identifier, "Identifier");
			this.identifier = identifier;
			this.nextToken = nextToken;
			this.namedValues = ((namedValues == null) ? null : new ReadOnlyEnumerableForUriParser<NamedValue>(namedValues));
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x0004C432 File Offset: 0x0004A632
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.InnerPath;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x0004C436 File Offset: 0x0004A636
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x0004C43E File Offset: 0x0004A63E
		// (set) Token: 0x06001613 RID: 5651 RVA: 0x0004C446 File Offset: 0x0004A646
		public override QueryToken NextToken
		{
			get
			{
				return this.nextToken;
			}
			set
			{
				this.nextToken = value;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x0004C44F File Offset: 0x0004A64F
		public IEnumerable<NamedValue> NamedValues
		{
			get
			{
				return this.namedValues;
			}
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x0004C457 File Offset: 0x0004A657
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000929 RID: 2345
		private readonly string identifier;

		// Token: 0x0400092A RID: 2346
		private readonly IEnumerable<NamedValue> namedValues;

		// Token: 0x0400092B RID: 2347
		private QueryToken nextToken;
	}
}
