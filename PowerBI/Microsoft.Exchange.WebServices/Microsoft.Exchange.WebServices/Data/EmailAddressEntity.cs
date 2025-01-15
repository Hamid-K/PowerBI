using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000055 RID: 85
	public sealed class EmailAddressEntity : ExtractedEntity
	{
		// Token: 0x060003CB RID: 971 RVA: 0x0000DF6D File Offset: 0x0000CF6D
		internal EmailAddressEntity()
		{
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000DF75 File Offset: 0x0000CF75
		// (set) Token: 0x060003CD RID: 973 RVA: 0x0000DF7D File Offset: 0x0000CF7D
		public string EmailAddress { get; internal set; }

		// Token: 0x060003CE RID: 974 RVA: 0x0000DF88 File Offset: 0x0000CF88
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null && localName == "EmailAddress")
			{
				this.EmailAddress = reader.ReadElementValue();
				return true;
			}
			return base.TryReadElementFromXml(reader);
		}
	}
}
