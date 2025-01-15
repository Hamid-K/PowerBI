using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200001A RID: 26
	public interface IEdmTypeDefinitionReference : IEdmTypeReference, IEdmElement
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000099 RID: 153
		bool IsUnbounded { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600009A RID: 154
		int? MaxLength { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600009B RID: 155
		bool? IsUnicode { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600009C RID: 156
		int? Precision { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600009D RID: 157
		int? Scale { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600009E RID: 158
		int? SpatialReferenceIdentifier { get; }
	}
}
