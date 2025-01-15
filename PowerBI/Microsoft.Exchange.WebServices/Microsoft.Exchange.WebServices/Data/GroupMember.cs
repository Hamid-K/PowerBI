using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000066 RID: 102
	[RequiredServerVersion(ExchangeVersion.Exchange2010)]
	public class GroupMember : ComplexProperty
	{
		// Token: 0x060004AF RID: 1199 RVA: 0x0001142B File Offset: 0x0001042B
		public GroupMember()
		{
			this.key = null;
			this.status = MemberStatus.Unrecognized;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00011441 File Offset: 0x00010441
		public GroupMember(string smtpAddress)
			: this()
		{
			this.AddressInformation = new EmailAddress(smtpAddress);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00011458 File Offset: 0x00010458
		public GroupMember(string address, string routingType, MailboxType mailboxType)
			: this()
		{
			switch (mailboxType)
			{
			case MailboxType.OneOff:
			case MailboxType.Mailbox:
			case MailboxType.PublicFolder:
			case MailboxType.PublicGroup:
			case MailboxType.Contact:
				this.AddressInformation = new EmailAddress(null, address, routingType, mailboxType);
				return;
			}
			throw new ServiceLocalException(Strings.InvalidMailboxType);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000114AE File Offset: 0x000104AE
		public GroupMember(string smtpAddress, MailboxType mailboxType)
			: this(smtpAddress, "SMTP", mailboxType)
		{
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x000114BD File Offset: 0x000104BD
		public GroupMember(string name, string address, string routingType)
			: this()
		{
			this.AddressInformation = new EmailAddress(name, address, routingType, MailboxType.OneOff);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x000114D4 File Offset: 0x000104D4
		public GroupMember(string name, string smtpAddress)
			: this(name, smtpAddress, "SMTP")
		{
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x000114E3 File Offset: 0x000104E3
		public GroupMember(ItemId contactGroupId)
			: this()
		{
			this.AddressInformation = new EmailAddress(null, null, null, MailboxType.ContactGroup, contactGroupId);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000114FB File Offset: 0x000104FB
		public GroupMember(ItemId contactId, string addressToLink)
			: this()
		{
			this.AddressInformation = new EmailAddress(null, addressToLink, null, MailboxType.Contact, contactId);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00011513 File Offset: 0x00010513
		public GroupMember(EmailAddress addressInformation)
			: this()
		{
			this.AddressInformation = new EmailAddress(addressInformation);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00011527 File Offset: 0x00010527
		internal GroupMember(GroupMember member)
			: this()
		{
			EwsUtilities.ValidateParam(member, "member");
			this.AddressInformation = new EmailAddress(member.AddressInformation);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001154C File Offset: 0x0001054C
		public GroupMember(Contact contact, EmailAddressKey emailAddressKey)
			: this()
		{
			EwsUtilities.ValidateParam(contact, "contact");
			EmailAddress emailAddress = contact.EmailAddresses[emailAddressKey];
			this.AddressInformation = new EmailAddress(emailAddress);
			this.addressInformation.Id = contact.Id;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00011594 File Offset: 0x00010594
		public string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0001159C File Offset: 0x0001059C
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x000115A4 File Offset: 0x000105A4
		public EmailAddress AddressInformation
		{
			get
			{
				return this.addressInformation;
			}
			internal set
			{
				if (this.addressInformation != null)
				{
					this.addressInformation.OnChange -= this.AddressInformationChanged;
				}
				this.addressInformation = value;
				if (this.addressInformation != null)
				{
					this.addressInformation.OnChange += this.AddressInformationChanged;
				}
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x000115F6 File Offset: 0x000105F6
		public MemberStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000115FE File Offset: 0x000105FE
		internal override void ReadAttributesFromXml(EwsServiceXmlReader reader)
		{
			this.key = reader.ReadAttributeValue<string>("Key");
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00011614 File Offset: 0x00010614
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "Status")
				{
					this.status = EwsUtilities.Parse<MemberStatus>(reader.ReadElementValue());
					return true;
				}
				if (localName == "Mailbox")
				{
					this.AddressInformation = new EmailAddress();
					this.AddressInformation.LoadFromXml(reader, reader.LocalName);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001167C File Offset: 0x0001067C
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "Status"))
					{
						if (text2 == "Mailbox")
						{
							this.AddressInformation = new EmailAddress();
							this.AddressInformation.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
						}
					}
					else
					{
						this.status = jsonProperty.ReadEnumValue<MemberStatus>(text);
					}
				}
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00011718 File Offset: 0x00010718
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("Key", this.key);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001172B File Offset: 0x0001072B
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.AddressInformation.WriteToXml(writer, XmlNamespace.Types, "Mailbox");
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00011740 File Offset: 0x00010740
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Key", this.key);
			jsonObject.Add("Mailbox", this.AddressInformation.InternalToJson(service));
			return jsonObject;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001177C File Offset: 0x0001077C
		private void AddressInformationChanged(ComplexProperty complexProperty)
		{
			this.Changed();
		}

		// Token: 0x040001AC RID: 428
		private EmailAddress addressInformation;

		// Token: 0x040001AD RID: 429
		private MemberStatus status;

		// Token: 0x040001AE RID: 430
		private string key;
	}
}
