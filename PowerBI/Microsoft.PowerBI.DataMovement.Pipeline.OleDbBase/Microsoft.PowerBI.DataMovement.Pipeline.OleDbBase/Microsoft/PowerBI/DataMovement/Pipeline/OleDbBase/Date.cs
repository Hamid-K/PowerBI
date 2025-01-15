using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200003A RID: 58
	public struct Date
	{
		// Token: 0x06000202 RID: 514 RVA: 0x00006242 File Offset: 0x00004442
		public Date(DateTime dateTime)
		{
			this.dateTime = dateTime;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000624B File Offset: 0x0000444B
		public DateTime DateTime
		{
			get
			{
				return this.dateTime;
			}
		}

		// Token: 0x0400007E RID: 126
		private readonly DateTime dateTime;
	}
}
