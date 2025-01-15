using System;
using System.Xml;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x02000027 RID: 39
	public class XMLData
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00003FB4 File Offset: 0x00003FB4
		public XMLData(string rawString)
		{
			this.m_data = rawString;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003FD0 File Offset: 0x00003FD0
		public XmlDocument XML
		{
			get
			{
				if (this.m_xmlDoc == null)
				{
					XmlDocument xmlDocument = new XmlDocument();
					if (this.m_data.Length > 0)
					{
						xmlDocument.LoadXml(this.m_data);
					}
					else
					{
						xmlDocument.LoadXml("<null/>");
					}
					this.m_xmlDoc = xmlDocument;
				}
				return this.m_xmlDoc;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004020 File Offset: 0x00004020
		public string RawString
		{
			get
			{
				return this.m_data;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004034 File Offset: 0x00004034
		public override string ToString()
		{
			if (this.m_data != "")
			{
				return this.XML.DocumentElement.OuterXml;
			}
			return this.m_data;
		}

		// Token: 0x04000054 RID: 84
		private XmlDocument m_xmlDoc;

		// Token: 0x04000055 RID: 85
		private string m_data;
	}
}
