using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000CE RID: 206
	public class ImageData : IXmlSerializable
	{
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0001D980 File Offset: 0x0001BB80
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x0001D988 File Offset: 0x0001BB88
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

		// Token: 0x0600092D RID: 2349 RVA: 0x0001D991 File Offset: 0x0001BB91
		public ImageData()
			: this(null)
		{
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0001D99A File Offset: 0x0001BB9A
		public ImageData(byte[] bytes)
		{
			this.m_data = bytes;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0001D9A9 File Offset: 0x0001BBA9
		public static implicit operator ImageData(byte[] bytes)
		{
			return new ImageData(bytes);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001D9B1 File Offset: 0x0001BBB1
		public static explicit operator byte[](ImageData value)
		{
			return value.Bytes;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001D9B9 File Offset: 0x0001BBB9
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0001D9BC File Offset: 0x0001BBBC
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string text = reader.ReadString();
			this.m_data = Convert.FromBase64String(text);
			reader.Skip();
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0001D9E4 File Offset: 0x0001BBE4
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

		// Token: 0x04000184 RID: 388
		private byte[] m_data;
	}
}
