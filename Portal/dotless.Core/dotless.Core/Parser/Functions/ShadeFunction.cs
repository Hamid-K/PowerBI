using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000098 RID: 152
	public class ShadeFunction : MixFunction
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x00016B5C File Offset: 0x00014D5C
		protected override Node Evaluate(Env env)
		{
			Guard.ExpectNumArguments(2, base.Arguments.Count, this, base.Location);
			Guard.ExpectNode<Color>(base.Arguments[0], this, base.Location);
			Guard.ExpectNode<Number>(base.Arguments[1], this, base.Location);
			double value = ((Number)base.Arguments[1]).Value;
			return base.Mix(new Color(0.0, 0.0, 0.0), (Color)base.Arguments[0], value);
		}
	}
}
