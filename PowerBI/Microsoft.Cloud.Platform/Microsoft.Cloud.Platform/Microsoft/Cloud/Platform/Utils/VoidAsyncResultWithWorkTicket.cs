using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002E6 RID: 742
	public class VoidAsyncResultWithWorkTicket<TWorkTicket> : VoidAsyncResult where TWorkTicket : WorkTicket
	{
		// Token: 0x060013CB RID: 5067 RVA: 0x00044BE7 File Offset: 0x00042DE7
		public VoidAsyncResultWithWorkTicket(TWorkTicket ticket, AsyncCallback callback, object context)
			: base(callback, context)
		{
			this.WorkTicket = ticket;
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x00044BF8 File Offset: 0x00042DF8
		// (set) Token: 0x060013CD RID: 5069 RVA: 0x00044C00 File Offset: 0x00042E00
		public TWorkTicket WorkTicket { get; private set; }
	}
}
