using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000040 RID: 64
	public sealed class ClientAppMetadata : ComplexProperty
	{
		// Token: 0x060002DE RID: 734 RVA: 0x0000B8F1 File Offset: 0x0000A8F1
		internal ClientAppMetadata()
		{
			base.Namespace = XmlNamespace.Types;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000B900 File Offset: 0x0000A900
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x0000B908 File Offset: 0x0000A908
		public string EndNodeUrl { get; private set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000B911 File Offset: 0x0000A911
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x0000B919 File Offset: 0x0000A919
		public string ActionUrl { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000B922 File Offset: 0x0000A922
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x0000B92A File Offset: 0x0000A92A
		public string AppStatus { get; private set; }

		// Token: 0x060002E5 RID: 741 RVA: 0x0000B934 File Offset: 0x0000A934
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "EndNodeUrl")
				{
					this.EndNodeUrl = reader.ReadElementValue<string>();
					return true;
				}
				if (localName == "ActionUrl")
				{
					this.ActionUrl = reader.ReadElementValue<string>();
					return true;
				}
				if (localName == "AppStatus")
				{
					this.AppStatus = reader.ReadElementValue<string>();
					return true;
				}
			}
			return false;
		}
	}
}
