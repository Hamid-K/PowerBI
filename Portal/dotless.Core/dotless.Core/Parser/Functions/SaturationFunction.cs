using System;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x020000A5 RID: 165
	public class SaturationFunction : SaturateFunction
	{
		// Token: 0x060004F4 RID: 1268 RVA: 0x00017048 File Offset: 0x00015248
		protected override Node EditHsl(HslColor color, Number number)
		{
			base.WarnNotSupportedByLessJS("saturation(color, number)", "saturate(color, number) or its opposite desaturate(color, number),");
			return base.EditHsl(color, number);
		}
	}
}
