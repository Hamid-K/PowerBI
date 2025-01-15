using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000080 RID: 128
	public sealed class PhoneEntity : ExtractedEntity
	{
		// Token: 0x060005D3 RID: 1491 RVA: 0x00014193 File Offset: 0x00013193
		internal PhoneEntity()
		{
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0001419B File Offset: 0x0001319B
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x000141A3 File Offset: 0x000131A3
		public string OriginalPhoneString { get; internal set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x000141AC File Offset: 0x000131AC
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x000141B4 File Offset: 0x000131B4
		public string PhoneString { get; internal set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x000141BD File Offset: 0x000131BD
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x000141C5 File Offset: 0x000131C5
		public string Type { get; internal set; }

		// Token: 0x060005DA RID: 1498 RVA: 0x000141D0 File Offset: 0x000131D0
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
