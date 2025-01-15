using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E99 RID: 7833
	internal sealed class DbPropertyInfoSet
	{
		// Token: 0x0600C1AF RID: 49583 RVA: 0x0026F2B0 File Offset: 0x0026D4B0
		public DbPropertyInfoSet(Guid propertyGroup, PropertyInfo[] propertyInfos)
		{
			this.propertyGroup = propertyGroup;
			this.propertyInfos = propertyInfos;
		}

		// Token: 0x17002F61 RID: 12129
		// (get) Token: 0x0600C1B0 RID: 49584 RVA: 0x0026F2C6 File Offset: 0x0026D4C6
		public Guid Group
		{
			get
			{
				return this.propertyGroup;
			}
		}

		// Token: 0x17002F62 RID: 12130
		// (get) Token: 0x0600C1B1 RID: 49585 RVA: 0x0026F2CE File Offset: 0x0026D4CE
		public PropertyInfo[] Infos
		{
			get
			{
				return this.propertyInfos;
			}
		}

		// Token: 0x040061A4 RID: 24996
		private Guid propertyGroup;

		// Token: 0x040061A5 RID: 24997
		private PropertyInfo[] propertyInfos;
	}
}
