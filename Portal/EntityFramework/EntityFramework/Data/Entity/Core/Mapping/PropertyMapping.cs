using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000559 RID: 1369
	public abstract class PropertyMapping : MappingItem
	{
		// Token: 0x060042D9 RID: 17113 RVA: 0x000E5C85 File Offset: 0x000E3E85
		internal PropertyMapping(EdmProperty property)
		{
			this._property = property;
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x000E5C94 File Offset: 0x000E3E94
		internal PropertyMapping()
		{
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x060042DB RID: 17115 RVA: 0x000E5C9C File Offset: 0x000E3E9C
		// (set) Token: 0x060042DC RID: 17116 RVA: 0x000E5CA4 File Offset: 0x000E3EA4
		public virtual EdmProperty Property
		{
			get
			{
				return this._property;
			}
			internal set
			{
				this._property = value;
			}
		}

		// Token: 0x040017E4 RID: 6116
		private EdmProperty _property;
	}
}
