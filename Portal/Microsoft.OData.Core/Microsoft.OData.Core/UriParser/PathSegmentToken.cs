using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001C5 RID: 453
	public abstract class PathSegmentToken
	{
		// Token: 0x060014E2 RID: 5346 RVA: 0x0003C188 File Offset: 0x0003A388
		protected PathSegmentToken(PathSegmentToken nextToken)
		{
			this.NextToken = nextToken;
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x0003C197 File Offset: 0x0003A397
		// (set) Token: 0x060014E4 RID: 5348 RVA: 0x0003C19F File Offset: 0x0003A39F
		public PathSegmentToken NextToken { get; internal set; }

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060014E5 RID: 5349
		public abstract string Identifier { get; }

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x0003C1A8 File Offset: 0x0003A3A8
		// (set) Token: 0x060014E7 RID: 5351 RVA: 0x0003C1B0 File Offset: 0x0003A3B0
		public bool IsStructuralProperty { get; set; }

		// Token: 0x060014E8 RID: 5352
		public abstract bool IsNamespaceOrContainerQualified();

		// Token: 0x060014E9 RID: 5353
		public abstract T Accept<T>(IPathSegmentTokenVisitor<T> visitor);

		// Token: 0x060014EA RID: 5354
		public abstract void Accept(IPathSegmentTokenVisitor visitor);
	}
}
