using System;
using System.Xml;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002116 RID: 8470
	public abstract class OpenXmlLeafElement : OpenXmlElement
	{
		// Token: 0x17003291 RID: 12945
		// (get) Token: 0x0600D142 RID: 53570 RVA: 0x0029A2CC File Offset: 0x002984CC
		// (set) Token: 0x0600D143 RID: 53571 RVA: 0x0029A2D4 File Offset: 0x002984D4
		internal OpenXmlElement ShadowElement { get; set; }

		// Token: 0x17003292 RID: 12946
		// (get) Token: 0x0600D145 RID: 53573 RVA: 0x00002105 File Offset: 0x00000305
		public override bool HasChildren
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17003293 RID: 12947
		// (get) Token: 0x0600D146 RID: 53574 RVA: 0x0029A2DD File Offset: 0x002984DD
		// (set) Token: 0x0600D147 RID: 53575 RVA: 0x0029A2F8 File Offset: 0x002984F8
		public override string InnerXml
		{
			get
			{
				if (this.ShadowElement != null)
				{
					return this.ShadowElement.InnerXml;
				}
				return string.Empty;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.ShadowElement = null;
					return;
				}
				throw new InvalidOperationException(ExceptionMessages.LeafElementInnerXmlCannotBeSet);
			}
		}

		// Token: 0x0600D148 RID: 53576 RVA: 0x0029A314 File Offset: 0x00298514
		internal override void WriteContentTo(XmlWriter w)
		{
			if (this.ShadowElement != null)
			{
				this.ShadowElement.WriteContentTo(w);
			}
		}

		// Token: 0x0600D149 RID: 53577 RVA: 0x0000336E File Offset: 0x0000156E
		public override void RemoveAllChildren()
		{
		}

		// Token: 0x0600D14A RID: 53578 RVA: 0x0029A32C File Offset: 0x0029852C
		internal override void Populate(XmlReader xmlReader, OpenXmlLoadMode loadMode)
		{
			this.LoadAttributes(xmlReader);
			if (!xmlReader.IsEmptyElement)
			{
				this.ShadowElement = new OpenXmlUnknownElement(this.Prefix, this.LocalName, this.NamespaceUri);
				this.ShadowElement.InnerXml = xmlReader.ReadInnerXml();
			}
			else
			{
				xmlReader.Skip();
			}
			base.RawOuterXml = string.Empty;
		}
	}
}
