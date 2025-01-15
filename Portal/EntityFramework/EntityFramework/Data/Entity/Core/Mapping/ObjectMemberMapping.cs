using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000554 RID: 1364
	internal abstract class ObjectMemberMapping
	{
		// Token: 0x060042C4 RID: 17092 RVA: 0x000E5AA2 File Offset: 0x000E3CA2
		protected ObjectMemberMapping(EdmMember edmMember, EdmMember clrMember)
		{
			this.m_edmMember = edmMember;
			this.m_clrMember = clrMember;
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x060042C5 RID: 17093 RVA: 0x000E5AB8 File Offset: 0x000E3CB8
		internal EdmMember EdmMember
		{
			get
			{
				return this.m_edmMember;
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x060042C6 RID: 17094 RVA: 0x000E5AC0 File Offset: 0x000E3CC0
		internal EdmMember ClrMember
		{
			get
			{
				return this.m_clrMember;
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x060042C7 RID: 17095
		internal abstract MemberMappingKind MemberMappingKind { get; }

		// Token: 0x040017D8 RID: 6104
		private readonly EdmMember m_edmMember;

		// Token: 0x040017D9 RID: 6105
		private readonly EdmMember m_clrMember;
	}
}
