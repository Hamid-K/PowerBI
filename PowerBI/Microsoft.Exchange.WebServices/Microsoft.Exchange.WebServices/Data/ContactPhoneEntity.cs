using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000046 RID: 70
	public sealed class ContactPhoneEntity : ComplexProperty
	{
		// Token: 0x0600032D RID: 813 RVA: 0x0000C5E5 File Offset: 0x0000B5E5
		internal ContactPhoneEntity()
		{
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000C5ED File Offset: 0x0000B5ED
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0000C5F5 File Offset: 0x0000B5F5
		public string OriginalPhoneString { get; internal set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000C5FE File Offset: 0x0000B5FE
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000C606 File Offset: 0x0000B606
		public string PhoneString { get; internal set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000C60F File Offset: 0x0000B60F
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000C617 File Offset: 0x0000B617
		public string Type { get; internal set; }

		// Token: 0x06000334 RID: 820 RVA: 0x0000C620 File Offset: 0x0000B620
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "OriginalPhoneString")
				{
					this.OriginalPhoneString = reader.ReadElementValue();
					return true;
				}
				if (localName == "PhoneString")
				{
					this.PhoneString = reader.ReadElementValue();
					return true;
				}
				if (localName == "Type")
				{
					this.Type = reader.ReadElementValue();
					return true;
				}
			}
			return base.TryReadElementFromXml(reader);
		}
	}
}
