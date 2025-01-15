using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001A6 RID: 422
	[ServiceObjectDefinition("DeclineItem", ReturnedByServer = false)]
	public sealed class DeclineMeetingInvitationMessage : CalendarResponseMessage<MeetingResponse>
	{
		// Token: 0x0600144D RID: 5197 RVA: 0x000372FD File Offset: 0x000362FD
		internal DeclineMeetingInvitationMessage(Item referenceItem)
			: base(referenceItem)
		{
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x00037306 File Offset: 0x00036306
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}
	}
}
