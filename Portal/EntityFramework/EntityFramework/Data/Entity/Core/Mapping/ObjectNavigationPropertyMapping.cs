using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000556 RID: 1366
	internal class ObjectNavigationPropertyMapping : ObjectMemberMapping
	{
		// Token: 0x060042C8 RID: 17096 RVA: 0x000E5AC8 File Offset: 0x000E3CC8
		internal ObjectNavigationPropertyMapping(NavigationProperty edmNavigationProperty, NavigationProperty clrNavigationProperty)
			: base(edmNavigationProperty, clrNavigationProperty)
		{
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x060042C9 RID: 17097 RVA: 0x000E5AD2 File Offset: 0x000E3CD2
		internal override MemberMappingKind MemberMappingKind
		{
			get
			{
				return MemberMappingKind.NavigationPropertyMapping;
			}
		}
	}
}
