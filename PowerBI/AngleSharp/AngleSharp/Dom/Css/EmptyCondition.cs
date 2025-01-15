using System;
using System.IO;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000208 RID: 520
	internal sealed class EmptyCondition : CssNode, IConditionFunction, ICssNode, IStyleFormattable
	{
		// Token: 0x0600138E RID: 5006 RVA: 0x0002F0AA File Offset: 0x0002D2AA
		public bool Check()
		{
			return true;
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00003C25 File Offset: 0x00001E25
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
		}
	}
}
