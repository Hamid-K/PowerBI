using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000176 RID: 374
	public sealed class ConvertNode : SingleValueNode
	{
		// Token: 0x060012AC RID: 4780 RVA: 0x00038902 File Offset: 0x00036B02
		public ConvertNode(SingleValueNode source, IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(source, "source");
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			this.source = source;
			this.typeReference = typeReference;
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x00038930 File Offset: 0x00036B30
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x00038938 File Offset: 0x00036B38
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x00038940 File Offset: 0x00036B40
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.Convert;
			}
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00038943 File Offset: 0x00036B43
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x0400086B RID: 2155
		private readonly SingleValueNode source;

		// Token: 0x0400086C RID: 2156
		private readonly IEdmTypeReference typeReference;
	}
}
