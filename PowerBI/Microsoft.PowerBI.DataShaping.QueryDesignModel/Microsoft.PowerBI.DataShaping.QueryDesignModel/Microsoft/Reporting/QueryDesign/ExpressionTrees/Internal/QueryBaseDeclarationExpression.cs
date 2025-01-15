using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000164 RID: 356
	internal abstract class QueryBaseDeclarationExpression : QueryExtensionExpressionBase
	{
		// Token: 0x0600143B RID: 5179 RVA: 0x0003A999 File Offset: 0x00038B99
		internal QueryBaseDeclarationExpression(ConceptualResultType resultType)
			: base(resultType)
		{
		}
	}
}
