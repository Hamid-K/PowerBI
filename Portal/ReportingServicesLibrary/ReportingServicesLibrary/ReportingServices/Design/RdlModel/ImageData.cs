using System;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003E1 RID: 993
	public sealed class ImageData : IXmlSerializable
	{
		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06001FA7 RID: 8103 RVA: 0x0007EAC2 File Offset: 0x0007CCC2
		// (set) Token: 0x06001FA8 RID: 8104 RVA: 0x0007EACA File Offset: 0x0007CCCA
		public byte[] Bytes
		{
			get
			{
				return this.m_data;
			}
			set
			{
				this.m_data = value;
			}
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x0007EAD4 File Offset: 0x0007CCD4
		public override bool Equals(object obj)
		{
			ImageData imageData = obj as ImageData;
			if (imageData == null)
			{
				return false;
			}
			if (imageData.Bytes.Length != this.m_data.Length)
			{
				return false;
			}
			for (int i = 0; i < this.m_data.Length; i++)
			{
				if (imageData.Bytes[i] != this.m_data[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x0007EB29 File Offset: 0x0007CD29
		public override int GetHashCode()
		{
			return this.m_data.GetHashCode();
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x00005C88 File Offset: 0x00003E88
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x0007EB38 File Offset: 0x0007CD38
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string text = reader.ReadString();
			this.m_data = Convert.FromBase64String(text);
			reader.Skip();
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x0007EB60 File Offset: 0x0007CD60
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			StringBuilder stringBuilder = new StringBuilder(Convert.ToBase64String(this.m_data));
			for (int i = 1000; i < stringBuilder.Length; i += 1000)
			{
				stringBuilder.Insert(i, "\n");
			}
			writer.WriteString(stringBuilder.ToString());
		}

		// Token: 0x04000DCD RID: 3533
		private byte[] m_data;
	}
}
