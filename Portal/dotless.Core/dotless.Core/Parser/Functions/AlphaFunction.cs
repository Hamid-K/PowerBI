using System;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000066 RID: 102
	public class AlphaFunction : FadeInFunction
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x0001559B File Offset: 0x0001379B
		protected override Node EditColor(Color color, Number number)
		{
			base.WarnNotSupportedByLessJS("alpha(color, number)", "fadein(color, number) or the opposite fadeout(color, number),");
			return base.EditColor(color, number);
		}
	}
}
