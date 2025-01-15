using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000122 RID: 290
	internal class GetServerTimeZonesRequest : MultiResponseServiceRequest<GetServerTimeZonesResponse>
	{
		// Token: 0x06000E32 RID: 3634 RVA: 0x0002BB6E File Offset: 0x0002AB6E
		internal override void Validate()
		{
			base.Validate();
			if (this.ids != null)
			{
				EwsUtilities.ValidateParamCollection(this.ids, "Ids");
			}
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0002BB8E File Offset: 0x0002AB8E
		internal GetServerTimeZonesRequest(ExchangeService service)
			: base(service, ServiceErrorHandling.ThrowOnError)
		{
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0002BB98 File Offset: 0x0002AB98
		internal override GetServerTimeZonesResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new GetServerTimeZonesResponse();
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0002BB9F File Offset: 0x0002AB9F
		internal override string GetResponseMessageXmlElementName()
		{
			return "GetServerTimeZonesResponseMessage";
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0002BBA6 File Offset: 0x0002ABA6
		internal override int GetExpectedResponseMessageCount()
		{
			return 1;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0002BBA9 File Offset: 0x0002ABA9
		internal override string GetXmlElementName()
		{
			return "GetServerTimeZones";
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0002BBB0 File Offset: 0x0002ABB0
		internal override string GetResponseXmlElementName()
		{
			return "GetServerTimeZonesResponse";
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0002BBB7 File Offset: 0x0002ABB7
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2010;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0002BBBC File Offset: 0x0002ABBC
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (this.Ids != null)
			{
				writer.WriteStartElement(XmlNamespace.Messages, "Ids");
				foreach (string text in this.ids)
				{
					writer.WriteElementValue(XmlNamespace.Types, "Id", text);
				}
				writer.WriteEndElement();
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x0002BC2C File Offset: 0x0002AC2C
		// (set) Token: 0x06000E3C RID: 3644 RVA: 0x0002BC34 File Offset: 0x0002AC34
		internal IEnumerable<string> Ids
		{
			get
			{
				return this.ids;
			}
			set
			{
				this.ids = value;
			}
		}

		// Token: 0x0400091A RID: 2330
		private IEnumerable<string> ids;
	}
}
