using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000160 RID: 352
	public sealed class SingleValuePropertyAccessNode : SingleValueNode
	{
		// Token: 0x06000F21 RID: 3873 RVA: 0x0002B7D4 File Offset: 0x000299D4
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

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x0002B845 File Offset: 0x00029A45
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x0002B84D File Offset: 0x00029A4D
		public IEdmProperty Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0002B855 File Offset: 0x00029A55
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.Property.Type;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x0002B862 File Offset: 0x00029A62
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleValuePropertyAccess;
			}
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0002B865 File Offset: 0x00029A65
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040007A4 RID: 1956
		private readonly SingleValueNode source;

		// Token: 0x040007A5 RID: 1957
		private readonly IEdmProperty property;
	}
}
