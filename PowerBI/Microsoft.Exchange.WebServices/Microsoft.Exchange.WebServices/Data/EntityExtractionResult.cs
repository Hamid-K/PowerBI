using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200005B RID: 91
	public sealed class EntityExtractionResult : ComplexProperty
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x0000E755 File Offset: 0x0000D755
		internal EntityExtractionResult()
		{
			base.Namespace = XmlNamespace.Types;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000E764 File Offset: 0x0000D764
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x0000E76C File Offset: 0x0000D76C
		public AddressEntityCollection Addresses { get; internal set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000E775 File Offset: 0x0000D775
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x0000E77D File Offset: 0x0000D77D
		public MeetingSuggestionCollection MeetingSuggestions { get; internal set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000E786 File Offset: 0x0000D786
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x0000E78E File Offset: 0x0000D78E
		public TaskSuggestionCollection TaskSuggestions { get; internal set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000E797 File Offset: 0x0000D797
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x0000E79F File Offset: 0x0000D79F
		public EmailAddressEntityCollection EmailAddresses { get; internal set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0000E7A8 File Offset: 0x0000D7A8
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x0000E7B0 File Offset: 0x0000D7B0
		public ContactEntityCollection Contacts { get; internal set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000E7B9 File Offset: 0x0000D7B9
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x0000E7C1 File Offset: 0x0000D7C1
		public UrlEntityCollection Urls { get; internal set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000E7CA File Offset: 0x0000D7CA
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x0000E7D2 File Offset: 0x0000D7D2
		public PhoneEntityCollection PhoneNumbers { get; internal set; }

		// Token: 0x06000409 RID: 1033 RVA: 0x0000E7DC File Offset: 0x0000D7DC
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60003b0-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(7);
					dictionary.Add("Addresses", 0);
					dictionary.Add("MeetingSuggestions", 1);
					dictionary.Add("TaskSuggestions", 2);
					dictionary.Add("EmailAddresses", 3);
					dictionary.Add("Contacts", 4);
					dictionary.Add("Urls", 5);
					dictionary.Add("PhoneNumbers", 6);
					<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60003b0-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60003b0-1.TryGetValue(localName, ref num))
				{
					switch (num)
					{
					case 0:
						this.Addresses = new AddressEntityCollection();
						this.Addresses.LoadFromXml(reader, XmlNamespace.Types, "Addresses");
						return true;
					case 1:
						this.MeetingSuggestions = new MeetingSuggestionCollection();
						this.MeetingSuggestions.LoadFromXml(reader, XmlNamespace.Types, "MeetingSuggestions");
						return true;
					case 2:
						this.TaskSuggestions = new TaskSuggestionCollection();
						this.TaskSuggestions.LoadFromXml(reader, XmlNamespace.Types, "TaskSuggestions");
						return true;
					case 3:
						this.EmailAddresses = new EmailAddressEntityCollection();
						this.EmailAddresses.LoadFromXml(reader, XmlNamespace.Types, "EmailAddresses");
						return true;
					case 4:
						this.Contacts = new ContactEntityCollection();
						this.Contacts.LoadFromXml(reader, XmlNamespace.Types, "Contacts");
						return true;
					case 5:
						this.Urls = new UrlEntityCollection();
						this.Urls.LoadFromXml(reader, XmlNamespace.Types, "Urls");
						return true;
					case 6:
						this.PhoneNumbers = new PhoneEntityCollection();
						this.PhoneNumbers.LoadFromXml(reader, XmlNamespace.Types, "PhoneNumbers");
						return true;
					}
				}
			}
			return base.TryReadElementFromXml(reader);
		}
	}
}
