using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x0200027D RID: 637
	internal sealed class NonSystemToken : PathSegmentToken
	{
		// Token: 0x06001627 RID: 5671 RVA: 0x0004C4E5 File Offset: 0x0004A6E5
		public NonSystemToken(string identifier, IEnumerable<NamedValue> namedValues, PathSegmentToken nextToken)
			: base(nextToken)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(identifier, "identifier");
			this.identifier = identifier;
			this.namedValues = namedValues;
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x0004C507 File Offset: 0x0004A707
		public IEnumerable<NamedValue> NamedValues
		{
			get
			{
				return this.namedValues;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x0004C50F File Offset: 0x0004A70F
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x0004C517 File Offset: 0x0004A717
		public override bool IsNamespaceOrContainerQualified()
		{
			return this.identifier.Contains(".");
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x0004C529 File Offset: 0x0004A729
		public override T Accept<T>(IPathSegmentTokenVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<IPathSegmentTokenVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x0004C53D File Offset: 0x0004A73D
		public override void Accept(IPathSegmentTokenVisitor visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<IPathSegmentTokenVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x04000931 RID: 2353
		private readonly IEnumerable<NamedValue> namedValues;

		// Token: 0x04000932 RID: 2354
		private readonly string identifier;
	}
}
