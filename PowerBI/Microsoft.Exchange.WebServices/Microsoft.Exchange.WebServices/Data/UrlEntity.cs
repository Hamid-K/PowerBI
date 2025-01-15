using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000A3 RID: 163
	public sealed class UrlEntity : ExtractedEntity
	{
		// Token: 0x06000762 RID: 1890 RVA: 0x00019157 File Offset: 0x00018157
		internal UrlEntity()
		{
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0001915F File Offset: 0x0001815F
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x00019167 File Offset: 0x00018167
		public string Url { get; internal set; }

		// Token: 0x06000765 RID: 1893 RVA: 0x00019170 File Offset: 0x00018170
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null && localName == "Url")
			{
				this.Url = reader.ReadElementValue();
				return true;
			}
			return base.TryReadElementFromXml(reader);
		}
	}
}
