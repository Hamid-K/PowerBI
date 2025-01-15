using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E97 RID: 7831
	public sealed class DbPropertySet
	{
		// Token: 0x0600C1AA RID: 49578 RVA: 0x0026F110 File Offset: 0x0026D310
		public DbPropertySet(Guid propertyGroup, DbProperty[] properties)
		{
			this.propertyGroup = propertyGroup;
			this.properties = properties;
		}

		// Token: 0x17002F5F RID: 12127
		// (get) Token: 0x0600C1AB RID: 49579 RVA: 0x0026F126 File Offset: 0x0026D326
		public Guid Group
		{
			get
			{
				return this.propertyGroup;
			}
		}

		// Token: 0x17002F60 RID: 12128
		// (get) Token: 0x0600C1AC RID: 49580 RVA: 0x0026F12E File Offset: 0x0026D32E
		public DbProperty[] Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x040061A2 RID: 24994
		private Guid propertyGroup;

		// Token: 0x040061A3 RID: 24995
		private DbProperty[] properties;
	}
}
