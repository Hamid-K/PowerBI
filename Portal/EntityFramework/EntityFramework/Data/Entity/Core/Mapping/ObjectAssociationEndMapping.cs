using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000552 RID: 1362
	internal class ObjectAssociationEndMapping : ObjectMemberMapping
	{
		// Token: 0x060042C0 RID: 17088 RVA: 0x000E5A88 File Offset: 0x000E3C88
		internal ObjectAssociationEndMapping(AssociationEndMember edmAssociationEnd, AssociationEndMember clrAssociationEnd)
			: base(edmAssociationEnd, clrAssociationEnd)
		{
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x060042C1 RID: 17089 RVA: 0x000E5A92 File Offset: 0x000E3C92
		internal override MemberMappingKind MemberMappingKind
		{
			get
			{
				return MemberMappingKind.AssociationEndMapping;
			}
		}
	}
}
