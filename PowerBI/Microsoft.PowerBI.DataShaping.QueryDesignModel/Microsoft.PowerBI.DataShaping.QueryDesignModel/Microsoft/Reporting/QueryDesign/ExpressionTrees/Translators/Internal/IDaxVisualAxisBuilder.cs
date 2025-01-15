using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000148 RID: 328
	internal interface IDaxVisualAxisBuilder
	{
		// Token: 0x060011AF RID: 4527
		void Group(IReadOnlyList<DaxExpression> keys, DaxExpression isSubtotal);

		// Token: 0x060011B0 RID: 4528
		void OrderBy([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "Direction" })] IReadOnlyList<global::System.ValueTuple<DaxExpression, SortDirection>> orderBy);
	}
}
