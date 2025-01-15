using System;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000073 RID: 115
	public sealed class AssociationEndMember : RelationshipEndMember
	{
		// Token: 0x0600092C RID: 2348 RVA: 0x00014E2B File Offset: 0x0001302B
		internal AssociationEndMember(string name, RefType endRefType, RelationshipMultiplicity multiplicity)
			: base(name, endRefType, multiplicity)
		{
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x00014E36 File Offset: 0x00013036
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.AssociationEndMember;
			}
		}
	}
}
