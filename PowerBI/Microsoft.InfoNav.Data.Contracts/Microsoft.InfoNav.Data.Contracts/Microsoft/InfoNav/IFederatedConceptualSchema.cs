using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav
{
	// Token: 0x02000046 RID: 70
	public interface IFederatedConceptualSchema
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000128 RID: 296
		IReadOnlyCollection<IConceptualSchema> Schemas { get; }

		// Token: 0x06000129 RID: 297
		bool TryGetSchema(string schemaId, out IConceptualSchema schema);
	}
}
