using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200002D RID: 45
	public static class EdmTypeExtensions
	{
		// Token: 0x0600012F RID: 303 RVA: 0x00006250 File Offset: 0x00004450
		public static bool IsDeltaFeed(this IEdmType type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			return type.GetType() == typeof(EdmDeltaCollectionType);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006275 File Offset: 0x00004475
		public static bool IsDeltaResource(this IEdmObject resource)
		{
			if (resource == null)
			{
				throw Error.ArgumentNull("resource");
			}
			return resource is EdmDeltaEntityObject || resource is EdmDeltaComplexObject;
		}
	}
}
