using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000025 RID: 37
	[NonValidatingParameterBinding]
	public class EdmChangedObjectCollection : Collection<IEdmChangedObject>, IEdmObject
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00005FCF File Offset: 0x000041CF
		public EdmChangedObjectCollection(IEdmEntityType entityType)
			: base(Enumerable.Empty<IEdmChangedObject>().ToList<IEdmChangedObject>())
		{
			this.Initialize(entityType);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005FE8 File Offset: 0x000041E8
		public EdmChangedObjectCollection(IEdmEntityType entityType, IList<IEdmChangedObject> changedObjectList)
			: base(changedObjectList)
		{
			this.Initialize(entityType);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005FF8 File Offset: 0x000041F8
		public IEdmTypeReference GetEdmType()
		{
			return this._edmTypeReference;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006000 File Offset: 0x00004200
		private void Initialize(IEdmEntityType entityType)
		{
			if (entityType == null)
			{
				throw Error.ArgumentNull("entityType");
			}
			this._entityType = entityType;
			this._edmType = new EdmDeltaCollectionType(new EdmEntityTypeReference(this._entityType, true));
			this._edmTypeReference = new EdmCollectionTypeReference(this._edmType);
		}

		// Token: 0x0400003A RID: 58
		private IEdmEntityType _entityType;

		// Token: 0x0400003B RID: 59
		private EdmDeltaCollectionType _edmType;

		// Token: 0x0400003C RID: 60
		private IEdmCollectionTypeReference _edmTypeReference;
	}
}
