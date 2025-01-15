using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x02000281 RID: 641
	internal sealed class StarToken : PathToken
	{
		// Token: 0x0600163A RID: 5690 RVA: 0x0004C5F3 File Offset: 0x0004A7F3
		public StarToken(QueryToken nextToken)
		{
			this.nextToken = nextToken;
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x0004C602 File Offset: 0x0004A802
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Star;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x0004C606 File Offset: 0x0004A806
		// (set) Token: 0x0600163D RID: 5693 RVA: 0x0004C60E File Offset: 0x0004A80E
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

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x0004C617 File Offset: 0x0004A817
		public override string Identifier
		{
			get
			{
				return "*";
			}
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x0004C61E File Offset: 0x0004A81E
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000937 RID: 2359
		private QueryToken nextToken;
	}
}
