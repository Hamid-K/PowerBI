using System;
using System.Xml;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000159 RID: 345
	internal sealed class ExecuteDiagnosticMethodResponse : ServiceResponse
	{
		// Token: 0x0600105F RID: 4191 RVA: 0x0002FDD9 File Offset: 0x0002EDD9
		internal ExecuteDiagnosticMethodResponse(ExchangeService service)
		{
			EwsUtilities.Assert(service != null, "ExecuteDiagnosticMethodResponse.ctor", "service is null");
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0002FDF8 File Offset: 0x0002EDF8
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			reader.ReadStartElement(XmlNamespace.Messages, "ReturnValue");
			using (XmlReader xmlReaderForNode = reader.GetXmlReaderForNode())
			{
				this.ReturnValue = new SafeXmlDocument();
				this.ReturnValue.Load(xmlReaderForNode);
			}
			reader.SkipCurrentElement();
			reader.ReadEndElementIfNecessary(XmlNamespace.Messages, "ReturnValue");
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x0002FE60 File Offset: 0x0002EE60
		// (set) Token: 0x06001062 RID: 4194 RVA: 0x0002FE68 File Offset: 0x0002EE68
		internal XmlDocument ReturnValue { get; private set; }
	}
}
