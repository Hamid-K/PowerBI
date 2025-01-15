using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000058 RID: 88
	public sealed class EmailUserEntity : ComplexProperty
	{
		// Token: 0x060003DF RID: 991 RVA: 0x0000E372 File Offset: 0x0000D372
		internal EmailUserEntity()
		{
			base.Namespace = XmlNamespace.Types;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000E381 File Offset: 0x0000D381
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0000E389 File Offset: 0x0000D389
		public string Name { get; internal set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000E392 File Offset: 0x0000D392
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x0000E39A File Offset: 0x0000D39A
		public string UserId { get; internal set; }

		// Token: 0x060003E4 RID: 996 RVA: 0x0000E3A4 File Offset: 0x0000D3A4
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "Name")
				{
					this.Name = reader.ReadElementValue();
					return true;
				}
				if (localName == "UserId")
				{
					this.UserId = reader.ReadElementValue();
					return true;
				}
			}
			return base.TryReadElementFromXml(reader);
		}
	}
}
