using System;
using System.Data;
using System.Text;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x0200007A RID: 122
	public sealed class CollectionType : EdmType
	{
		// Token: 0x06000951 RID: 2385 RVA: 0x000156A4 File Offset: 0x000138A4
		internal CollectionType(EdmType elementType)
			: this(TypeUsage.Create(elementType))
		{
			base.DataSpace = elementType.DataSpace;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x000156BE File Offset: 0x000138BE
		internal CollectionType(TypeUsage elementType)
			: base(CollectionType.GetIdentity(EntityUtil.GenericCheckArgumentNull<TypeUsage>(elementType, "elementType")), "Transient", elementType.EdmType.DataSpace)
		{
			this._typeUsage = elementType;
			this.SetReadOnly();
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x000156F3 File Offset: 0x000138F3
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.CollectionType;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x000156F6 File Offset: 0x000138F6
		[MetadataProperty(BuiltInTypeKind.TypeUsage, false)]
		public TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsage;
			}
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00015700 File Offset: 0x00013900
		private static string GetIdentity(TypeUsage typeUsage)
		{
			StringBuilder stringBuilder = new StringBuilder(50);
			stringBuilder.Append("collection[");
			typeUsage.BuildIdentity(stringBuilder);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0001573C File Offset: 0x0001393C
		internal override bool EdmEquals(MetadataItem item)
		{
			if (this == item)
			{
				return true;
			}
			if (item == null || BuiltInTypeKind.CollectionType != item.BuiltInTypeKind)
			{
				return false;
			}
			CollectionType collectionType = (CollectionType)item;
			return this.TypeUsage.EdmEquals(collectionType.TypeUsage);
		}

		// Token: 0x04000764 RID: 1892
		private readonly TypeUsage _typeUsage;
	}
}
