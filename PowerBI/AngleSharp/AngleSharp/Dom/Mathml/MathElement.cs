using System;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Services;

namespace AngleSharp.Dom.Mathml
{
	// Token: 0x020001BD RID: 445
	internal class MathElement : Element
	{
		// Token: 0x06000F2E RID: 3886 RVA: 0x00046E7E File Offset: 0x0004507E
		public MathElement(Document owner, string name, string prefix = null, NodeFlags flags = NodeFlags.None)
			: base(owner, name, prefix, NamespaceNames.MathMlUri, flags | NodeFlags.MathMember)
		{
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x00046E98 File Offset: 0x00045098
		public override INode Clone(bool deep = true)
		{
			MathElement mathElement = base.Owner.Options.GetFactory<IElementFactory<MathElement>>().Create(base.Owner, base.LocalName, base.Prefix);
			base.CloneElement(mathElement, deep);
			return mathElement;
		}
	}
}
