using System;
using System.Collections.Generic;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200018E RID: 398
	internal interface IGroupItem : IEquatable<IGroupItem>
	{
		// Token: 0x0600154F RID: 5455
		IEnumerable<KeyValuePair<string, QueryExpression>> GetGroupKeys();
	}
}
