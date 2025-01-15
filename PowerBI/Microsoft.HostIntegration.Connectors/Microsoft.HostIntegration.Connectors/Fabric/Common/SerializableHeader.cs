using System;
using System.ServiceModel.Channels;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003CE RID: 974
	internal abstract class SerializableHeader : MessageHeader, IXmlSerializable
	{
		// Token: 0x06002243 RID: 8771 RVA: 0x00069E0C File Offset: 0x0006800C
		protected sealed override void OnWriteStartHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			this.OnWriteStartElement(writer);
			base.WriteHeaderAttributes(writer, messageVersion);
			this.WriteAttributes(writer);
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x00069E24 File Offset: 0x00068024
		protected sealed override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			this.WriteContent(writer);
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x00069E30 File Offset: 0x00068030
		public void WriteXml(XmlWriter writer)
		{
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(writer);
			this.WriteTo(xmlDictionaryWriter);
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x00069E4C File Offset: 0x0006804C
		public void ReadXml(XmlReader reader)
		{
			XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateDictionaryReader(reader);
			xmlDictionaryReader.Read();
			this.ReadFrom(xmlDictionaryReader);
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x00069E6E File Offset: 0x0006806E
		public XmlSchema GetSchema()
		{
			ReleaseAssert.IsTrue(false);
			return null;
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x00069E77 File Offset: 0x00068077
		public void WriteTo(XmlDictionaryWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.OnWriteStartElement(writer);
			this.WriteAttributes(writer);
			this.WriteContent(writer);
			writer.WriteEndElement();
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x00069EA2 File Offset: 0x000680A2
		protected virtual void OnWriteStartElement(XmlDictionaryWriter writer)
		{
			writer.WriteStartElement(this.Name, this.Namespace);
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x00069EB6 File Offset: 0x000680B6
		public void WriteAttributes(XmlDictionaryWriter writer)
		{
			this.OnWriteAttributes(writer);
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x000036A9 File Offset: 0x000018A9
		protected virtual void OnWriteAttributes(XmlDictionaryWriter writer)
		{
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x00069EBF File Offset: 0x000680BF
		public void WriteContent(XmlDictionaryWriter writer)
		{
			this.OnWriteContent(writer);
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x000036A9 File Offset: 0x000018A9
		protected virtual void OnWriteContent(XmlDictionaryWriter writer)
		{
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x00069EC8 File Offset: 0x000680C8
		public void ReadFrom(XmlDictionaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.ReadAttributes(reader);
			if (!reader.IsEmptyElement)
			{
				reader.Read();
				this.ReadContent(reader);
				return;
			}
			reader.Read();
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x00069EFD File Offset: 0x000680FD
		protected void ReadAttributes(XmlDictionaryReader reader)
		{
			this.OnReadAttributes(reader);
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x000036A9 File Offset: 0x000018A9
		protected virtual void OnReadAttributes(XmlDictionaryReader reader)
		{
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x00069F06 File Offset: 0x00068106
		public void ReadContent(XmlDictionaryReader reader)
		{
			this.OnReadContent(reader);
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x000036A9 File Offset: 0x000018A9
		protected virtual void OnReadContent(XmlDictionaryReader reader)
		{
		}
	}
}
