using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Mdx;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004BB RID: 1211
	internal sealed class SapBwMdxSelectExpression : MdxExpression
	{
		// Token: 0x060027D3 RID: 10195 RVA: 0x000755C7 File Offset: 0x000737C7
		public SapBwMdxSelectExpression(MdxExpression select, IList<SapBwMdxVariableExpression> variables)
		{
			this.select = select;
			this.variables = variables;
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x000755E0 File Offset: 0x000737E0
		public override void Write(MdxExpressionWriter writer)
		{
			this.select.Write(writer);
			if (this.variables.Count > 0)
			{
				writer.Write("SAP VARIABLES");
				foreach (SapBwMdxVariableExpression sapBwMdxVariableExpression in this.variables)
				{
					sapBwMdxVariableExpression.Write(writer);
				}
			}
		}

		// Token: 0x040010D2 RID: 4306
		private readonly MdxExpression select;

		// Token: 0x040010D3 RID: 4307
		private readonly IList<SapBwMdxVariableExpression> variables;
	}
}
