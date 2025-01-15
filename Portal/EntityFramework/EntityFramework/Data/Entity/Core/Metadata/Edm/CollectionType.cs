using System;
using System.Data.Entity.Utilities;
using System.Text;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000490 RID: 1168
	public class CollectionType : EdmType
	{
		// Token: 0x060039C5 RID: 14789 RVA: 0x000BE506 File Offset: 0x000BC706
		internal CollectionType()
		{
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x000BE50E File Offset: 0x000BC70E
		internal CollectionType(EdmType elementType)
			: this(TypeUsage.Create(elementType))
		{
			this.DataSpace = elementType.DataSpace;
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x000BE528 File Offset: 0x000BC728
		internal CollectionType(TypeUsage elementType)
			: base(CollectionType.GetIdentity(Check.NotNull<TypeUsage>(elementType, "elementType")), "Transient", elementType.EdmType.DataSpace)
		{
			this._typeUsage = elementType;
			this.SetReadOnly();
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x060039C8 RID: 14792 RVA: 0x000BE55D File Offset: 0x000BC75D
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.CollectionType;
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x060039C9 RID: 14793 RVA: 0x000BE560 File Offset: 0x000BC760
		[MetadataProperty(BuiltInTypeKind.TypeUsage, false)]
		public virtual TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsage;
			}
		}

		// Token: 0x060039CA RID: 14794 RVA: 0x000BE568 File Offset: 0x000BC768
		private static string GetIdentity(TypeUsage typeUsage)
		{
			StringBuilder stringBuilder = new StringBuilder(50);
			stringBuilder.Append("collection[");
			typeUsage.BuildIdentity(stringBuilder);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x000BE5A4 File Offset: 0x000BC7A4
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

		// Token: 0x0400134D RID: 4941
		private readonly TypeUsage _typeUsage;
	}
}
