using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000F3 RID: 243
	public interface IEdmReferentialConstraint
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060004CE RID: 1230
		IEnumerable<EdmReferentialConstraintPropertyPair> PropertyPairs { get; }
	}
}
