using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000057 RID: 87
	[EditorBrowsable(1)]
	public sealed class EmailAddressEntry : DictionaryEntryProperty<EmailAddressKey>
	{
		// Token: 0x060003D4 RID: 980 RVA: 0x0000DFFB File Offset: 0x0000CFFB
		internal EmailAddressEntry()
		{
			this.emailAddress = new EmailAddress();
			this.emailAddress.OnChange += this.EmailAddressChanged;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000E025 File Offset: 0x0000D025
		internal EmailAddressEntry(EmailAddressKey key, EmailAddress emailAddress)
			: base(key)
		{
			this.emailAddress = emailAddress;
			if (this.emailAddress != null)
			{
				this.emailAddress.OnChange += this.EmailAddressChanged;
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000E054 File Offset: 0x0000D054
		internal override void ReadAttributesFromXml(EwsServiceXmlReader reader)
		{
			base.ReadAttributesFromXml(reader);
			this.EmailAddress.Name = reader.ReadAttributeValue<string>("Name");
			this.EmailAddress.RoutingType = reader.ReadAttributeValue<string>("RoutingType");
			string text = reader.ReadAttributeValue("MailboxType");
			if (!string.IsNullOrEmpty(text))
			{
				this.EmailAddress.MailboxType = new MailboxType?(EwsUtilities.Parse<MailboxType>(text));
				return;
			}
			this.EmailAddress.MailboxType = default(MailboxType?);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000E0D3 File Offset: 0x0000D0D3
		internal override void ReadTextValueFromXml(EwsServiceXmlReader reader)
		{
			this.EmailAddress.Address = reader.ReadValue();
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000E0E8 File Offset: 0x0000D0E8
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "Key"))
					{
						if (!(text2 == "Name"))
						{
							if (!(text2 == "RoutingType"))
							{
								if (!(text2 == "MailboxType"))
								{
									if (text2 == "EmailAddress")
									{
										this.EmailAddress.Address = jsonProperty.ReadAsString(text);
									}
								}
								else
								{
									this.EmailAddress.MailboxType = new MailboxType?(jsonProperty.ReadEnumValue<MailboxType>(text));
								}
							}
							else
							{
								this.EmailAddress.RoutingType = jsonProperty.ReadAsString(text);
							}
						}
						else
						{
							this.EmailAddress.Name = jsonProperty.ReadAsString(text);
						}
					}
					else
					{
						base.Key = jsonProperty.ReadEnumValue<EmailAddressKey>(text);
					}
				}
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000E1E8 File Offset: 0x0000D1E8
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			base.WriteAttributesToXml(writer);
			if (writer.Service.RequestedServerVersion > ExchangeVersion.Exchange2007_SP1)
			{
				writer.WriteAttributeValue("Name", this.EmailAddress.Name);
				writer.WriteAttributeValue("RoutingType", this.EmailAddress.RoutingType);
				if (this.EmailAddress.MailboxType != MailboxType.Unknown)
				{
					writer.WriteAttributeValue("MailboxType", this.EmailAddress.MailboxType);
				}
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000E275 File Offset: 0x0000D275
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteValue(this.EmailAddress.Address, "EmailAddress");
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000E290 File Offset: 0x0000D290
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Key", base.Key);
			jsonObject.Add("Name", this.EmailAddress.Name);
			jsonObject.Add("RoutingType", this.EmailAddress.RoutingType);
			if (this.EmailAddress.MailboxType != null)
			{
				jsonObject.Add("MailboxType", this.EmailAddress.MailboxType.Value);
			}
			jsonObject.Add("EmailAddress", this.EmailAddress.Address);
			return jsonObject;
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000E334 File Offset: 0x0000D334
		// (set) Token: 0x060003DD RID: 989 RVA: 0x0000E33C File Offset: 0x0000D33C
		public EmailAddress EmailAddress
		{
			get
			{
				return this.emailAddress;
			}
			set
			{
				this.SetFieldValue<EmailAddress>(ref this.emailAddress, value);
				if (this.emailAddress != null)
				{
					this.emailAddress.OnChange += this.EmailAddressChanged;
				}
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000E36A File Offset: 0x0000D36A
		private void EmailAddressChanged(ComplexProperty complexProperty)
		{
			this.Changed();
		}

		// Token: 0x04000181 RID: 385
		private EmailAddress emailAddress;
	}
}
