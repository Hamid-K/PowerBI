using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Mathml
{
	// Token: 0x020001BE RID: 446
	internal sealed class MathIdentifierElement : MathElement
	{
		// Token: 0x06000F30 RID: 3888 RVA: 0x00046ED6 File Offset: 0x000450D6
		public MathIdentifierElement(Document owner, string prefix = null)
			: base(owner, TagNames.Mi, prefix, NodeFlags.Special | NodeFlags.Scoped | NodeFlags.MathTip)
		{
		}
	}
}
