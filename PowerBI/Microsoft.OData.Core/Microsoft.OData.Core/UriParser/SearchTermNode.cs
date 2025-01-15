using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001A3 RID: 419
	public sealed class SearchTermNode : SingleValueNode
	{
		// Token: 0x0600140A RID: 5130 RVA: 0x0003AD1E File Offset: 0x00038F1E
		public SearchTermNode(string text)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(text, "literalText");
			this.text = text;
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x0003AD38 File Offset: 0x00038F38
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x0003AD40 File Offset: 0x00038F40
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return SearchTermNode.BoolTypeReference;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x0003AD47 File Offset: 0x00038F47
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SearchTerm;
			}
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0003AD4B File Offset: 0x00038F4B
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008D7 RID: 2263
		private static readonly IEdmTypeReference BoolTypeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(typeof(bool));

		// Token: 0x040008D8 RID: 2264
		private readonly string text;
	}
}
