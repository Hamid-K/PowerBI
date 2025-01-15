using System;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000093 RID: 147
	public class DarkenFunction : LightenFunction
	{
		// Token: 0x060004CB RID: 1227 RVA: 0x00016799 File Offset: 0x00014999
		protected override Node EditHsl(HslColor color, Number number)
		{
			return base.EditHsl(color, -number);
		}
	}
}
