using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000553 RID: 1363
	internal class ObjectComplexPropertyMapping : ObjectPropertyMapping
	{
		// Token: 0x060042C2 RID: 17090 RVA: 0x000E5A95 File Offset: 0x000E3C95
		internal ObjectComplexPropertyMapping(EdmProperty edmProperty, EdmProperty clrProperty)
			: base(edmProperty, clrProperty)
		{
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x060042C3 RID: 17091 RVA: 0x000E5A9F File Offset: 0x000E3C9F
		internal override MemberMappingKind MemberMappingKind
		{
			get
			{
				return MemberMappingKind.ComplexPropertyMapping;
			}
		}
	}
}
