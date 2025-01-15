using System;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200025C RID: 604
	public sealed class SearchTermNode : SingleValueNode
	{
		// Token: 0x0600154B RID: 5451 RVA: 0x0004B09C File Offset: 0x0004929C
		public SearchTermNode(string text)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(text, "literalText");
			this.text = text;
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x0004B0B6 File Offset: 0x000492B6
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x0004B0BE File Offset: 0x000492BE
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return SearchTermNode.BoolTypeReference;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x0004B0C5 File Offset: 0x000492C5
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SearchTerm;
			}
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0004B0C9 File Offset: 0x000492C9
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008DF RID: 2271
		private static readonly IEdmTypeReference BoolTypeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(typeof(bool));

		// Token: 0x040008E0 RID: 2272
		private readonly string text;
	}
}
