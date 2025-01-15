using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200007E RID: 126
	internal sealed class RdmQueryFunctionExpression : IRdmQueryExpression
	{
		// Token: 0x06000277 RID: 631 RVA: 0x0000C406 File Offset: 0x0000A606
		internal RdmQueryFunctionExpression(string functionName, List<IRdmQueryExpression> arguments)
		{
			this._functionName = functionName;
			this._arguments = arguments;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000C41C File Offset: 0x0000A61C
		internal string FunctionName
		{
			get
			{
				return this._functionName;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000C424 File Offset: 0x0000A624
		internal List<IRdmQueryExpression> Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000C42C File Offset: 0x0000A62C
		public void FindFormulaComponents(FormulaParserContext context)
		{
			if (this._arguments != null)
			{
				foreach (IRdmQueryExpression rdmQueryExpression in this._arguments)
				{
					if (rdmQueryExpression != null)
					{
						rdmQueryExpression.FindFormulaComponents(context);
					}
				}
			}
			context.FunctionName = this._functionName;
		}

		// Token: 0x04000197 RID: 407
		private readonly string _functionName;

		// Token: 0x04000198 RID: 408
		private readonly List<IRdmQueryExpression> _arguments;
	}
}
