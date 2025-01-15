using System;
using System.Collections.Generic;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x0200012D RID: 301
	public sealed class NonSystemToken : PathSegmentToken
	{
		// Token: 0x06000C6D RID: 3181 RVA: 0x0002CF8E File Offset: 0x0002B18E
		public NonSystemToken(string identifier, IEnumerable<NamedValue> namedValues, PathSegmentToken nextToken)
			: base(nextToken)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(identifier, "identifier");
			this.identifier = identifier;
			this.namedValues = namedValues;
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0002CFB1 File Offset: 0x0002B1B1
		public IEnumerable<NamedValue> NamedValues
		{
			get
			{
				return this.namedValues;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x0002CFB9 File Offset: 0x0002B1B9
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0002CFC1 File Offset: 0x0002B1C1
		public override bool IsNamespaceOrContainerQualified()
		{
			return this.identifier.Contains(".");
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0002CFD3 File Offset: 0x0002B1D3
		public override T Accept<T>(IPathSegmentTokenVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<IPathSegmentTokenVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0002CFE8 File Offset: 0x0002B1E8
		public override void Accept(IPathSegmentTokenVisitor visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<IPathSegmentTokenVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0400067A RID: 1658
		private readonly IEnumerable<NamedValue> namedValues;

		// Token: 0x0400067B RID: 1659
		private readonly string identifier;
	}
}
