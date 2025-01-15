using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002B7 RID: 695
	internal class ThrottlerAsyncResult : VoidAsyncResultWithWorkTicket<WorkTicket>, IIdentifiable
	{
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x0004149D File Offset: 0x0003F69D
		// (set) Token: 0x060012BE RID: 4798 RVA: 0x000414A5 File Offset: 0x0003F6A5
		public string Name { get; private set; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x000414AE File Offset: 0x0003F6AE
		// (set) Token: 0x060012C0 RID: 4800 RVA: 0x000414B6 File Offset: 0x0003F6B6
		public bool IsQueueFull { get; set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x000414BF File Offset: 0x0003F6BF
		// (set) Token: 0x060012C2 RID: 4802 RVA: 0x000414C7 File Offset: 0x0003F6C7
		public DateTime EnqueuedDateTime { get; set; }

		// Token: 0x060012C3 RID: 4803 RVA: 0x000414D0 File Offset: 0x0003F6D0
		internal ThrottlerAsyncResult(string name, WorkTicket ticket, AsyncCallback callback, object context)
			: base(ticket, callback, context)
		{
			this.Name = name;
		}
	}
}
