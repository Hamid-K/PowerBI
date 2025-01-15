using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000250 RID: 592
	public enum ViewFilter
	{
		// Token: 0x04001274 RID: 4724
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		All,
		// Token: 0x04001275 RID: 4725
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		Flagged,
		// Token: 0x04001276 RID: 4726
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		HasAttachment,
		// Token: 0x04001277 RID: 4727
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		ToOrCcMe,
		// Token: 0x04001278 RID: 4728
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		Unread,
		// Token: 0x04001279 RID: 4729
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		TaskActive,
		// Token: 0x0400127A RID: 4730
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		TaskOverdue,
		// Token: 0x0400127B RID: 4731
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		TaskCompleted,
		// Token: 0x0400127C RID: 4732
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		Suggestions,
		// Token: 0x0400127D RID: 4733
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		SuggestionsRespond,
		// Token: 0x0400127E RID: 4734
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		SuggestionsDelete
	}
}
