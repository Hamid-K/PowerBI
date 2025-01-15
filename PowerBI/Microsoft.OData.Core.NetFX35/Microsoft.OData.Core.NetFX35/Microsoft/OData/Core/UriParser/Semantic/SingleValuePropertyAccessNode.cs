using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000265 RID: 613
	public sealed class SingleValuePropertyAccessNode : SingleValueNode
	{
		// Token: 0x06001598 RID: 5528 RVA: 0x0004BC40 File Offset: 0x00049E40
		public SingleValuePropertyAccessNode(SingleValueNode source, IEdmProperty property)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(source, "source");
			ExceptionUtils.CheckArgumentNotNull<IEdmProperty>(property, "property");
			if (property.PropertyKind != EdmPropertyKind.Structural)
			{
				throw new ArgumentException(Strings.Nodes_PropertyAccessShouldBeNonEntityProperty(property.Name));
			}
			if (property.Type.IsCollection())
			{
				throw new ArgumentException(Strings.Nodes_PropertyAccessTypeShouldNotBeCollection(property.Name));
			}
			this.source = source;
			this.property = property;
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x0004BCAF File Offset: 0x00049EAF
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x0004BCB7 File Offset: 0x00049EB7
		public IEdmProperty Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x0004BCBF File Offset: 0x00049EBF
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.Property.Type;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x0004BCCC File Offset: 0x00049ECC
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleValuePropertyAccess;
			}
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x0004BCCF File Offset: 0x00049ECF
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008FD RID: 2301
		private readonly SingleValueNode source;

		// Token: 0x040008FE RID: 2302
		private readonly IEdmProperty property;
	}
}
