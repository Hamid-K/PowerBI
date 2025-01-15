using System;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x0200012F RID: 303
	public abstract class PathSegmentToken
	{
		// Token: 0x06000C78 RID: 3192 RVA: 0x0002D032 File Offset: 0x0002B232
		protected PathSegmentToken(PathSegmentToken nextToken)
		{
			this.NextToken = nextToken;
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x0002D041 File Offset: 0x0002B241
		// (set) Token: 0x06000C7A RID: 3194 RVA: 0x0002D049 File Offset: 0x0002B249
		public PathSegmentToken NextToken { get; internal set; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000C7B RID: 3195
		public abstract string Identifier { get; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x0002D052 File Offset: 0x0002B252
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x0002D05A File Offset: 0x0002B25A
		public bool IsStructuralProperty { get; set; }

		// Token: 0x06000C7E RID: 3198
		public abstract bool IsNamespaceOrContainerQualified();

		// Token: 0x06000C7F RID: 3199
		public abstract T Accept<T>(IPathSegmentTokenVisitor<T> visitor);

		// Token: 0x06000C80 RID: 3200
		public abstract void Accept(IPathSegmentTokenVisitor visitor);
	}
}
