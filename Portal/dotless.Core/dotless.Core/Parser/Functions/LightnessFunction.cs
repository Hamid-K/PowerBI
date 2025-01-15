using System;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000094 RID: 148
	public class LightnessFunction : LightenFunction
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x000167B0 File Offset: 0x000149B0
		protected override Node EditHsl(HslColor color, Number number)
		{
			base.WarnNotSupportedByLessJS("lightness(color, number)", "lighten(color, number) or its opposite darken(color, number),");
			return base.EditHsl(color, number);
		}
	}
}
