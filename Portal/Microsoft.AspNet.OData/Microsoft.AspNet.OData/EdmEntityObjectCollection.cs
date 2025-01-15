using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200003C RID: 60
	[NonValidatingParameterBinding]
	public class EdmEntityObjectCollection : Collection<IEdmEntityObject>, IEdmObject
	{
		// Token: 0x06000178 RID: 376 RVA: 0x0000750A File Offset: 0x0000570A
		public EdmEntityObjectCollection(IEdmCollectionTypeReference edmType)
		{
			this.Initialize(edmType);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007519 File Offset: 0x00005719
		public EdmEntityObjectCollection(IEdmCollectionTypeReference edmType, IList<IEdmEntityObject> list)
			: base(list)
		{
			this.Initialize(edmType);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007529 File Offset: 0x00005729
		public IEdmTypeReference GetEdmType()
		{
			return this._edmType;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007534 File Offset: 0x00005734
		private void Initialize(IEdmCollectionTypeReference edmType)
		{
			if (edmType == null)
			{
				throw Error.ArgumentNull("edmType");
			}
			if (!edmType.ElementType().IsEntity())
			{
				throw Error.Argument("edmType", SRResources.UnexpectedElementType, new object[]
				{
					edmType.ElementType().ToTraceString(),
					edmType.ToTraceString(),
					typeof(IEdmEntityType).Name
				});
			}
			this._edmType = edmType;
		}

		// Token: 0x04000065 RID: 101
		private IEdmCollectionTypeReference _edmType;
	}
}
