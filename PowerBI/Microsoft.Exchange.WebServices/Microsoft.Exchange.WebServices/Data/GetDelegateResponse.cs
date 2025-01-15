using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000164 RID: 356
	internal sealed class GetDelegateResponse : DelegateManagementResponse
	{
		// Token: 0x0600109E RID: 4254 RVA: 0x00030F25 File Offset: 0x0002FF25
		internal GetDelegateResponse(bool readDelegateUsers)
			: base(readDelegateUsers, null)
		{
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00030F36 File Offset: 0x0002FF36
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			if (base.ErrorCode == ServiceError.NoError)
			{
				if (base.DelegateUserResponses.Count > 0)
				{
					reader.Read();
				}
				if (reader.IsStartElement(XmlNamespace.Messages, "DeliverMeetingRequests"))
				{
					this.meetingRequestsDeliveryScope = reader.ReadElementValue<MeetingRequestsDeliveryScope>();
				}
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060010A0 RID: 4256 RVA: 0x00030F75 File Offset: 0x0002FF75
		internal MeetingRequestsDeliveryScope MeetingRequestsDeliveryScope
		{
			get
			{
				return this.meetingRequestsDeliveryScope;
			}
		}

		// Token: 0x040009B7 RID: 2487
		private MeetingRequestsDeliveryScope meetingRequestsDeliveryScope = MeetingRequestsDeliveryScope.NoForward;
	}
}
