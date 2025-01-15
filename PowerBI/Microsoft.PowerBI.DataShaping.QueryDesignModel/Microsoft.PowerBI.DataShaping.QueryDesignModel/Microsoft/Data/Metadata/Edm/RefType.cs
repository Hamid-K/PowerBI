using System;
using System.Data;
using System.Text;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000A9 RID: 169
	public sealed class RefType : EdmType
	{
		// Token: 0x06000B66 RID: 2918 RVA: 0x0001D2C1 File Offset: 0x0001B4C1
		internal RefType(EntityType entityType)
			: base(RefType.GetIdentity(EntityUtil.GenericCheckArgumentNull<EntityType>(entityType, "entityType")), "Transient", entityType.DataSpace)
		{
			this._elementType = entityType;
			this.SetReadOnly();
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x0001D2F1 File Offset: 0x0001B4F1
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.RefType;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0001D2F5 File Offset: 0x0001B4F5
		[MetadataProperty(BuiltInTypeKind.EntityTypeBase, false)]
		public EntityTypeBase ElementType
		{
			get
			{
				return this._elementType;
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0001D300 File Offset: 0x0001B500
		private static string GetIdentity(EntityTypeBase entityTypeBase)
		{
			StringBuilder stringBuilder = new StringBuilder(50);
			stringBuilder.Append("reference[");
			entityTypeBase.BuildIdentity(stringBuilder);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x040008A6 RID: 2214
		private readonly EntityTypeBase _elementType;
	}
}
