using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Microsoft.IdentityModel.Json.Converters
{
	// Token: 0x020000F1 RID: 241
	[NullableContext(1)]
	[Nullable(0)]
	internal class XmlElementWrapper : XmlNodeWrapper, IXmlElement, IXmlNode
	{
		// Token: 0x06000CA5 RID: 3237 RVA: 0x00033345 File Offset: 0x00031545
		public XmlElementWrapper(XmlElement element)
			: base(element)
		{
			this._element = element;
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00033358 File Offset: 0x00031558
		public void SetAttributeNode(IXmlNode attribute)
		{
			XmlNodeWrapper xmlNodeWrapper = (XmlNodeWrapper)attribute;
			this._element.SetAttributeNode((XmlAttribute)xmlNodeWrapper.WrappedNode);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00033383 File Offset: 0x00031583
		[return: Nullable(2)]
		public string GetPrefixOfNamespace(string namespaceUri)
		{
			return this._element.GetPrefixOfNamespace(namespaceUri);
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00033391 File Offset: 0x00031591
		public bool IsEmpty
		{
			get
			{
				return this._element.IsEmpty;
			}
		}

		// Token: 0x04000406 RID: 1030
		private readonly XmlElement _element;
	}
}
