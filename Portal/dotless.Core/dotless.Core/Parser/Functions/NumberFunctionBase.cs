using System;
using System.Linq;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x0200009B RID: 155
	public abstract class NumberFunctionBase : Function
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x00016C48 File Offset: 0x00014E48
		protected override Node Evaluate(Env env)
		{
			Guard.ExpectMinArguments(1, base.Arguments.Count, this, base.Location);
			Guard.ExpectNode<Number>(base.Arguments[0], this, base.Arguments[0].Location);
			Number number = base.Arguments[0] as Number;
			Node[] array = base.Arguments.Skip(1).ToArray<Node>();
			return this.Eval(env, number, array);
		}

		// Token: 0x060004DE RID: 1246
		protected abstract Node Eval(Env env, Number number, Node[] args);
	}
}
