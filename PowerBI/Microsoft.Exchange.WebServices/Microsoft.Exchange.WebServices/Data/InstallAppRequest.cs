using System;
using System.IO;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200012D RID: 301
	internal sealed class InstallAppRequest : SimpleServiceRequestBase
	{
		// Token: 0x06000E96 RID: 3734 RVA: 0x0002C69C File Offset: 0x0002B69C
		internal InstallAppRequest(ExchangeService service, Stream manifestStream)
			: base(service)
		{
			this.manifestStream = manifestStream;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0002C6AC File Offset: 0x0002B6AC
		internal override string GetXmlElementName()
		{
			return "InstallApp";
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0002C6B3 File Offset: 0x0002B6B3
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Messages, "Manifest");
			writer.WriteBase64ElementValue(this.manifestStream);
			writer.WriteEndElement();
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0002C6D3 File Offset: 0x0002B6D3
		internal override string GetResponseXmlElementName()
		{
			return "InstallAppResponse";
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0002C6DC File Offset: 0x0002B6DC
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			InstallAppResponse installAppResponse = new InstallAppResponse();
			installAppResponse.LoadFromXml(reader, "InstallAppResponse");
			return installAppResponse;
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0002C6FC File Offset: 0x0002B6FC
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0002C700 File Offset: 0x0002B700
		internal InstallAppResponse Execute()
		{
			InstallAppResponse installAppResponse = (InstallAppResponse)base.InternalExecute();
			installAppResponse.ThrowIfNecessary();
			return installAppResponse;
		}

		// Token: 0x04000939 RID: 2361
		private readonly Stream manifestStream;
	}
}
