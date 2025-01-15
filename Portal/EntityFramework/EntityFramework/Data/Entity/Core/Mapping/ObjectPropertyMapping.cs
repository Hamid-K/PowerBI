using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000557 RID: 1367
	internal class ObjectPropertyMapping : ObjectMemberMapping
	{
		// Token: 0x060042CA RID: 17098 RVA: 0x000E5AD5 File Offset: 0x000E3CD5
		internal ObjectPropertyMapping(EdmProperty edmProperty, EdmProperty clrProperty)
			: base(edmProperty, clrProperty)
		{
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x060042CB RID: 17099 RVA: 0x000E5ADF File Offset: 0x000E3CDF
		internal EdmProperty ClrProperty
		{
			get
			{
				return (EdmProperty)base.ClrMember;
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x060042CC RID: 17100 RVA: 0x000E5AEC File Offset: 0x000E3CEC
		internal override MemberMappingKind MemberMappingKind
		{
			get
			{
				return MemberMappingKind.ScalarPropertyMapping;
			}
		}
	}
}
