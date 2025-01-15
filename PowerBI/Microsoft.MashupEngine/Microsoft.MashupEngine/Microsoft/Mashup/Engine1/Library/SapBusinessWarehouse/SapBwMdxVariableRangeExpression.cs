using System;
using Microsoft.Mashup.Engine1.Library.Mdx;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004BE RID: 1214
	internal sealed class SapBwMdxVariableRangeExpression : MdxExpression
	{
		// Token: 0x060027D8 RID: 10200 RVA: 0x000756BF File Offset: 0x000738BF
		public SapBwMdxVariableRangeExpression(MdxExpression start, MdxExpression end)
		{
			this.start = start;
			this.end = end;
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x000756D5 File Offset: 0x000738D5
		public override void Write(MdxExpressionWriter writer)
		{
			this.start.Write(writer);
			writer.Write(":");
			this.end.Write(writer);
		}

		// Token: 0x040010DA RID: 4314
		private readonly MdxExpression start;

		// Token: 0x040010DB RID: 4315
		private readonly MdxExpression end;
	}
}
