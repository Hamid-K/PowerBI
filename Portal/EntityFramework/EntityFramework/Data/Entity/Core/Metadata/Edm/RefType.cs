using System;
using System.Data.Entity.Utilities;
using System.Text;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004F0 RID: 1264
	public class RefType : EdmType
	{
		// Token: 0x06003ECC RID: 16076 RVA: 0x000D0F03 File Offset: 0x000CF103
		internal RefType()
		{
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x000D0F0B File Offset: 0x000CF10B
		internal RefType(EntityType entityType)
			: base(RefType.GetIdentity(Check.NotNull<EntityType>(entityType, "entityType")), "Transient", entityType.DataSpace)
		{
			this._elementType = entityType;
			this.SetReadOnly();
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06003ECE RID: 16078 RVA: 0x000D0F3B File Offset: 0x000CF13B
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.RefType;
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06003ECF RID: 16079 RVA: 0x000D0F3F File Offset: 0x000CF13F
		[MetadataProperty(BuiltInTypeKind.EntityTypeBase, false)]
		public virtual EntityTypeBase ElementType
		{
			get
			{
				return this._elementType;
			}
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x000D0F48 File Offset: 0x000CF148
		private static string GetIdentity(EntityTypeBase entityTypeBase)
		{
			StringBuilder stringBuilder = new StringBuilder(50);
			stringBuilder.Append("reference[");
			entityTypeBase.BuildIdentity(stringBuilder);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x000D0F82 File Offset: 0x000CF182
		public override int GetHashCode()
		{
			return (this._elementType.GetHashCode() * 397) ^ typeof(RefType).GetHashCode();
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x000D0FA8 File Offset: 0x000CF1A8
		public override bool Equals(object obj)
		{
			RefType refType = obj as RefType;
			return refType != null && refType._elementType == this._elementType;
		}

		// Token: 0x04001570 RID: 5488
		private readonly EntityTypeBase _elementType;
	}
}
