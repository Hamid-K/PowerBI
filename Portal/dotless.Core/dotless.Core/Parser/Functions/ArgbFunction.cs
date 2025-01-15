using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000069 RID: 105
	public class ArgbFunction : Function
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x000155DF File Offset: 0x000137DF
		protected override Node Evaluate(Env env)
		{
			Guard.ExpectNumArguments(1, base.Arguments.Count, this, base.Location);
			return new TextNode(Guard.ExpectNode<Color>(base.Arguments[0], this, base.Location).ToArgb());
		}
	}
}
