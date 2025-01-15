using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000E2 RID: 226
	internal interface INamedProjection : INamedItem
	{
		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000DD6 RID: 3542
		QueryExpression Expression { get; }
	}
}
