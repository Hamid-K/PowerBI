using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001A4 RID: 420
	public sealed class AcceptMeetingInvitationMessage : CalendarResponseMessage<MeetingResponse>
	{
		// Token: 0x06001444 RID: 5188 RVA: 0x00037290 File Offset: 0x00036290
		internal AcceptMeetingInvitationMessage(Item referenceItem, bool tentative)
			: base(referenceItem)
		{
			this.tentative = tentative;
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x000372A0 File Offset: 0x000362A0
		internal override string GetXmlElementNameOverride()
		{
			if (this.tentative)
			{
				return "TentativelyAcceptItem";
			}
			return "AcceptItem";
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x000372B5 File Offset: 0x000362B5
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x000372B8 File Offset: 0x000362B8
		public bool Tentative
		{
			get
			{
				return this.tentative;
			}
		}

		// Token: 0x04000A0B RID: 2571
		private bool tentative;
	}
}
