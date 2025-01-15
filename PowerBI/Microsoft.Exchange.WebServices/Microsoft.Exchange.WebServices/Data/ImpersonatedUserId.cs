using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000299 RID: 665
	public sealed class ImpersonatedUserId
	{
		// Token: 0x0600176E RID: 5998 RVA: 0x0003FB61 File Offset: 0x0003EB61
		public ImpersonatedUserId()
		{
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0003FB69 File Offset: 0x0003EB69
		public ImpersonatedUserId(ConnectingIdType idType, string id)
			: this()
		{
			this.idType = idType;
			this.id = id;
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0003FB80 File Offset: 0x0003EB80
		internal void WriteToXml(EwsServiceXmlWriter writer)
		{
			if (string.IsNullOrEmpty(this.id))
			{
				throw new ArgumentException(Strings.IdPropertyMustBeSet);
			}
			writer.WriteStartElement(XmlNamespace.Types, "ExchangeImpersonation");
			writer.WriteStartElement(XmlNamespace.Types, "ConnectingSID");
			string text = ((this.idType == ConnectingIdType.SmtpAddress && writer.Service.RequestedServerVersion == ExchangeVersion.Exchange2007_SP1) ? "PrimarySmtpAddress" : this.IdType.ToString());
			writer.WriteElementValue(XmlNamespace.Types, text, this.id);
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x0003FC0A File Offset: 0x0003EC0A
		// (set) Token: 0x06001772 RID: 6002 RVA: 0x0003FC12 File Offset: 0x0003EC12
		public ConnectingIdType IdType
		{
			get
			{
				return this.idType;
			}
			set
			{
				this.idType = value;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x0003FC1B File Offset: 0x0003EC1B
		// (set) Token: 0x06001774 RID: 6004 RVA: 0x0003FC23 File Offset: 0x0003EC23
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x04001356 RID: 4950
		private ConnectingIdType idType;

		// Token: 0x04001357 RID: 4951
		private string id;
	}
}
