using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000262 RID: 610
	public sealed class SingleValueCastNode : SingleValueNode
	{
		// Token: 0x06001584 RID: 5508 RVA: 0x0004BAB5 File Offset: 0x00049CB5
		public SingleValueCastNode(SingleValueNode source, IEdmComplexType complexType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmComplexType>(complexType, "complexType");
			this.source = source;
			this.typeReference = new EdmComplexTypeReference(complexType, false);
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x0004BADC File Offset: 0x00049CDC
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x0004BAE4 File Offset: 0x00049CE4
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x0004BAEC File Offset: 0x00049CEC
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleValueCast;
			}
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x0004BAF0 File Offset: 0x00049CF0
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008F4 RID: 2292
		private readonly SingleValueNode source;

		// Token: 0x040008F5 RID: 2293
		private readonly IEdmComplexTypeReference typeReference;
	}
}
