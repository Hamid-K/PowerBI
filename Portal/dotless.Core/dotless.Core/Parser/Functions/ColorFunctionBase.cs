using System;
using System.Linq;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x0200006E RID: 110
	public abstract class ColorFunctionBase : Function
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x00015754 File Offset: 0x00013954
		protected override Node Evaluate(Env env)
		{
			Guard.ExpectMinArguments(1, base.Arguments.Count<Node>(), this, base.Location);
			Guard.ExpectNode<Color>(base.Arguments[0], this, base.Arguments[0].Location);
			Color color = base.Arguments[0] as Color;
			if (base.Arguments.Count == 2)
			{
				Guard.ExpectNode<Number>(base.Arguments[1], this, base.Arguments[1].Location);
				Number number = base.Arguments[1] as Number;
				Node node = this.EditColor(color, number);
				if (node != null)
				{
					return node;
				}
			}
			return this.Eval(color);
		}

		// Token: 0x06000465 RID: 1125
		protected abstract Node Eval(Color color);

		// Token: 0x06000466 RID: 1126 RVA: 0x00015808 File Offset: 0x00013A08
		protected virtual Node EditColor(Color color, Number number)
		{
			return null;
		}
	}
}
