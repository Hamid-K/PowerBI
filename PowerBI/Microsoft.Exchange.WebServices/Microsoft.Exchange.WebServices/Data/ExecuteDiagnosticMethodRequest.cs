using System;
using System.Xml;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000102 RID: 258
	internal sealed class ExecuteDiagnosticMethodRequest : MultiResponseServiceRequest<ExecuteDiagnosticMethodResponse>
	{
		// Token: 0x06000CDB RID: 3291 RVA: 0x000298DB File Offset: 0x000288DB
		internal ExecuteDiagnosticMethodRequest(ExchangeService service)
			: base(service, ServiceErrorHandling.ThrowOnError)
		{
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x000298E5 File Offset: 0x000288E5
		internal override string GetXmlElementName()
		{
			return "ExecuteDiagnosticMethod";
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x000298EC File Offset: 0x000288EC
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Messages, "Verb", this.Verb);
			writer.WriteStartElement(XmlNamespace.Messages, "Parameter");
			writer.WriteNode(this.Parameter);
			writer.WriteEndElement();
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0002991E File Offset: 0x0002891E
		internal override string GetResponseXmlElementName()
		{
			return "ExecuteDiagnosticMethodResponse";
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00029925 File Offset: 0x00028925
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00029928 File Offset: 0x00028928
		// (set) Token: 0x06000CE1 RID: 3297 RVA: 0x00029930 File Offset: 0x00028930
		internal string Verb { get; set; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00029939 File Offset: 0x00028939
		// (set) Token: 0x06000CE3 RID: 3299 RVA: 0x00029941 File Offset: 0x00028941
		internal XmlNode Parameter { get; set; }

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0002994A File Offset: 0x0002894A
		internal override ExecuteDiagnosticMethodResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new ExecuteDiagnosticMethodResponse(service);
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00029952 File Offset: 0x00028952
		internal override string GetResponseMessageXmlElementName()
		{
			return "ExecuteDiagnosticMethodResponseMessage";
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00029959 File Offset: 0x00028959
		internal override int GetExpectedResponseMessageCount()
		{
			return 1;
		}
	}
}
