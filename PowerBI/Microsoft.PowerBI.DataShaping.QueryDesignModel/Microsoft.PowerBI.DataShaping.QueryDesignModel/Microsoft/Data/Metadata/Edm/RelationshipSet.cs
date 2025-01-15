using System;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000AC RID: 172
	public abstract class RelationshipSet : EntitySetBase
	{
		// Token: 0x06000B6F RID: 2927 RVA: 0x0001D3B2 File Offset: 0x0001B5B2
		internal RelationshipSet(string name, string schema, string table, string definingQuery, RelationshipType relationshipType)
			: base(name, schema, table, definingQuery, relationshipType)
		{
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0001D3C1 File Offset: 0x0001B5C1
		public new RelationshipType ElementType
		{
			get
			{
				return (RelationshipType)base.ElementType;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x0001D3CE File Offset: 0x0001B5CE
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.RelationshipSet;
			}
		}
	}
}
