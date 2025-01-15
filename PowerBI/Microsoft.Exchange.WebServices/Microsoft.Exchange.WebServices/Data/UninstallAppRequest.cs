using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000143 RID: 323
	internal sealed class UninstallAppRequest : SimpleServiceRequestBase
	{
		// Token: 0x06000FB9 RID: 4025 RVA: 0x0002E686 File Offset: 0x0002D686
		internal UninstallAppRequest(ExchangeService service, string id)
			: base(service)
		{
			this.ID = id;
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0002E696 File Offset: 0x0002D696
		internal override string GetXmlElementName()
		{
			return "UninstallApp";
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0002E69D File Offset: 0x0002D69D
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Messages, "ID", this.ID);
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0002E6B1 File Offset: 0x0002D6B1
		internal override string GetResponseXmlElementName()
		{
			return "UninstallAppResponse";
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0002E6B8 File Offset: 0x0002D6B8
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			UninstallAppResponse uninstallAppResponse = new UninstallAppResponse();
			uninstallAppResponse.LoadFromXml(reader, "UninstallAppResponse");
			return uninstallAppResponse;
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0002E6D8 File Offset: 0x0002D6D8
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0002E6DC File Offset: 0x0002D6DC
		internal UninstallAppResponse Execute()
		{
			UninstallAppResponse uninstallAppResponse = (UninstallAppResponse)base.InternalExecute();
			uninstallAppResponse.ThrowIfNecessary();
			return uninstallAppResponse;
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x0002E6FC File Offset: 0x0002D6FC
		// (set) Token: 0x06000FC1 RID: 4033 RVA: 0x0002E704 File Offset: 0x0002D704
		private string ID { get; set; }
	}
}
