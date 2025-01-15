using System;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x0200007C RID: 124
	public class GrayscaleFunction : GreyscaleFunction
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x00016378 File Offset: 0x00014578
		protected override Node Eval(Color color)
		{
			base.WarnNotSupportedByLessJS("grayscale(color)", "greyscale(color)");
			return base.Eval(color);
		}
	}
}
