using System;
using System.Reflection;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000129 RID: 297
	public class CollectionPropertyConfiguration : StructuralPropertyConfiguration
	{
		// Token: 0x06000A3D RID: 2621 RVA: 0x00029BE8 File Offset: 0x00027DE8
		public CollectionPropertyConfiguration(PropertyInfo property, StructuralTypeConfiguration declaringType)
			: base(property, declaringType)
		{
			if (!TypeHelper.IsCollection(property.PropertyType, out this._elementType))
			{
				throw Error.Argument("property", SRResources.CollectionPropertiesMustReturnIEnumerable, new object[]
				{
					property.Name,
					property.DeclaringType.FullName
				});
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0000623D File Offset: 0x0000443D
		public override PropertyKind Kind
		{
			get
			{
				return PropertyKind.Collection;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x00029C3D File Offset: 0x00027E3D
		public override Type RelatedClrType
		{
			get
			{
				return this.ElementType;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x00029C45 File Offset: 0x00027E45
		public Type ElementType
		{
			get
			{
				return this._elementType;
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x000282CF File Offset: 0x000264CF
		public CollectionPropertyConfiguration IsOptional()
		{
			base.OptionalProperty = true;
			return this;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x000282D9 File Offset: 0x000264D9
		public CollectionPropertyConfiguration IsRequired()
		{
			base.OptionalProperty = false;
			return this;
		}

		// Token: 0x0400033B RID: 827
		private Type _elementType;
	}
}
