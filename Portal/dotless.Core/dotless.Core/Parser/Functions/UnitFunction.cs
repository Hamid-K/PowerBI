using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x020000AA RID: 170
	public class UnitFunction : Function
	{
		// Token: 0x060004FE RID: 1278 RVA: 0x00017274 File Offset: 0x00015474
		protected override Node Evaluate(Env env)
		{
			Guard.ExpectMaxArguments(2, base.Arguments.Count, this, base.Location);
			Guard.ExpectNode<Number>(base.Arguments[0], this, base.Location);
			Number number = base.Arguments[0] as Number;
			string text = string.Empty;
			if (base.Arguments.Count == 2)
			{
				if (base.Arguments[1] is Keyword)
				{
					text = ((Keyword)base.Arguments[1]).Value;
				}
				else
				{
					text = base.Arguments[1].ToCSS(env);
				}
			}
			return new Number(number.Value, text);
		}
	}
}
