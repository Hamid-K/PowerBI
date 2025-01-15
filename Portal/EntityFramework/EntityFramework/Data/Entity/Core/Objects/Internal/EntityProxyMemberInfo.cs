using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200043B RID: 1083
	internal sealed class EntityProxyMemberInfo
	{
		// Token: 0x060034DB RID: 13531 RVA: 0x000A9EA0 File Offset: 0x000A80A0
		internal EntityProxyMemberInfo(EdmMember member, int propertyIndex)
		{
			this._member = member;
			this._propertyIndex = propertyIndex;
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x060034DC RID: 13532 RVA: 0x000A9EB6 File Offset: 0x000A80B6
		internal EdmMember EdmMember
		{
			get
			{
				return this._member;
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x060034DD RID: 13533 RVA: 0x000A9EBE File Offset: 0x000A80BE
		internal int PropertyIndex
		{
			get
			{
				return this._propertyIndex;
			}
		}

		// Token: 0x04001108 RID: 4360
		private readonly EdmMember _member;

		// Token: 0x04001109 RID: 4361
		private readonly int _propertyIndex;
	}
}
