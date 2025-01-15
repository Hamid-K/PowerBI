using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E8F RID: 7823
	public struct Date
	{
		// Token: 0x0600C163 RID: 49507 RVA: 0x0026E188 File Offset: 0x0026C388
		public Date(DateTime dateTime)
		{
			this.dateTime = dateTime;
		}

		// Token: 0x17002F3D RID: 12093
		// (get) Token: 0x0600C164 RID: 49508 RVA: 0x0026E191 File Offset: 0x0026C391
		public DateTime DateTime
		{
			get
			{
				return this.dateTime;
			}
		}

		// Token: 0x04006193 RID: 24979
		private readonly DateTime dateTime;
	}
}
