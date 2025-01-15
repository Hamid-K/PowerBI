using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002A1 RID: 673
	public sealed class NameResolution
	{
		// Token: 0x060017C2 RID: 6082 RVA: 0x00040D02 File Offset: 0x0003FD02
		internal NameResolution(NameResolutionCollection owner)
		{
			EwsUtilities.Assert(owner != null, "NameResolution.ctor", "owner is null.");
			this.owner = owner;
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x00040D34 File Offset: 0x0003FD34
		internal void LoadFromXml(EwsServiceXmlReader reader)
		{
			reader.ReadStartElement(XmlNamespace.Types, "Resolution");
			reader.ReadStartElement(XmlNamespace.Types, "Mailbox");
			this.mailbox.LoadFromXml(reader, "Mailbox");
			reader.Read();
			if (reader.IsStartElement(XmlNamespace.Types, "Contact"))
			{
				this.contact = new Contact(this.owner.Session);
				this.contact.LoadFromXml(reader, true, PropertySet.FirstClassProperties, false);
				reader.ReadEndElement(XmlNamespace.Types, "Resolution");
				return;
			}
			reader.EnsureCurrentNodeIsEndElement(XmlNamespace.Types, "Resolution");
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00040DC0 File Offset: 0x0003FDC0
		internal void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "Mailbox"))
					{
						if (text2 == "Contact")
						{
							this.contact = new Contact(this.owner.Session);
							this.contact.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service, true, PropertySet.FirstClassProperties, false);
						}
					}
					else
					{
						this.mailbox.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
					}
				}
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x00040E74 File Offset: 0x0003FE74
		public EmailAddress Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x00040E7C File Offset: 0x0003FE7C
		public Contact Contact
		{
			get
			{
				return this.contact;
			}
		}

		// Token: 0x0400137C RID: 4988
		private NameResolutionCollection owner;

		// Token: 0x0400137D RID: 4989
		private EmailAddress mailbox = new EmailAddress();

		// Token: 0x0400137E RID: 4990
		private Contact contact;
	}
}
