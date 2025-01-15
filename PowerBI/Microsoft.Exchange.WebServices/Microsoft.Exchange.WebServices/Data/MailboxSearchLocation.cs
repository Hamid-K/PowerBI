using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000219 RID: 537
	public enum MailboxSearchLocation
	{
		// Token: 0x04000E99 RID: 3737
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		PrimaryOnly,
		// Token: 0x04000E9A RID: 3738
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		ArchiveOnly,
		// Token: 0x04000E9B RID: 3739
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		All
	}
}
