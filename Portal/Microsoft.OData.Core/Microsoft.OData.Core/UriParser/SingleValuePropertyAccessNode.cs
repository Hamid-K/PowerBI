using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001AE RID: 430
	public sealed class SingleValuePropertyAccessNode : SingleValueNode
	{
		// Token: 0x06001458 RID: 5208 RVA: 0x0003B880 File Offset: 0x00039A80
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

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x0003B8F1 File Offset: 0x00039AF1
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x0003B8F9 File Offset: 0x00039AF9
		public IEdmProperty Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x0003B901 File Offset: 0x00039B01
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.Property.Type;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x0003B90E File Offset: 0x00039B0E
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleValuePropertyAccess;
			}
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0003B911 File Offset: 0x00039B11
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008F1 RID: 2289
		private readonly SingleValueNode source;

		// Token: 0x040008F2 RID: 2290
		private readonly IEdmProperty property;
	}
}
