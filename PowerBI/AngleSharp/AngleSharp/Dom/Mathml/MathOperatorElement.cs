using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Mathml
{
	// Token: 0x020001C0 RID: 448
	internal sealed class MathOperatorElement : MathElement
	{
		// Token: 0x06000F32 RID: 3890 RVA: 0x00046EFE File Offset: 0x000450FE
		public MathOperatorElement(Document owner, string prefix = null)
			: base(owner, TagNames.Mo, prefix, NodeFlags.Special | NodeFlags.Scoped | NodeFlags.MathTip)
		{
		}
	}
}
