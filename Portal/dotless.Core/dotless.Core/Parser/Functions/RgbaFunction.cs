using System;
using System.Collections.Generic;
using System.Linq;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x020000A0 RID: 160
	public class RgbaFunction : Function
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x00016E70 File Offset: 0x00015070
		protected override Node Evaluate(Env env)
		{
			if (base.Arguments.Count == 2)
			{
				Color color = Guard.ExpectNode<Color>(base.Arguments[0], this, base.Location);
				Number number = Guard.ExpectNode<Number>(base.Arguments[1], this, base.Location);
				return new Color(color.RGB, number.Value);
			}
			Guard.ExpectNumArguments(4, base.Arguments.Count, this, base.Location);
			List<Number> list = Guard.ExpectAllNodes<Number>(base.Arguments, this, base.Location);
			double[] array = (from n in list.Take(3)
				select n.ToNumber(255.0)).ToArray<double>();
			double num = list[3].ToNumber(1.0);
			return new Color(array, num);
		}
	}
}
