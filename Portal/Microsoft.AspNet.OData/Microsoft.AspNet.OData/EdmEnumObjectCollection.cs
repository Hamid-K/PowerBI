using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000022 RID: 34
	[NonValidatingParameterBinding]
	public class EdmEnumObjectCollection : Collection<IEdmEnumObject>, IEdmObject
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00005EAF File Offset: 0x000040AF
		public EdmEnumObjectCollection(IEdmCollectionTypeReference edmType)
			: this(edmType, Enumerable.Empty<IEdmEnumObject>().ToList<IEdmEnumObject>())
		{
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005EC2 File Offset: 0x000040C2
		public EdmEnumObjectCollection(IEdmCollectionTypeReference edmType, IList<IEdmEnumObject> list)
			: base(list)
		{
			this.Initialize(edmType);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005ED2 File Offset: 0x000040D2
		public IEdmTypeReference GetEdmType()
		{
			return this._edmType;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005EDC File Offset: 0x000040DC
		private void Initialize(IEdmCollectionTypeReference edmType)
		{
			if (edmType == null)
			{
				throw Error.ArgumentNull("edmType");
			}
			if (!edmType.ElementType().IsEnum())
			{
				throw Error.Argument("edmType", SRResources.UnexpectedElementType, new object[]
				{
					edmType.ElementType().ToTraceString(),
					edmType.ToTraceString(),
					typeof(IEdmEnumType).Name
				});
			}
			this._edmType = edmType;
		}

		// Token: 0x04000036 RID: 54
		private IEdmCollectionTypeReference _edmType;
	}
}
