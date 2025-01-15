using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000026 RID: 38
	internal class EdmDeltaCollectionType : IEdmCollectionType, IEdmType, IEdmElement
	{
		// Token: 0x06000104 RID: 260 RVA: 0x0000603F File Offset: 0x0000423F
		internal EdmDeltaCollectionType(IEdmTypeReference entityTypeReference)
		{
			if (entityTypeReference == null)
			{
				throw Error.ArgumentNull("entityTypeReference");
			}
			this._entityTypeReference = entityTypeReference;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000605C File Offset: 0x0000425C
		public EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Collection;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000605F File Offset: 0x0000425F
		public IEdmTypeReference ElementType
		{
			get
			{
				return this._entityTypeReference;
			}
		}

		// Token: 0x0400003D RID: 61
		private IEdmTypeReference _entityTypeReference;
	}
}
