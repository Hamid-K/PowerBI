using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x02000273 RID: 627
	internal sealed class DottedIdentifierToken : PathToken
	{
		// Token: 0x060015DB RID: 5595 RVA: 0x0004C0CA File Offset: 0x0004A2CA
		public DottedIdentifierToken(string identifier, QueryToken nextToken)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(identifier, "Identifier");
			this.identifier = identifier;
			this.nextToken = nextToken;
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x0004C0EB File Offset: 0x0004A2EB
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.DottedIdentifier;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x0004C0EF File Offset: 0x0004A2EF
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x0004C0F7 File Offset: 0x0004A2F7
		// (set) Token: 0x060015DF RID: 5599 RVA: 0x0004C0FF File Offset: 0x0004A2FF
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

		// Token: 0x060015E0 RID: 5600 RVA: 0x0004C108 File Offset: 0x0004A308
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000911 RID: 2321
		private readonly string identifier;

		// Token: 0x04000912 RID: 2322
		private QueryToken nextToken;
	}
}
