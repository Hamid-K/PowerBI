using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000FC RID: 252
	internal sealed class DeleteItemRequest : DeleteRequest<ServiceResponse>
	{
		// Token: 0x06000C99 RID: 3225 RVA: 0x000293C0 File Offset: 0x000283C0
		internal DeleteItemRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x000293D8 File Offset: 0x000283D8
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.ItemIds, "ItemIds");
			if (this.SuppressReadReceipts && base.Service.RequestedServerVersion < ExchangeVersion.Exchange2013)
			{
				throw new ServiceVersionException(string.Format(Strings.ParameterIncompatibleWithRequestVersion, "SuppressReadReceipts", ExchangeVersion.Exchange2013));
			}
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00029431 File Offset: 0x00028431
		internal override int GetExpectedResponseMessageCount()
		{
			return this.itemIds.Count;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0002943E File Offset: 0x0002843E
		internal override ServiceResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new ServiceResponse();
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00029445 File Offset: 0x00028445
		internal override string GetXmlElementName()
		{
			return "DeleteItem";
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0002944C File Offset: 0x0002844C
		internal override string GetResponseXmlElementName()
		{
			return "DeleteItemResponse";
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00029453 File Offset: 0x00028453
		internal override string GetResponseMessageXmlElementName()
		{
			return "DeleteItemResponseMessage";
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0002945C File Offset: 0x0002845C
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			base.WriteAttributesToXml(writer);
			if (this.AffectedTaskOccurrences != null)
			{
				writer.WriteAttributeValue("AffectedTaskOccurrences", this.AffectedTaskOccurrences.Value);
			}
			if (this.SendCancellationsMode != null)
			{
				writer.WriteAttributeValue("SendMeetingCancellations", this.SendCancellationsMode.Value);
			}
			if (this.SuppressReadReceipts)
			{
				writer.WriteAttributeValue("SuppressReadReceipts", true);
			}
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x000294E5 File Offset: 0x000284E5
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.itemIds.WriteToXml(writer, XmlNamespace.Messages, "ItemIds");
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x000294FC File Offset: 0x000284FC
		protected override void InternalToJson(JsonObject body)
		{
			if (this.AffectedTaskOccurrences != null)
			{
				body.Add("AffectedTaskOccurrences", this.AffectedTaskOccurrences.Value);
			}
			if (this.SendCancellationsMode != null)
			{
				body.Add("SendMeetingCancellations", this.SendCancellationsMode.Value);
			}
			if (this.SuppressReadReceipts)
			{
				body.Add("SuppressReadReceipts", true);
			}
			if (this.ItemIds.Count > 0)
			{
				body.Add("ItemIds", this.ItemIds.InternalToJson(base.Service));
			}
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x000295A3 File Offset: 0x000285A3
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x000295A6 File Offset: 0x000285A6
		internal ItemIdWrapperList ItemIds
		{
			get
			{
				return this.itemIds;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x000295AE File Offset: 0x000285AE
		// (set) Token: 0x06000CA6 RID: 3238 RVA: 0x000295B6 File Offset: 0x000285B6
		internal AffectedTaskOccurrence? AffectedTaskOccurrences
		{
			get
			{
				return this.affectedTaskOccurrences;
			}
			set
			{
				this.affectedTaskOccurrences = value;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x000295BF File Offset: 0x000285BF
		// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x000295C7 File Offset: 0x000285C7
		internal SendCancellationsMode? SendCancellationsMode
		{
			get
			{
				return this.sendCancellationsMode;
			}
			set
			{
				this.sendCancellationsMode = value;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x000295D0 File Offset: 0x000285D0
		// (set) Token: 0x06000CAA RID: 3242 RVA: 0x000295D8 File Offset: 0x000285D8
		public bool SuppressReadReceipts { get; set; }

		// Token: 0x040008CE RID: 2254
		private ItemIdWrapperList itemIds = new ItemIdWrapperList();

		// Token: 0x040008CF RID: 2255
		private AffectedTaskOccurrence? affectedTaskOccurrences;

		// Token: 0x040008D0 RID: 2256
		private SendCancellationsMode? sendCancellationsMode;
	}
}
