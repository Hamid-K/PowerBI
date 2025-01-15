using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004F4 RID: 1268
	public abstract class RelationshipSet : EntitySetBase
	{
		// Token: 0x06003EDB RID: 16091 RVA: 0x000D10D0 File Offset: 0x000CF2D0
		internal RelationshipSet(string name, string schema, string table, string definingQuery, RelationshipType relationshipType)
			: base(name, schema, table, definingQuery, relationshipType)
		{
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06003EDC RID: 16092 RVA: 0x000D10DF File Offset: 0x000CF2DF
		public new RelationshipType ElementType
		{
			get
			{
				return (RelationshipType)base.ElementType;
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06003EDD RID: 16093 RVA: 0x000D10EC File Offset: 0x000CF2EC
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.RelationshipSet;
			}
		}
	}
}
