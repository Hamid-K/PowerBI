using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000076 RID: 118
	public class ExtractFunction : ListFunctionBase
	{
		// Token: 0x0600047C RID: 1148 RVA: 0x00015D48 File Offset: 0x00013F48
		protected override Node Eval(Env env, Node[] list, Node[] args)
		{
			Guard.ExpectNumArguments(1, args.Length, this, base.Location);
			Guard.ExpectNode<Number>(args[0], this, args[0].Location);
			int num = (int)(args[0] as Number).Value;
			return list[num - 1];
		}
	}
}
