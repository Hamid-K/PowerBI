using System;
using System.IO;
using System.Xml;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200003F RID: 63
	public sealed class ClientApp : ComplexProperty
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x0000B80C File Offset: 0x0000A80C
		internal ClientApp()
		{
			base.Namespace = XmlNamespace.Types;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000B81B File Offset: 0x0000A81B
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x0000B823 File Offset: 0x0000A823
		public XmlDocument Manifest { get; internal set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000B82C File Offset: 0x0000A82C
		// (set) Token: 0x060002DB RID: 731 RVA: 0x0000B834 File Offset: 0x0000A834
		public ClientAppMetadata Metadata { get; internal set; }

		// Token: 0x060002DC RID: 732 RVA: 0x0000B840 File Offset: 0x0000A840
		internal static SafeXmlDocument ReadToXmlDocument(EwsServiceXmlReader reader)
		{
			SafeXmlDocument safeXmlDocument2;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				reader.ReadBase64ElementValue(memoryStream);
				memoryStream.Position = 0L;
				SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
				safeXmlDocument.Load(memoryStream);
				safeXmlDocument2 = safeXmlDocument;
			}
			return safeXmlDocument2;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000B890 File Offset: 0x0000A890
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "Manifest")
				{
					this.Manifest = ClientApp.ReadToXmlDocument(reader);
					return true;
				}
				if (localName == "Metadata")
				{
					this.Metadata = new ClientAppMetadata();
					this.Metadata.LoadFromXml(reader, XmlNamespace.Types, "Metadata");
					return true;
				}
			}
			return false;
		}
	}
}
