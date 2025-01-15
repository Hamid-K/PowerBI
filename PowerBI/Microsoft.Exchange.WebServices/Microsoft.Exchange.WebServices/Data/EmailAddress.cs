using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200003A RID: 58
	public class EmailAddress : ComplexProperty, ISearchStringProvider
	{
		// Token: 0x0600029C RID: 668 RVA: 0x0000ADE4 File Offset: 0x00009DE4
		public EmailAddress()
		{
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000ADEC File Offset: 0x00009DEC
		public EmailAddress(string smtpAddress)
			: this()
		{
			this.address = smtpAddress;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000ADFB File Offset: 0x00009DFB
		public EmailAddress(string name, string smtpAddress)
			: this(smtpAddress)
		{
			this.name = name;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000AE0B File Offset: 0x00009E0B
		public EmailAddress(string name, string address, string routingType)
			: this(name, address)
		{
			this.routingType = routingType;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000AE1C File Offset: 0x00009E1C
		internal EmailAddress(string name, string address, string routingType, MailboxType mailboxType)
			: this(name, address, routingType)
		{
			this.mailboxType = new MailboxType?(mailboxType);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000AE34 File Offset: 0x00009E34
		internal EmailAddress(string name, string address, string routingType, MailboxType mailboxType, ItemId itemId)
			: this(name, address, routingType)
		{
			this.mailboxType = new MailboxType?(mailboxType);
			this.id = itemId;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000AE54 File Offset: 0x00009E54
		internal EmailAddress(EmailAddress mailbox)
			: this()
		{
			EwsUtilities.ValidateParam(mailbox, "mailbox");
			this.Name = mailbox.Name;
			this.Address = mailbox.Address;
			this.RoutingType = mailbox.RoutingType;
			this.MailboxType = mailbox.MailboxType;
			this.Id = mailbox.Id;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000AEAE File Offset: 0x00009EAE
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000AEB6 File Offset: 0x00009EB6
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.SetFieldValue<string>(ref this.name, value);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000AEC5 File Offset: 0x00009EC5
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000AECD File Offset: 0x00009ECD
		public string Address
		{
			get
			{
				return this.address;
			}
			set
			{
				this.SetFieldValue<string>(ref this.address, value);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000AEDC File Offset: 0x00009EDC
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000AEE4 File Offset: 0x00009EE4
		public string RoutingType
		{
			get
			{
				return this.routingType;
			}
			set
			{
				this.SetFieldValue<string>(ref this.routingType, value);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000AEF3 File Offset: 0x00009EF3
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000AEFB File Offset: 0x00009EFB
		public MailboxType? MailboxType
		{
			get
			{
				return this.mailboxType;
			}
			set
			{
				this.SetFieldValue<MailboxType?>(ref this.mailboxType, value);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000AF0A File Offset: 0x00009F0A
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000AF12 File Offset: 0x00009F12
		public ItemId Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.SetFieldValue<ItemId>(ref this.id, value);
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000AF21 File Offset: 0x00009F21
		public static implicit operator EmailAddress(string smtpAddress)
		{
			return new EmailAddress(smtpAddress);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000AF2C File Offset: 0x00009F2C
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "Name")
				{
					this.name = reader.ReadElementValue();
					return true;
				}
				if (localName == "EmailAddress")
				{
					this.address = reader.ReadElementValue();
					return true;
				}
				if (localName == "RoutingType")
				{
					this.routingType = reader.ReadElementValue();
					return true;
				}
				if (localName == "MailboxType")
				{
					this.mailboxType = new MailboxType?(reader.ReadElementValue<MailboxType>());
					return true;
				}
				if (localName == "ItemId")
				{
					this.id = new ItemId();
					this.id.LoadFromXml(reader, reader.LocalName);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000AFE8 File Offset: 0x00009FE8
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "Name"))
					{
						if (!(text2 == "EmailAddress"))
						{
							if (!(text2 == "RoutingType"))
							{
								if (!(text2 == "MailboxType"))
								{
									if (text2 == "ItemId")
									{
										this.id = new ItemId();
										this.id.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
									}
								}
								else
								{
									this.mailboxType = new MailboxType?(jsonProperty.ReadEnumValue<MailboxType>(text));
								}
							}
							else
							{
								this.routingType = jsonProperty.ReadAsString(text);
							}
						}
						else
						{
							this.address = jsonProperty.ReadAsString(text);
						}
					}
					else
					{
						this.name = jsonProperty.ReadAsString(text);
					}
				}
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000B0E4 File Offset: 0x0000A0E4
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Types, "Name", this.Name);
			writer.WriteElementValue(XmlNamespace.Types, "EmailAddress", this.Address);
			writer.WriteElementValue(XmlNamespace.Types, "RoutingType", this.RoutingType);
			writer.WriteElementValue(XmlNamespace.Types, "MailboxType", this.MailboxType);
			if (this.Id != null)
			{
				this.Id.WriteToXml(writer, "ItemId");
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000B158 File Offset: 0x0000A158
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Name", this.Name);
			jsonObject.Add("EmailAddress", this.Address);
			jsonObject.Add("RoutingType", this.RoutingType);
			jsonObject.Add("MailboxType", (Enum)this.MailboxType);
			if (this.Id != null)
			{
				jsonObject.Add("ItemId", this.Id.InternalToJson(service));
			}
			return jsonObject;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000B1D9 File Offset: 0x0000A1D9
		string ISearchStringProvider.GetSearchString()
		{
			return this.Address;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000B1E4 File Offset: 0x0000A1E4
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.Address))
			{
				return string.Empty;
			}
			string text;
			if (!string.IsNullOrEmpty(this.RoutingType))
			{
				text = this.RoutingType + ":" + this.Address;
			}
			else
			{
				text = this.Address;
			}
			if (!string.IsNullOrEmpty(this.Name))
			{
				return this.Name + " <" + text + ">";
			}
			return text;
		}

		// Token: 0x0400012D RID: 301
		internal const string SmtpRoutingType = "SMTP";

		// Token: 0x0400012E RID: 302
		private string name;

		// Token: 0x0400012F RID: 303
		private string address;

		// Token: 0x04000130 RID: 304
		private string routingType;

		// Token: 0x04000131 RID: 305
		private MailboxType? mailboxType;

		// Token: 0x04000132 RID: 306
		private ItemId id;
	}
}
