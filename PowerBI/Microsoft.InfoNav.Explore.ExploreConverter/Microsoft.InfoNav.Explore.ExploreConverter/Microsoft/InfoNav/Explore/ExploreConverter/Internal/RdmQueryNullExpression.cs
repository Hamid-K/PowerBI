using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000082 RID: 130
	internal sealed class RdmQueryNullExpression : IRdmQueryExpression
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000C578 File Offset: 0x0000A778
		public void FindFormulaComponents(FormulaParserContext context)
		{
			context.Literal = NullPrimitiveValue.Instance;
		}
	}
}
