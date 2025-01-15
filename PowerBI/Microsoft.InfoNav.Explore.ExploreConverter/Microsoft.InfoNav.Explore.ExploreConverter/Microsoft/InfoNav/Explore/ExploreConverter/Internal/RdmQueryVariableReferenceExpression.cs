using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000085 RID: 133
	internal sealed class RdmQueryVariableReferenceExpression : IRdmQueryExpression
	{
		// Token: 0x06000291 RID: 657 RVA: 0x0000C611 File Offset: 0x0000A811
		internal RdmQueryVariableReferenceExpression(string variableName)
		{
			this._variableName = variableName;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000C620 File Offset: 0x0000A820
		internal string VariableName
		{
			get
			{
				return this._variableName;
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000C628 File Offset: 0x0000A828
		public void FindFormulaComponents(FormulaParserContext context)
		{
		}

		// Token: 0x040001A3 RID: 419
		private readonly string _variableName;
	}
}
