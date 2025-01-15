using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200013F RID: 319
	internal class SubscribeToPushNotificationsRequest : SubscribeRequest<PushSubscription>
	{
		// Token: 0x06000F80 RID: 3968 RVA: 0x0002E0D3 File Offset: 0x0002D0D3
		internal SubscribeToPushNotificationsRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0002E0E4 File Offset: 0x0002D0E4
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.Url, "Url");
			if (this.Frequency < 1 || this.Frequency > 1440)
			{
				throw new ArgumentException(string.Format(Strings.InvalidFrequencyValue, this.Frequency));
			}
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0002E13D File Offset: 0x0002D13D
		internal override string GetSubscriptionXmlElementName()
		{
			return "PushSubscriptionRequest";
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0002E144 File Offset: 0x0002D144
		internal override void InternalWriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Types, "StatusFrequency", this.Frequency);
			writer.WriteElementValue(XmlNamespace.Types, "URL", this.Url.ToString());
			if (base.Service.RequestedServerVersion >= ExchangeVersion.Exchange2013 && !string.IsNullOrEmpty(this.callerData))
			{
				writer.WriteElementValue(XmlNamespace.Types, "CallerData", this.CallerData);
			}
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0002E1AC File Offset: 0x0002D1AC
		internal override void AddJsonProperties(JsonObject jsonSubscribeRequest, ExchangeService service)
		{
			jsonSubscribeRequest.Add("StatusFrequency", this.Frequency);
			jsonSubscribeRequest.Add("URL", this.Url.ToString());
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0002E1D5 File Offset: 0x0002D1D5
		internal override SubscribeResponse<PushSubscription> CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new SubscribeResponse<PushSubscription>(new PushSubscription(service));
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0002E1E2 File Offset: 0x0002D1E2
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0002E1E5 File Offset: 0x0002D1E5
		// (set) Token: 0x06000F88 RID: 3976 RVA: 0x0002E1ED File Offset: 0x0002D1ED
		public int Frequency
		{
			get
			{
				return this.frequency;
			}
			set
			{
				this.frequency = value;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x0002E1F6 File Offset: 0x0002D1F6
		// (set) Token: 0x06000F8A RID: 3978 RVA: 0x0002E1FE File Offset: 0x0002D1FE
		public Uri Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x0002E207 File Offset: 0x0002D207
		// (set) Token: 0x06000F8C RID: 3980 RVA: 0x0002E20F File Offset: 0x0002D20F
		public string CallerData
		{
			get
			{
				return this.callerData;
			}
			set
			{
				this.callerData = value;
			}
		}

		// Token: 0x0400096D RID: 2413
		private int frequency = 30;

		// Token: 0x0400096E RID: 2414
		private Uri url;

		// Token: 0x0400096F RID: 2415
		private string callerData;
	}
}
