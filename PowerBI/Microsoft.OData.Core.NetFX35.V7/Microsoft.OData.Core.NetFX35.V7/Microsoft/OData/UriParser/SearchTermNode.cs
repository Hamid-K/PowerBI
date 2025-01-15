using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000157 RID: 343
	public sealed class SearchTermNode : SingleValueNode
	{
		// Token: 0x06000EE5 RID: 3813 RVA: 0x0002ADD6 File Offset: 0x00028FD6
		public SearchTermNode(string text)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(text, "literalText");
			this.text = text;
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x0002ADF0 File Offset: 0x00028FF0
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x0002ADF8 File Offset: 0x00028FF8
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return SearchTermNode.BoolTypeReference;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x0002ADFF File Offset: 0x00028FFF
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SearchTerm;
			}
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0002AE03 File Offset: 0x00029003
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000793 RID: 1939
		private static readonly IEdmTypeReference BoolTypeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(typeof(bool));

		// Token: 0x04000794 RID: 1940
		private readonly string text;
	}
}
