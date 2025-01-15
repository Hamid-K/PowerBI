using System;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Services;

namespace AngleSharp.Dom.Svg
{
	// Token: 0x020001B1 RID: 433
	internal class SvgElement : Element, ISvgElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle
	{
		// Token: 0x06000F26 RID: 3878 RVA: 0x00046DB4 File Offset: 0x00044FB4
		public SvgElement(Document owner, string name, string prefix = null, NodeFlags flags = NodeFlags.None)
			: base(owner, name, prefix, NamespaceNames.SvgUri, flags | NodeFlags.SvgMember)
		{
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x00046DCC File Offset: 0x00044FCC
		public override INode Clone(bool deep = true)
		{
			SvgElement svgElement = base.Owner.Options.GetFactory<IElementFactory<SvgElement>>().Create(base.Owner, base.LocalName, base.Prefix);
			base.CloneElement(svgElement, deep);
			return svgElement;
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x00046E0C File Offset: 0x0004500C
		internal override void SetupElement()
		{
			base.SetupElement();
			string ownAttribute = this.GetOwnAttribute(AttributeNames.Style);
			if (ownAttribute != null)
			{
				base.UpdateStyle(ownAttribute);
			}
		}
	}
}
