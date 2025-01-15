using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000179 RID: 377
	public abstract class PathSegmentToken
	{
		// Token: 0x06000FB5 RID: 4021 RVA: 0x0002C1B8 File Offset: 0x0002A3B8
		protected PathSegmentToken(PathSegmentToken nextToken)
		{
			this.nextToken = nextToken;
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x0002C1C7 File Offset: 0x0002A3C7
		public PathSegmentToken NextToken
		{
			get
			{
				return this.nextToken;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000FB7 RID: 4023
		public abstract string Identifier { get; }

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x0002C1CF File Offset: 0x0002A3CF
		// (set) Token: 0x06000FB9 RID: 4025 RVA: 0x0002C1D7 File Offset: 0x0002A3D7
		public bool IsStructuralProperty { get; set; }

		// Token: 0x06000FBA RID: 4026
		public abstract bool IsNamespaceOrContainerQualified();

		// Token: 0x06000FBB RID: 4027
		public abstract T Accept<T>(IPathSegmentTokenVisitor<T> visitor);

		// Token: 0x06000FBC RID: 4028
		public abstract void Accept(IPathSegmentTokenVisitor visitor);

		// Token: 0x06000FBD RID: 4029 RVA: 0x0002C1E0 File Offset: 0x0002A3E0
		internal void SetNextToken(PathSegmentToken nextTokenIn)
		{
			this.nextToken = nextTokenIn;
		}

		// Token: 0x040007DC RID: 2012
		private PathSegmentToken nextToken;
	}
}
