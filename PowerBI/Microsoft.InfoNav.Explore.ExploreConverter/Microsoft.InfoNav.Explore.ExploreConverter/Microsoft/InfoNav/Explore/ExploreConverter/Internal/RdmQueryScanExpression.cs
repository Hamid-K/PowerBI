using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000084 RID: 132
	internal sealed class RdmQueryScanExpression : IRdmQueryExpression
	{
		// Token: 0x0600028E RID: 654 RVA: 0x0000C5EC File Offset: 0x0000A7EC
		internal RdmQueryScanExpression(string target)
		{
			this._target = target;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000C5FB File Offset: 0x0000A7FB
		internal string Target
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000C603 File Offset: 0x0000A803
		public void FindFormulaComponents(FormulaParserContext context)
		{
			context.EntityName = this._target;
		}

		// Token: 0x040001A2 RID: 418
		private readonly string _target;
	}
}
