using System;
using System.Globalization;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x0200012A RID: 298
	public class CollectionTypeConfiguration : IEdmTypeConfiguration
	{
		// Token: 0x06000A43 RID: 2627 RVA: 0x00029C4D File Offset: 0x00027E4D
		public CollectionTypeConfiguration(IEdmTypeConfiguration elementType, Type clrType)
		{
			if (elementType == null)
			{
				throw Error.ArgumentNull("elementType");
			}
			if (clrType == null)
			{
				throw Error.ArgumentNull("clrType");
			}
			this._elementType = elementType;
			this._clrType = clrType;
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x00029C85 File Offset: 0x00027E85
		public IEdmTypeConfiguration ElementType
		{
			get
			{
				return this._elementType;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00029C8D File Offset: 0x00027E8D
		public Type ClrType
		{
			get
			{
				return this._clrType;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00029C95 File Offset: 0x00027E95
		public string FullName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x00029C9D File Offset: 0x00027E9D
		public string Namespace
		{
			get
			{
				return "Edm";
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x00029CA4 File Offset: 0x00027EA4
		public string Name
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "Collection({0})", new object[] { this.ElementType.FullName });
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x0000605C File Offset: 0x0000425C
		public EdmTypeKind Kind
		{
			get
			{
				return EdmTypeKind.Collection;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x00029CC9 File Offset: 0x00027EC9
		public ODataModelBuilder ModelBuilder
		{
			get
			{
				return this._elementType.ModelBuilder;
			}
		}

		// Token: 0x0400033C RID: 828
		private IEdmTypeConfiguration _elementType;

		// Token: 0x0400033D RID: 829
		private Type _clrType;
	}
}
