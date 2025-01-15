using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000044 RID: 68
	public sealed class ContactEntity : ExtractedEntity
	{
		// Token: 0x06000318 RID: 792 RVA: 0x0000C3AE File Offset: 0x0000B3AE
		internal ContactEntity()
		{
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000C3B6 File Offset: 0x0000B3B6
		// (set) Token: 0x0600031A RID: 794 RVA: 0x0000C3BE File Offset: 0x0000B3BE
		public string PersonName { get; internal set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000C3C7 File Offset: 0x0000B3C7
		// (set) Token: 0x0600031C RID: 796 RVA: 0x0000C3CF File Offset: 0x0000B3CF
		public string BusinessName { get; internal set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000C3D8 File Offset: 0x0000B3D8
		// (set) Token: 0x0600031E RID: 798 RVA: 0x0000C3E0 File Offset: 0x0000B3E0
		public ContactPhoneEntityCollection PhoneNumbers { get; internal set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000C3E9 File Offset: 0x0000B3E9
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0000C3F1 File Offset: 0x0000B3F1
		public StringList Urls { get; internal set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000C3FA File Offset: 0x0000B3FA
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0000C402 File Offset: 0x0000B402
		public StringList EmailAddresses { get; internal set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000C40B File Offset: 0x0000B40B
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000C413 File Offset: 0x0000B413
		public StringList Addresses { get; internal set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000C41C File Offset: 0x0000B41C
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0000C424 File Offset: 0x0000B424
		public string ContactString { get; internal set; }

		// Token: 0x06000327 RID: 807 RVA: 0x0000C430 File Offset: 0x0000B430
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60002d0-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(7);
					dictionary.Add("PersonName", 0);
					dictionary.Add("BusinessName", 1);
					dictionary.Add("PhoneNumbers", 2);
					dictionary.Add("Urls", 3);
					dictionary.Add("EmailAddresses", 4);
					dictionary.Add("Addresses", 5);
					dictionary.Add("ContactString", 6);
					<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60002d0-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60002d0-1.TryGetValue(localName, ref num))
				{
					switch (num)
					{
					case 0:
						this.PersonName = reader.ReadElementValue();
						return true;
					case 1:
						this.BusinessName = reader.ReadElementValue();
						return true;
					case 2:
						this.PhoneNumbers = new ContactPhoneEntityCollection();
						this.PhoneNumbers.LoadFromXml(reader, XmlNamespace.Types, "PhoneNumbers");
						return true;
					case 3:
						this.Urls = new StringList("Url");
						this.Urls.LoadFromXml(reader, XmlNamespace.Types, "Urls");
						return true;
					case 4:
						this.EmailAddresses = new StringList("EmailAddress");
						this.EmailAddresses.LoadFromXml(reader, XmlNamespace.Types, "EmailAddresses");
						return true;
					case 5:
						this.Addresses = new StringList("Address");
						this.Addresses.LoadFromXml(reader, XmlNamespace.Types, "Addresses");
						return true;
					case 6:
						this.ContactString = reader.ReadElementValue();
						return true;
					}
				}
			}
			return base.TryReadElementFromXml(reader);
		}
	}
}
