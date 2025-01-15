using System;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000067 RID: 103
	public class FadeOutFunction : AlphaFunction
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x000155BD File Offset: 0x000137BD
		protected override Node EditColor(Color color, Number number)
		{
			return base.EditColor(color, -number);
		}
	}
}
