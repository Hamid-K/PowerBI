using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x02000274 RID: 628
	internal sealed class EndPathToken : PathToken
	{
		// Token: 0x060015E1 RID: 5601 RVA: 0x0004C111 File Offset: 0x0004A311
		public EndPathToken(string identifier, QueryToken nextToken)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(identifier, "Identifier");
			this.identifier = identifier;
			this.nextToken = nextToken;
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x0004C132 File Offset: 0x0004A332
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.EndPath;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x0004C135 File Offset: 0x0004A335
		// (set) Token: 0x060015E4 RID: 5604 RVA: 0x0004C13D File Offset: 0x0004A33D
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

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x0004C146 File Offset: 0x0004A346
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x0004C14E File Offset: 0x0004A34E
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000913 RID: 2323
		private readonly string identifier;

		// Token: 0x04000914 RID: 2324
		private QueryToken nextToken;
	}
}
