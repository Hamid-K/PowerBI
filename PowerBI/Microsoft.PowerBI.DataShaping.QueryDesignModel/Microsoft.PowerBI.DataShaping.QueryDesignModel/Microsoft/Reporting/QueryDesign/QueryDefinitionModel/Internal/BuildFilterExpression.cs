using System;
using System.Collections.Generic;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000F9 RID: 249
	// (Invoke) Token: 0x06000E6D RID: 3693
	internal delegate QueryExpression BuildFilterExpression(EntitySet entitySet, ISet<IEdmFieldInstance> groupingFields, QueryExpression predicate, IConceptualEntity entity = null, ISet<IConceptualColumn> groupingColumns = null);
}
