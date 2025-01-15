using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000097 RID: 151
	public class TintFunction : MixFunction
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x00016AAC File Offset: 0x00014CAC
		protected override Node Evaluate(Env env)
		{
			Guard.ExpectNumArguments(2, base.Arguments.Count, this, base.Location);
			Guard.ExpectNode<Color>(base.Arguments[0], this, base.Location);
			Guard.ExpectNode<Number>(base.Arguments[1], this, base.Location);
			double value = ((Number)base.Arguments[1]).Value;
			return base.Mix(new Color(255.0, 255.0, 255.0), (Color)base.Arguments[0], value);
		}
	}
}
