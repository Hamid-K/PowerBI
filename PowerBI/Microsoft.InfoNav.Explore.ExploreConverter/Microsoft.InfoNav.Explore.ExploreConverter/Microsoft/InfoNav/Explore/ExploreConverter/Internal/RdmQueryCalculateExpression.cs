using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200007B RID: 123
	internal sealed class RdmQueryCalculateExpression : IRdmQueryExpression
	{
		// Token: 0x06000265 RID: 613 RVA: 0x0000BEFD File Offset: 0x0000A0FD
		internal RdmQueryCalculateExpression(IRdmQueryExpression argument)
		{
			this._argument = argument;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000BF0C File Offset: 0x0000A10C
		internal IRdmQueryExpression Argument
		{
			get
			{
				return this._argument;
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000BF14 File Offset: 0x0000A114
		public void FindFormulaComponents(FormulaParserContext context)
		{
			if (this._argument != null)
			{
				this._argument.FindFormulaComponents(context);
			}
		}

		// Token: 0x04000194 RID: 404
		private readonly IRdmQueryExpression _argument;
	}
}
