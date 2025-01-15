using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200013E RID: 318
	internal class SubscribeToPullNotificationsRequest : SubscribeRequest<PullSubscription>
	{
		// Token: 0x06000F77 RID: 3959 RVA: 0x0002E030 File Offset: 0x0002D030
		internal SubscribeToPullNotificationsRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0002E041 File Offset: 0x0002D041
		internal override void Validate()
		{
			base.Validate();
			if (this.Timeout < 1 || this.Timeout > 1440)
			{
				throw new ArgumentException(string.Format(Strings.InvalidTimeoutValue, this.Timeout));
			}
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0002E07F File Offset: 0x0002D07F
		internal override SubscribeResponse<PullSubscription> CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new SubscribeResponse<PullSubscription>(new PullSubscription(service));
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0002E08C File Offset: 0x0002D08C
		internal override string GetSubscriptionXmlElementName()
		{
			return "PullSubscriptionRequest";
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0002E093 File Offset: 0x0002D093
		internal override void InternalWriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Types, "Timeout", this.Timeout);
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0002E0AC File Offset: 0x0002D0AC
		internal override void AddJsonProperties(JsonObject jsonSubscribeRequest, ExchangeService service)
		{
			jsonSubscribeRequest.Add("Timeout", this.Timeout);
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0002E0BF File Offset: 0x0002D0BF
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x0002E0C2 File Offset: 0x0002D0C2
		// (set) Token: 0x06000F7F RID: 3967 RVA: 0x0002E0CA File Offset: 0x0002D0CA
		public int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				this.timeout = value;
			}
		}

		// Token: 0x0400096C RID: 2412
		private int timeout = 30;
	}
}
