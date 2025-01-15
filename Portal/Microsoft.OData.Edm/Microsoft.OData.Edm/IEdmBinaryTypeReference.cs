using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000081 RID: 129
	public interface IEdmBinaryTypeReference : IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600038B RID: 907
		bool IsUnbounded { get; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600038C RID: 908
		int? MaxLength { get; }
	}
}
