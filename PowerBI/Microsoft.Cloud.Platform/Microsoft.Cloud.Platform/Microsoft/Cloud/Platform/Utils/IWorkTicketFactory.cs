using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002F3 RID: 755
	public interface IWorkTicketFactory : IIdentifiable
	{
		// Token: 0x060013F1 RID: 5105
		WorkTicket TryCreateWorkTicket(IIdentifiable owningEntity);

		// Token: 0x060013F2 RID: 5106
		WorkTicket CreateWorkTicket(IIdentifiable owningEntity);

		// Token: 0x060013F3 RID: 5107
		WorkTicket CreateWorkTicket(IIdentifiable owningEntity, WorkTicket ticket);

		// Token: 0x060013F4 RID: 5108
		bool TryCreateWorkTicket(IIdentifiable owningEntity, WorkTicket ticket);
	}
}
