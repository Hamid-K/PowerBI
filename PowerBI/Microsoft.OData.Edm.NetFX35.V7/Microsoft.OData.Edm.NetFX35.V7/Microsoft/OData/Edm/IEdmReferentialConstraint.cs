using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000B6 RID: 182
	public interface IEdmReferentialConstraint
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004AC RID: 1196
		IEnumerable<EdmReferentialConstraintPropertyPair> PropertyPairs { get; }
	}
}
