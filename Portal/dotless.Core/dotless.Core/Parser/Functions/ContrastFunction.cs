using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000071 RID: 113
	public class ContrastFunction : Function
	{
		// Token: 0x0600046F RID: 1135 RVA: 0x000159A8 File Offset: 0x00013BA8
		protected override Node Evaluate(Env env)
		{
			Guard.ExpectMinArguments(1, base.Arguments.Count, this, base.Location);
			Guard.ExpectMaxArguments(4, base.Arguments.Count, this, base.Location);
			Guard.ExpectNode<Color>(base.Arguments[0], this, base.Location);
			Color color = (Color)base.Arguments[0];
			if (base.Arguments.Count > 1)
			{
				Guard.ExpectNode<Color>(base.Arguments[1], this, base.Location);
			}
			if (base.Arguments.Count > 2)
			{
				Guard.ExpectNode<Color>(base.Arguments[2], this, base.Location);
			}
			if (base.Arguments.Count > 3)
			{
				Guard.ExpectNode<Number>(base.Arguments[3], this, base.Location);
			}
			Color color2 = ((base.Arguments.Count > 1) ? ((Color)base.Arguments[1]) : new Color(255.0, 255.0, 255.0));
			Color color3 = ((base.Arguments.Count > 2) ? ((Color)base.Arguments[2]) : new Color(0.0, 0.0, 0.0));
			double num = ((base.Arguments.Count > 3) ? ((Number)base.Arguments[3]).ToNumber() : 0.43);
			if (color3.Luma > color2.Luma)
			{
				Color color4 = color2;
				color2 = color3;
				color3 = color4;
			}
			if (color.Luma >= num)
			{
				return color3;
			}
			return color2;
		}
	}
}
