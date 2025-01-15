using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000E6 RID: 230
	internal class AddDelegateRequest : DelegateManagementRequestBase<DelegateManagementResponse>
	{
		// Token: 0x06000BC3 RID: 3011 RVA: 0x00027D34 File Offset: 0x00026D34
		internal AddDelegateRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00027D48 File Offset: 0x00026D48
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParamCollection(this.DelegateUsers, "DelegateUsers");
			foreach (DelegateUser delegateUser in this.DelegateUsers)
			{
				delegateUser.ValidateUpdateDelegate();
			}
			if (this.MeetingRequestsDeliveryScope != null)
			{
				EwsUtilities.ValidateEnumVersionValue(this.MeetingRequestsDeliveryScope.Value, base.Service.RequestedServerVersion);
			}
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00027DE4 File Offset: 0x00026DE4
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

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00027E84 File Offset: 0x00026E84
		internal override string GetXmlElementName()
		{
			return "AddDelegate";
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00027E8B File Offset: 0x00026E8B
		internal override string GetResponseXmlElementName()
		{
			return "AddDelegateResponse";
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00027E92 File Offset: 0x00026E92
		internal override DelegateManagementResponse CreateResponse()
		{
			return new DelegateManagementResponse(true, this.delegateUsers);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00027EA0 File Offset: 0x00026EA0
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x00027EA3 File Offset: 0x00026EA3
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x00027EAB File Offset: 0x00026EAB
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

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x00027EB4 File Offset: 0x00026EB4
		public List<DelegateUser> DelegateUsers
		{
			get
			{
				return this.delegateUsers;
			}
		}

		// Token: 0x040008B0 RID: 2224
		private List<DelegateUser> delegateUsers = new List<DelegateUser>();

		// Token: 0x040008B1 RID: 2225
		private MeetingRequestsDeliveryScope? meetingRequestsDeliveryScope;
	}
}
