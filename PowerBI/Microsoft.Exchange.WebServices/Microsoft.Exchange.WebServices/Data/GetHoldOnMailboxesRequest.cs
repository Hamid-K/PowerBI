using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000116 RID: 278
	internal sealed class GetHoldOnMailboxesRequest : SimpleServiceRequestBase, IJsonSerializable
	{
		// Token: 0x06000DBE RID: 3518 RVA: 0x0002B326 File Offset: 0x0002A326
		internal GetHoldOnMailboxesRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0002B32F File Offset: 0x0002A32F
		internal override string GetResponseXmlElementName()
		{
			return "GetHoldOnMailboxesResponse";
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0002B336 File Offset: 0x0002A336
		internal override string GetXmlElementName()
		{
			return "GetHoldOnMailboxes";
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0002B33D File Offset: 0x0002A33D
		internal override void Validate()
		{
			base.Validate();
			if (string.IsNullOrEmpty(this.HoldId))
			{
				throw new ServiceValidationException(Strings.HoldIdParameterIsNotSpecified);
			}
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0002B364 File Offset: 0x0002A364
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			GetHoldOnMailboxesResponse getHoldOnMailboxesResponse = new GetHoldOnMailboxesResponse();
			getHoldOnMailboxesResponse.LoadFromXml(reader, this.GetResponseXmlElementName());
			return getHoldOnMailboxesResponse;
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0002B385 File Offset: 0x0002A385
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Messages, "HoldId", this.HoldId);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0002B399 File Offset: 0x0002A399
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0002B39C File Offset: 0x0002A39C
		internal GetHoldOnMailboxesResponse Execute()
		{
			return (GetHoldOnMailboxesResponse)base.InternalExecute();
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0002B3B8 File Offset: 0x0002A3B8
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			return new JsonObject();
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x0002B3CC File Offset: 0x0002A3CC
		// (set) Token: 0x06000DC8 RID: 3528 RVA: 0x0002B3D4 File Offset: 0x0002A3D4
		public string HoldId { get; set; }
	}
}
