using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200002B RID: 43
	public sealed class AddressEntity : ExtractedEntity
	{
		// Token: 0x06000201 RID: 513 RVA: 0x00009400 File Offset: 0x00008400
		internal AddressEntity()
		{
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00009408 File Offset: 0x00008408
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00009410 File Offset: 0x00008410
		public string Address { get; internal set; }

		// Token: 0x06000204 RID: 516 RVA: 0x0000941C File Offset: 0x0000841C
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null && localName == "Address")
			{
				this.Address = reader.ReadElementValue();
				return true;
			}
			return base.TryReadElementFromXml(reader);
		}
	}
}
