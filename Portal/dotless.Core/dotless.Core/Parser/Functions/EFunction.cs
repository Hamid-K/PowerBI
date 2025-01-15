using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x020000A8 RID: 168
	public class EFunction : Function
	{
		// Token: 0x060004FA RID: 1274 RVA: 0x00017100 File Offset: 0x00015300
		protected override Node Evaluate(Env env)
		{
			Guard.ExpectMaxArguments(1, base.Arguments.Count, this, base.Location);
			base.WarnNotSupportedByLessJS("e(string)", "~\"\"");
			if (base.Arguments.Count == 0)
			{
				return new TextNode("");
			}
			Node node = base.Arguments[0];
			if (node is Quoted)
			{
				return new TextNode((node as Quoted).UnescapeContents());
			}
			return new TextNode(node.ToCSS(env));
		}
	}
}
