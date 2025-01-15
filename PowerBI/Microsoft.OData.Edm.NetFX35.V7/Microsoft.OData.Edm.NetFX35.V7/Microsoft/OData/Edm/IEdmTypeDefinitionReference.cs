using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000C7 RID: 199
	public interface IEdmTypeDefinitionReference : IEdmTypeReference, IEdmElement
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060004C1 RID: 1217
		bool IsUnbounded { get; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060004C2 RID: 1218
		int? MaxLength { get; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060004C3 RID: 1219
		bool? IsUnicode { get; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060004C4 RID: 1220
		int? Precision { get; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060004C5 RID: 1221
		int? Scale { get; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060004C6 RID: 1222
		int? SpatialReferenceIdentifier { get; }
	}
}
