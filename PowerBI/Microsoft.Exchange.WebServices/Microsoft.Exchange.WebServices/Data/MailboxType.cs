using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200021B RID: 539
	public enum MailboxType
	{
		// Token: 0x04000EA6 RID: 3750
		[RequiredServerVersion(ExchangeVersion.Exchange2010)]
		Unknown,
		// Token: 0x04000EA7 RID: 3751
		[RequiredServerVersion(ExchangeVersion.Exchange2010)]
		OneOff,
		// Token: 0x04000EA8 RID: 3752
		Mailbox,
		// Token: 0x04000EA9 RID: 3753
		[RequiredServerVersion(ExchangeVersion.Exchange2007_SP1)]
		PublicFolder,
		// Token: 0x04000EAA RID: 3754
		[EwsEnum("PublicDL")]
		PublicGroup,
		// Token: 0x04000EAB RID: 3755
		[EwsEnum("PrivateDL")]
		ContactGroup,
		// Token: 0x04000EAC RID: 3756
		Contact
	}
}
