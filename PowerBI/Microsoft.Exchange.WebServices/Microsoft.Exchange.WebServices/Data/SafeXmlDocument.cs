using System;
using System.IO;
using System.Xml;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000301 RID: 769
	internal class SafeXmlDocument : XmlDocument
	{
		// Token: 0x06001B5C RID: 7004 RVA: 0x0004924C File Offset: 0x0004824C
		public SafeXmlDocument()
		{
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x0004927C File Offset: 0x0004827C
		public SafeXmlDocument(XmlImplementation imp)
		{
			throw new NotSupportedException("Not supported");
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x000492B4 File Offset: 0x000482B4
		public SafeXmlDocument(XmlNameTable nt)
			: base(nt)
		{
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x000492E4 File Offset: 0x000482E4
		public override void Load(Stream inStream)
		{
			using (XmlReader xmlReader = XmlReader.Create(inStream, this.settings))
			{
				this.Load(xmlReader);
			}
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x00049324 File Offset: 0x00048324
		public override void Load(string filename)
		{
			using (XmlReader xmlReader = XmlReader.Create(filename, this.settings))
			{
				this.Load(xmlReader);
			}
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x00049364 File Offset: 0x00048364
		public override void Load(TextReader txtReader)
		{
			using (XmlReader xmlReader = XmlReader.Create(txtReader, this.settings))
			{
				this.Load(xmlReader);
			}
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x000493A4 File Offset: 0x000483A4
		public override void Load(XmlReader reader)
		{
			if (reader.Settings != null && !reader.Settings.ProhibitDtd)
			{
				throw new XmlDtdException();
			}
			try
			{
				base.Load(reader);
			}
			catch (XmlException ex)
			{
				if (ex.Message.StartsWith("For security reasons DTD is prohibited in this XML document.", 5))
				{
					throw new XmlDtdException();
				}
			}
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x00049404 File Offset: 0x00048404
		public override void LoadXml(string xml)
		{
			using (XmlReader xmlReader = XmlReader.Create(new StringReader(xml), this.settings))
			{
				base.Load(xmlReader);
			}
		}

		// Token: 0x04001454 RID: 5204
		private XmlReaderSettings settings = new XmlReaderSettings
		{
			ProhibitDtd = true,
			XmlResolver = null
		};
	}
}
