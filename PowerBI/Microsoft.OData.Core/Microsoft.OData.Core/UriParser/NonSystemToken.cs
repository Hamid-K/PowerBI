using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001C3 RID: 451
	public sealed class NonSystemToken : PathSegmentToken
	{
		// Token: 0x060014D7 RID: 5335 RVA: 0x0003C0E7 File Offset: 0x0003A2E7
		public NonSystemToken(string identifier, IEnumerable<NamedValue> namedValues, PathSegmentToken nextToken)
			: base(nextToken)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(identifier, "identifier");
			this.identifier = identifier;
			this.namedValues = namedValues;
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x0003C10A File Offset: 0x0003A30A
		public IEnumerable<NamedValue> NamedValues
		{
			get
			{
				return this.namedValues;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x0003C112 File Offset: 0x0003A312
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0003C11A File Offset: 0x0003A31A
		public override bool IsNamespaceOrContainerQualified()
		{
			return this.identifier.Contains(".");
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0003C12C File Offset: 0x0003A32C
		public override T Accept<T>(IPathSegmentTokenVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<IPathSegmentTokenVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0003C141 File Offset: 0x0003A341
		public override void Accept(IPathSegmentTokenVisitor visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<IPathSegmentTokenVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0400091B RID: 2331
		private readonly IEnumerable<NamedValue> namedValues;

		// Token: 0x0400091C RID: 2332
		private readonly string identifier;
	}
}
