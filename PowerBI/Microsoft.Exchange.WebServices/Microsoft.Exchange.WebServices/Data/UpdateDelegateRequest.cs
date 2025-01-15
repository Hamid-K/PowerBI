using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000146 RID: 326
	internal class UpdateDelegateRequest : DelegateManagementRequestBase<DelegateManagementResponse>
	{
		// Token: 0x06000FD5 RID: 4053 RVA: 0x0002E825 File Offset: 0x0002D825
		internal UpdateDelegateRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0002E83C File Offset: 0x0002D83C
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParamCollection(this.DelegateUsers, "DelegateUsers");
			foreach (DelegateUser delegateUser in this.DelegateUsers)
			{
				delegateUser.ValidateUpdateDelegate();
			}
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0002E8A4 File Offset: 0x0002D8A4
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			base.WriteElementsToXml(writer);
			writer.WriteStartElement(XmlNamespace.Messages, "DelegateUsers");
			foreach (DelegateUser delegateUser in this.DelegateUsers)
			{
				delegateUser.WriteToXml(writer, "DelegateUser");
			}
			writer.WriteEndElement();
			if (this.MeetingRequestsDeliveryScope != null)
			{
				writer.WriteElementValue(XmlNamespace.Messages, "DeliverMeetingRequests", this.MeetingRequestsDeliveryScope.Value);
			}
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0002E944 File Offset: 0x0002D944
		internal override string GetResponseXmlElementName()
		{
			return "UpdateDelegateResponse";
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0002E94B File Offset: 0x0002D94B
		internal override DelegateManagementResponse CreateResponse()
		{
			return new DelegateManagementResponse(true, this.delegateUsers);
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0002E959 File Offset: 0x0002D959
		internal override string GetXmlElementName()
		{
			return "UpdateDelegate";
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0002E960 File Offset: 0x0002D960
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x0002E963 File Offset: 0x0002D963
		// (set) Token: 0x06000FDD RID: 4061 RVA: 0x0002E96B File Offset: 0x0002D96B
		public MeetingRequestsDeliveryScope? MeetingRequestsDeliveryScope
		{
			get
			{
				return this.meetingRequestsDeliveryScope;
			}
			set
			{
				this.meetingRequestsDeliveryScope = value;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x0002E974 File Offset: 0x0002D974
		public List<DelegateUser> DelegateUsers
		{
			get
			{
				return this.delegateUsers;
			}
		}

		// Token: 0x0400097C RID: 2428
		private List<DelegateUser> delegateUsers = new List<DelegateUser>();

		// Token: 0x0400097D RID: 2429
		private MeetingRequestsDeliveryScope? meetingRequestsDeliveryScope;
	}
}
