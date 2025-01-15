using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Mathml
{
	// Token: 0x020001C1 RID: 449
	internal sealed class MathStringElement : MathElement
	{
		// Token: 0x06000F33 RID: 3891 RVA: 0x00046F12 File Offset: 0x00045112
		public MathStringElement(Document owner, string prefix = null)
			: base(owner, TagNames.Ms, prefix, NodeFlags.Special | NodeFlags.Scoped | NodeFlags.MathTip)
		{
		}
	}
}
