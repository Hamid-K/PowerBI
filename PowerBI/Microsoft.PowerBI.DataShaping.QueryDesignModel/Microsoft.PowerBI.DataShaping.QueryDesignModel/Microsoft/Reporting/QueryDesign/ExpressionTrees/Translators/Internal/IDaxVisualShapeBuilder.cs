using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000147 RID: 327
	internal interface IDaxVisualShapeBuilder
	{
		// Token: 0x060011AD RID: 4525
		IDaxVisualAxisBuilder Axis(QueryVisualAxisName name);

		// Token: 0x060011AE RID: 4526
		void Densify(DaxExpression isDensifiedColumnName);
	}
}
