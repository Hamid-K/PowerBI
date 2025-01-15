using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004C2 RID: 1218
	public class CustomXmlObjectSerializer : XmlObjectSerializer
	{
		// Token: 0x0600252A RID: 9514 RVA: 0x000842DA File Offset: 0x000824DA
		public CustomXmlObjectSerializer(Type typeToSerialize)
		{
			this.Initialize(typeToSerialize, null, null);
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000842EB File Offset: 0x000824EB
		public CustomXmlObjectSerializer(Type typeToSerialize, XmlQualifiedName qualifiedName)
		{
			if (qualifiedName == null)
			{
				throw new CommunicationFrameworkArgumentException("qualifiedName");
			}
			this.Initialize(typeToSerialize, qualifiedName.Name, qualifiedName.Namespace);
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x0008431C File Offset: 0x0008251C
		private void Initialize(Type type, string rootName, string rootNamespace)
		{
			if (type == null)
			{
				throw new CommunicationFrameworkArgumentException("type");
			}
			this.m_rootName = rootName;
			this.m_rootNamespace = rootNamespace ?? string.Empty;
			if (this.m_rootName == null)
			{
				this.m_serializer = new XmlSerializer(type);
				return;
			}
			XmlRootAttribute xmlRootAttribute = new XmlRootAttribute
			{
				ElementName = this.m_rootName,
				Namespace = this.m_rootNamespace
			};
			this.m_serializer = new XmlSerializer(type, xmlRootAttribute);
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x00084394 File Offset: 0x00082594
		public override bool IsStartObject(XmlDictionaryReader reader)
		{
			if (reader == null)
			{
				throw new CommunicationFrameworkArgumentException("reader");
			}
			reader.MoveToElement();
			if (this.m_rootName != null)
			{
				return reader.IsStartElement(this.m_rootName, this.m_rootNamespace);
			}
			return reader.IsStartElement();
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000843CC File Offset: 0x000825CC
		public override object ReadObject(XmlDictionaryReader reader, bool verifyObjectName)
		{
			if (reader == null)
			{
				throw new CommunicationFrameworkArgumentException("reader");
			}
			return this.m_serializer.Deserialize(reader);
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x0003722B File Offset: 0x0003542B
		public override void WriteStartObject(XmlDictionaryWriter writer, object graph)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x000843E8 File Offset: 0x000825E8
		public override void WriteObjectContent(XmlDictionaryWriter writer, object graph)
		{
			if (writer == null)
			{
				throw new CommunicationFrameworkArgumentException("writer");
			}
			if (writer.WriteState != WriteState.Element)
			{
				throw new CommunicationFrameworkSerializationException("WriteState '{0}' is in-valid.".FormatWithCurrentCulture(new object[] { writer.WriteState }));
			}
			this.m_serializer.Serialize(writer, graph);
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x0003722B File Offset: 0x0003542B
		public override void WriteEndObject(XmlDictionaryWriter writer)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x0008443D File Offset: 0x0008263D
		public override void WriteObject(XmlDictionaryWriter writer, object graph)
		{
			if (writer == null)
			{
				throw new CommunicationFrameworkArgumentException("writer");
			}
			this.m_serializer.Serialize(writer, graph);
		}

		// Token: 0x04000D13 RID: 3347
		private XmlSerializer m_serializer;

		// Token: 0x04000D14 RID: 3348
		private string m_rootName;

		// Token: 0x04000D15 RID: 3349
		private string m_rootNamespace;
	}
}
