using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Mathml
{
	// Token: 0x020001BF RID: 447
	internal sealed class MathNumberElement : MathElement
	{
		// Token: 0x06000F31 RID: 3889 RVA: 0x00046EEA File Offset: 0x000450EA
		public MathNumberElement(Document owner, string prefix = null)
			: base(owner, TagNames.Mn, prefix, NodeFlags.Special | NodeFlags.Scoped | NodeFlags.MathTip)
		{
		}
	}
}
