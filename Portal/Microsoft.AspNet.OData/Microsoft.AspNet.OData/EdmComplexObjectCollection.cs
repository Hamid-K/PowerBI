using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200003A RID: 58
	[NonValidatingParameterBinding]
	public class EdmComplexObjectCollection : Collection<IEdmComplexObject>, IEdmObject
	{
		// Token: 0x06000171 RID: 369 RVA: 0x0000744C File Offset: 0x0000564C
		public EdmComplexObjectCollection(IEdmCollectionTypeReference edmType)
		{
			this.Initialize(edmType);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000745B File Offset: 0x0000565B
		public EdmComplexObjectCollection(IEdmCollectionTypeReference edmType, IList<IEdmComplexObject> list)
			: base(list)
		{
			this.Initialize(edmType);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000746B File Offset: 0x0000566B
		public IEdmTypeReference GetEdmType()
		{
			return this._edmType;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007474 File Offset: 0x00005674
		private void Initialize(IEdmCollectionTypeReference edmType)
		{
			if (edmType == null)
			{
				throw Error.ArgumentNull("edmType");
			}
			if (!edmType.ElementType().IsComplex())
			{
				throw Error.Argument("edmType", SRResources.UnexpectedElementType, new object[]
				{
					edmType.ElementType().ToTraceString(),
					edmType.ToTraceString(),
					typeof(IEdmComplexType).Name
				});
			}
			this._edmType = edmType;
		}

		// Token: 0x04000064 RID: 100
		private IEdmCollectionTypeReference _edmType;
	}
}
