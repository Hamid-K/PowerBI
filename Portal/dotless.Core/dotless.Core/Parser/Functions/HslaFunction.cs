using System;
using System.Linq;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000080 RID: 128
	public class HslaFunction : Function
	{
		// Token: 0x060004A4 RID: 1188 RVA: 0x00016518 File Offset: 0x00014718
		protected override Node Evaluate(Env env)
		{
			Guard.ExpectNumArguments(4, base.Arguments.Count, this, base.Location);
			Guard.ExpectAllNodes<Number>(base.Arguments, this, base.Location);
			Number[] array = base.Arguments.Cast<Number>().ToArray<Number>();
			return new HslColor(array[0], array[1], array[2], array[3]).ToRgbColor();
		}
	}
}
