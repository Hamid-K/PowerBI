using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000177 RID: 375
	public sealed class NonSystemToken : PathSegmentToken
	{
		// Token: 0x06000FAA RID: 4010 RVA: 0x0002C117 File Offset: 0x0002A317
		public NonSystemToken(string identifier, IEnumerable<NamedValue> namedValues, PathSegmentToken nextToken)
			: base(nextToken)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(identifier, "identifier");
			this.identifier = identifier;
			this.namedValues = namedValues;
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x0002C13A File Offset: 0x0002A33A
		public IEnumerable<NamedValue> NamedValues
		{
			get
			{
				return this.namedValues;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x0002C142 File Offset: 0x0002A342
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0002C14A File Offset: 0x0002A34A
		public override bool IsNamespaceOrContainerQualified()
		{
			return this.identifier.Contains(".");
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0002C15C File Offset: 0x0002A35C
		public override T Accept<T>(IPathSegmentTokenVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<IPathSegmentTokenVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0002C171 File Offset: 0x0002A371
		public override void Accept(IPathSegmentTokenVisitor visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<IPathSegmentTokenVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x040007D8 RID: 2008
		private readonly IEnumerable<NamedValue> namedValues;

		// Token: 0x040007D9 RID: 2009
		private readonly string identifier;
	}
}
