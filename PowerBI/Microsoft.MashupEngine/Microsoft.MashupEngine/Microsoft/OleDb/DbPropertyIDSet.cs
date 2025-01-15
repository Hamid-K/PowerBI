using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E95 RID: 7829
	internal sealed class DbPropertyIDSet
	{
		// Token: 0x0600C1A0 RID: 49568 RVA: 0x0026F044 File Offset: 0x0026D244
		public DbPropertyIDSet(Guid propertyGroup, DBPROPID[] IDs)
		{
			this.propertyGroup = propertyGroup;
			this.ids = IDs;
		}

		// Token: 0x17002F5A RID: 12122
		// (get) Token: 0x0600C1A1 RID: 49569 RVA: 0x0026F05A File Offset: 0x0026D25A
		public Guid Group
		{
			get
			{
				return this.propertyGroup;
			}
		}

		// Token: 0x17002F5B RID: 12123
		// (get) Token: 0x0600C1A2 RID: 49570 RVA: 0x0026F062 File Offset: 0x0026D262
		public DBPROPID[] IDs
		{
			get
			{
				return this.ids;
			}
		}

		// Token: 0x0400619D RID: 24989
		private Guid propertyGroup;

		// Token: 0x0400619E RID: 24990
		private DBPROPID[] ids;
	}
}
