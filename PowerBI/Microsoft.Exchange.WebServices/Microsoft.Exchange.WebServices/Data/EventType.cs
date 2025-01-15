using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000204 RID: 516
	public enum EventType
	{
		// Token: 0x04000DED RID: 3565
		[EwsEnum("StatusEvent")]
		Status,
		// Token: 0x04000DEE RID: 3566
		[EwsEnum("NewMailEvent")]
		NewMail,
		// Token: 0x04000DEF RID: 3567
		[EwsEnum("DeletedEvent")]
		Deleted,
		// Token: 0x04000DF0 RID: 3568
		[EwsEnum("ModifiedEvent")]
		Modified,
		// Token: 0x04000DF1 RID: 3569
		[EwsEnum("MovedEvent")]
		Moved,
		// Token: 0x04000DF2 RID: 3570
		[EwsEnum("CopiedEvent")]
		Copied,
		// Token: 0x04000DF3 RID: 3571
		[EwsEnum("CreatedEvent")]
		Created,
		// Token: 0x04000DF4 RID: 3572
		[RequiredServerVersion(ExchangeVersion.Exchange2010_SP1)]
		[EwsEnum("FreeBusyChangedEvent")]
		FreeBusyChanged
	}
}
