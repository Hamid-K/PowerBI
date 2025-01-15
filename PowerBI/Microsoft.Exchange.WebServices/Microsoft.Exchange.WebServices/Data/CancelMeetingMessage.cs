using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001A5 RID: 421
	[ServiceObjectDefinition("CancelCalendarItem", ReturnedByServer = false)]
	public sealed class CancelMeetingMessage : CalendarResponseMessageBase<MeetingCancellation>
	{
		// Token: 0x06001448 RID: 5192 RVA: 0x000372C0 File Offset: 0x000362C0
		internal CancelMeetingMessage(Item referenceItem)
			: base(referenceItem)
		{
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x000372C9 File Offset: 0x000362C9
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x000372CC File Offset: 0x000362CC
		internal override ServiceObjectSchema GetSchema()
		{
			return CancelMeetingMessageSchema.Instance;
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x000372D3 File Offset: 0x000362D3
		// (set) Token: 0x0600144C RID: 5196 RVA: 0x000372EA File Offset: 0x000362EA
		public MessageBody Body
		{
			get
			{
				return (MessageBody)base.PropertyBag[CancelMeetingMessageSchema.Body];
			}
			set
			{
				base.PropertyBag[CancelMeetingMessageSchema.Body] = value;
			}
		}
	}
}
