using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Mathml
{
	// Token: 0x020001C2 RID: 450
	internal sealed class MathTextElement : MathElement
	{
		// Token: 0x06000F34 RID: 3892 RVA: 0x00046F26 File Offset: 0x00045126
		public MathTextElement(Document owner, string prefix = null)
			: base(owner, TagNames.Mtext, prefix, NodeFlags.Special | NodeFlags.Scoped | NodeFlags.MathTip)
		{
		}
	}
}
