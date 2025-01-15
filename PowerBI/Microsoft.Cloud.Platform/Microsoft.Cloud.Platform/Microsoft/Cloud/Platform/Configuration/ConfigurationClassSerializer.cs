using System;
using System.IO;
using System.Xml;

namespace Microsoft.Cloud.Platform.Configuration
{
	// Token: 0x02000428 RID: 1064
	public class ConfigurationClassSerializer
	{
		// Token: 0x060020CF RID: 8399 RVA: 0x0007B75D File Offset: 0x0007995D
		public ConfigurationClassSerializer(Type type)
		{
			this.m_type = type;
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x0007B76C File Offset: 0x0007996C
		public void Serialize(Stream stream, IConfigurationClass configurationClassToSerialize)
		{
			using (XmlWriter xmlWriter = XmlWriter.Create(stream))
			{
				xmlWriter.WriteStartElement(this.m_type.Name);
				configurationClassToSerialize.WriteXml(xmlWriter);
				xmlWriter.WriteEndElement();
			}
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x0007B7BC File Offset: 0x000799BC
		public void Serialize(XmlWriter xmlWriter, IConfigurationClass configurationClassToSerialize)
		{
			xmlWriter.WriteStartElement(this.m_type.Name);
			configurationClassToSerialize.WriteXml(xmlWriter);
			xmlWriter.WriteEndElement();
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x0007B7DC File Offset: 0x000799DC
		public IConfigurationClass Deserialize(XmlReader reader)
		{
			object obj = Activator.CreateInstance(this.m_type, false);
			this.m_type.GetMethod("ReadXml").Invoke(obj, new object[] { reader });
			return obj as IConfigurationClass;
		}

		// Token: 0x04000B41 RID: 2881
		private Type m_type;
	}
}
