using System;
using Microsoft.Data.Metadata.Edm;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001E0 RID: 480
	internal sealed class CollectionType : EdmType
	{
		// Token: 0x060016C8 RID: 5832 RVA: 0x0003EBA1 File Offset: 0x0003CDA1
		internal CollectionType(CollectionType collectionType)
		{
			this._collectionType = ArgumentValidation.CheckNotNull<CollectionType>(collectionType, "collectionType");
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x0003EBBA File Offset: 0x0003CDBA
		internal sealed override EdmType InternalEdmType
		{
			get
			{
				return this._collectionType;
			}
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x0003EBC2 File Offset: 0x0003CDC2
		internal static CollectionType Create(CollectionType collectionType)
		{
			return new CollectionType(collectionType);
		}

		// Token: 0x04000C3A RID: 3130
		private readonly CollectionType _collectionType;
	}
}
