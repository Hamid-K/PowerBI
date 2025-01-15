using System;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x020000A4 RID: 164
	public class DesaturateFunction : SaturateFunction
	{
		// Token: 0x060004F2 RID: 1266 RVA: 0x00017031 File Offset: 0x00015231
		protected override Node EditHsl(HslColor color, Number number)
		{
			return base.EditHsl(color, -number);
		}
	}
}
