using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000197 RID: 407
	public class VectorData : IXmlSerializable
	{
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x0002219A File Offset: 0x0002039A
		// (set) Token: 0x06000D43 RID: 3395 RVA: 0x000221A2 File Offset: 0x000203A2
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

		// Token: 0x06000D44 RID: 3396 RVA: 0x000221AB File Offset: 0x000203AB
		public VectorData()
		{
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x000221B3 File Offset: 0x000203B3
		public VectorData(byte[] bytes)
		{
			this.m_data = bytes;
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x000221C2 File Offset: 0x000203C2
		public static implicit operator VectorData(byte[] bytes)
		{
			return new VectorData(bytes);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x000221CA File Offset: 0x000203CA
		public static implicit operator byte[](VectorData value)
		{
			return value.Bytes;
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x000221D2 File Offset: 0x000203D2
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x000221D8 File Offset: 0x000203D8
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string text = reader.ReadString();
			this.m_data = Convert.FromBase64String(text);
			reader.Skip();
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00022200 File Offset: 0x00020400
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.m_data != null)
			{
				string text = Convert.ToBase64String(this.m_data);
				for (int i = 0; i < text.Length; i += 1000)
				{
					if (i > 0)
					{
						writer.WriteString("\n");
					}
					int num = Math.Min(1000, text.Length - i);
					char[] array = text.ToCharArray(i, num);
					writer.WriteChars(array, 0, array.Length);
				}
			}
		}

		// Token: 0x0400053A RID: 1338
		private byte[] m_data;
	}
}
