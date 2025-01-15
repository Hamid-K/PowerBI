using System;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000129 RID: 297
	internal sealed class DaxExpressionWithSourceLocation
	{
		// Token: 0x0600106F RID: 4207 RVA: 0x0002CF3B File Offset: 0x0002B13B
		public DaxExpressionWithSourceLocation(DaxExpression expression, QueryItemSourceLocation sourceLocation)
		{
			this.Expression = expression;
			this.SourceLocation = sourceLocation;
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x0002CF51 File Offset: 0x0002B151
		public DaxExpression Expression { get; }

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x0002CF59 File Offset: 0x0002B159
		public QueryItemSourceLocation SourceLocation { get; }
	}
}
