using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000195 RID: 405
	internal interface ICalendarActionProvider
	{
		// Token: 0x06001251 RID: 4689
		CalendarActionResults Accept(bool sendResponse);

		// Token: 0x06001252 RID: 4690
		CalendarActionResults AcceptTentatively(bool sendResponse);

		// Token: 0x06001253 RID: 4691
		CalendarActionResults Decline(bool sendResponse);

		// Token: 0x06001254 RID: 4692
		AcceptMeetingInvitationMessage CreateAcceptMessage(bool tentative);

		// Token: 0x06001255 RID: 4693
		DeclineMeetingInvitationMessage CreateDeclineMessage();
	}
}
