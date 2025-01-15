using System;
using System.Xml;

namespace Microsoft.Identity.Json.Converters
{
	// Token: 0x020000F0 RID: 240
	internal class XmlElementWrapper : XmlNodeWrapper, IXmlElement, IXmlNode
	{
		// Token: 0x06000C95 RID: 3221 RVA: 0x00032B91 File Offset: 0x00030D91
		public XmlElementWrapper(XmlElement element)
			: base(element)
		{
			this._element = element;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00032BA4 File Offset: 0x00030DA4
		public void SetAttributeNode(IXmlNode attribute)
		{
			XmlNodeWrapper xmlNodeWrapper = (XmlNodeWrapper)attribute;
			this._element.SetAttributeNode((XmlAttribute)xmlNodeWrapper.WrappedNode);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00032BCF File Offset: 0x00030DCF
		public string GetPrefixOfNamespace(string namespaceUri)
		{
			return this._element.GetPrefixOfNamespace(namespaceUri);
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x00032BDD File Offset: 0x00030DDD
		public bool IsEmpty
		{
			get
			{
				return this._element.IsEmpty;
			}
		}

		// Token: 0x040003E9 RID: 1001
		private readonly XmlElement _element;
	}
}
