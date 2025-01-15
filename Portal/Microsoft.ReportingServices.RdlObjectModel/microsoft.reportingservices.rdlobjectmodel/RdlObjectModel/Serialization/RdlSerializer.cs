using System;
using System.IO;
using System.Xml;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
	// Token: 0x020002DC RID: 732
	public class RdlSerializer
	{
		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x0003445E File Offset: 0x0003265E
		public RdlSerializerSettings Settings
		{
			get
			{
				return this.m_settings;
			}
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00034466 File Offset: 0x00032666
		public RdlSerializer()
		{
			this.m_settings = new RdlSerializerSettings();
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00034479 File Offset: 0x00032679
		public RdlSerializer(RdlSerializerSettings settings)
		{
			this.m_settings = settings;
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00034488 File Offset: 0x00032688
		public Report Deserialize(Stream stream)
		{
			return (Report)this.Deserialize(stream, typeof(Report));
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x000344A0 File Offset: 0x000326A0
		public Report Deserialize(TextReader textReader)
		{
			return (Report)this.Deserialize(textReader, typeof(Report));
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x000344B8 File Offset: 0x000326B8
		public Report Deserialize(XmlReader xmlReader)
		{
			return (Report)this.Deserialize(xmlReader, typeof(Report));
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x000344D0 File Offset: 0x000326D0
		public object Deserialize(Stream stream, Type objectType)
		{
			return new RdlReader(this.m_settings).Deserialize(stream, objectType);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x000344E4 File Offset: 0x000326E4
		public object Deserialize(TextReader textReader, Type objectType)
		{
			return new RdlReader(this.m_settings).Deserialize(textReader, objectType);
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x000344F8 File Offset: 0x000326F8
		public object Deserialize(XmlReader xmlReader, Type objectType)
		{
			return new RdlReader(this.m_settings).Deserialize(xmlReader, objectType);
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0003450C File Offset: 0x0003270C
		public void Serialize(Stream stream, object o)
		{
			XmlWriter xmlWriter = XmlWriter.Create(stream, this.GetXmlWriterSettings());
			this.Serialize(xmlWriter, o);
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x00034530 File Offset: 0x00032730
		public void Serialize(TextWriter textWriter, object o)
		{
			XmlWriter xmlWriter = XmlWriter.Create(textWriter, this.GetXmlWriterSettings());
			this.Serialize(xmlWriter, o);
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x00034552 File Offset: 0x00032752
		public void Serialize(XmlWriter xmlWriter, object o)
		{
			new RdlWriter(this.m_settings).Serialize(xmlWriter, o);
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x00034566 File Offset: 0x00032766
		private XmlWriterSettings GetXmlWriterSettings()
		{
			return new XmlWriterSettings
			{
				Indent = true
			};
		}

		// Token: 0x040006FD RID: 1789
		private readonly RdlSerializerSettings m_settings;
	}
}
