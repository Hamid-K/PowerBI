using System;
using System.Linq;
using dotless.Core.Exceptions;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000095 RID: 149
	public abstract class ListFunctionBase : Function
	{
		// Token: 0x060004CF RID: 1231 RVA: 0x000167D4 File Offset: 0x000149D4
		protected override Node Evaluate(Env env)
		{
			Guard.ExpectMinArguments(1, base.Arguments.Count, this, base.Location);
			Guard.ExpectNodeToBeOneOf<Expression, Value>(base.Arguments[0], this, base.Arguments[0].Location);
			if (base.Arguments[0] is Expression)
			{
				Expression expression = base.Arguments[0] as Expression;
				Node[] array = base.Arguments.Skip(1).ToArray<Node>();
				return this.Eval(env, expression.Value.ToArray<Node>(), array);
			}
			if (base.Arguments[0] is Value)
			{
				Value value = base.Arguments[0] as Value;
				Node[] array2 = base.Arguments.Skip(1).ToArray<Node>();
				return this.Eval(env, value.Values.ToArray<Node>(), array2);
			}
			throw new ParsingException(string.Format("First argument to the list function was a {0}", base.Arguments[0].GetType().Name.ToLowerInvariant()), base.Location);
		}

		// Token: 0x060004D0 RID: 1232
		protected abstract Node Eval(Env env, Node[] list, Node[] args);
	}
}
