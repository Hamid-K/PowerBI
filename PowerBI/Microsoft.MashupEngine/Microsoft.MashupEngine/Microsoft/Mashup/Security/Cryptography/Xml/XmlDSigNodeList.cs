using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.Mashup.Security.Cryptography.Xml
{
	// Token: 0x02002003 RID: 8195
	internal sealed class XmlDSigNodeList : XmlNodeList
	{
		// Token: 0x17003059 RID: 12377
		// (get) Token: 0x0600C7A6 RID: 51110 RVA: 0x0027B97C File Offset: 0x00279B7C
		public override int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x0600C7A7 RID: 51111 RVA: 0x0027B989 File Offset: 0x00279B89
		public void Add(XmlNode node)
		{
			this.m_list.Add(node);
		}

		// Token: 0x0600C7A8 RID: 51112 RVA: 0x0027B997 File Offset: 0x00279B97
		public override IEnumerator GetEnumerator()
		{
			return this.m_list.GetEnumerator();
		}

		// Token: 0x0600C7A9 RID: 51113 RVA: 0x0027B9A9 File Offset: 0x00279BA9
		public override XmlNode Item(int index)
		{
			return this.m_list[index];
		}

		// Token: 0x040065F4 RID: 26100
		private List<XmlNode> m_list = new List<XmlNode>();
	}
}
