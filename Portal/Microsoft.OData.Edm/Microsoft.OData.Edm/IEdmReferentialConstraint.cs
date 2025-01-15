using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000028 RID: 40
	public interface IEdmReferentialConstraint
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000B7 RID: 183
		IEnumerable<EdmReferentialConstraintPropertyPair> PropertyPairs { get; }
	}
}
