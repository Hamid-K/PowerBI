using System;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x0200027C RID: 636
	internal abstract class PathSegmentToken : ODataAnnotatable
	{
		// Token: 0x0600161E RID: 5662 RVA: 0x0004C4B4 File Offset: 0x0004A6B4
		protected PathSegmentToken(PathSegmentToken nextToken)
		{
			this.nextToken = nextToken;
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x0004C4C3 File Offset: 0x0004A6C3
		public PathSegmentToken NextToken
		{
			get
			{
				return this.nextToken;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001620 RID: 5664
		public abstract string Identifier { get; }

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x0004C4CB File Offset: 0x0004A6CB
		// (set) Token: 0x06001622 RID: 5666 RVA: 0x0004C4D3 File Offset: 0x0004A6D3
		public bool IsStructuralProperty { get; set; }

		// Token: 0x06001623 RID: 5667
		public abstract bool IsNamespaceOrContainerQualified();

		// Token: 0x06001624 RID: 5668
		public abstract T Accept<T>(IPathSegmentTokenVisitor<T> visitor);

		// Token: 0x06001625 RID: 5669
		public abstract void Accept(IPathSegmentTokenVisitor visitor);

		// Token: 0x06001626 RID: 5670 RVA: 0x0004C4DC File Offset: 0x0004A6DC
		internal void SetNextToken(PathSegmentToken nextTokenIn)
		{
			this.nextToken = nextTokenIn;
		}

		// Token: 0x0400092F RID: 2351
		private PathSegmentToken nextToken;
	}
}
