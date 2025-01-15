using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000108 RID: 264
	public interface IEdmRecordExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600076C RID: 1900
		IEdmStructuredTypeReference DeclaredType { get; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600076D RID: 1901
		IEnumerable<IEdmPropertyConstructor> Properties { get; }
	}
}
