using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Xml
{
	// Token: 0x020001AC RID: 428
	internal sealed class XmlElement : Element
	{
		// Token: 0x06000F18 RID: 3864 RVA: 0x00046C05 File Offset: 0x00044E05
		public XmlElement(Document owner, string name, string prefix = null)
			: base(owner, name, prefix, NamespaceNames.XmlUri, NodeFlags.None)
		{
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x00046C16 File Offset: 0x00044E16
		// (set) Token: 0x06000F1A RID: 3866 RVA: 0x00046C1E File Offset: 0x00044E1E
		internal string IdAttribute { get; set; }

		// Token: 0x06000F1B RID: 3867 RVA: 0x00046C28 File Offset: 0x00044E28
		public override INode Clone(bool deep = true)
		{
			XmlElement xmlElement = new XmlElement(base.Owner, base.LocalName, base.Prefix);
			base.CloneElement(xmlElement, deep);
			xmlElement.IdAttribute = this.IdAttribute;
			return xmlElement;
		}
	}
}
