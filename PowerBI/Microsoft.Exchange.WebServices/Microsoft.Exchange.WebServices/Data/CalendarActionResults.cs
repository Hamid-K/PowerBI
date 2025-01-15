using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200028A RID: 650
	public sealed class CalendarActionResults
	{
		// Token: 0x06001702 RID: 5890 RVA: 0x0003ED72 File Offset: 0x0003DD72
		internal CalendarActionResults(IEnumerable<Item> items)
		{
			this.appointment = EwsUtilities.FindFirstItemOfType<Appointment>(items);
			this.meetingRequest = EwsUtilities.FindFirstItemOfType<MeetingRequest>(items);
			this.meetingResponse = EwsUtilities.FindFirstItemOfType<MeetingResponse>(items);
			this.meetingCancellation = EwsUtilities.FindFirstItemOfType<MeetingCancellation>(items);
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x0003EDAA File Offset: 0x0003DDAA
		public Appointment Appointment
		{
			get
			{
				return this.appointment;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x0003EDB2 File Offset: 0x0003DDB2
		public MeetingRequest MeetingRequest
		{
			get
			{
				return this.meetingRequest;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x0003EDBA File Offset: 0x0003DDBA
		public MeetingResponse MeetingResponse
		{
			get
			{
				return this.meetingResponse;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x0003EDC2 File Offset: 0x0003DDC2
		public MeetingCancellation MeetingCancellation
		{
			get
			{
				return this.meetingCancellation;
			}
		}

		// Token: 0x04001339 RID: 4921
		private Appointment appointment;

		// Token: 0x0400133A RID: 4922
		private MeetingRequest meetingRequest;

		// Token: 0x0400133B RID: 4923
		private MeetingResponse meetingResponse;

		// Token: 0x0400133C RID: 4924
		private MeetingCancellation meetingCancellation;
	}
}
