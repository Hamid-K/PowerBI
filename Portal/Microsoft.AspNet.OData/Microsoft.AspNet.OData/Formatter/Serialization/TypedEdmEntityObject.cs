using System;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x0200019E RID: 414
	internal class TypedEdmEntityObject : TypedEdmStructuredObject, IEdmEntityObject, IEdmStructuredObject, IEdmObject
	{
		// Token: 0x06000DB0 RID: 3504 RVA: 0x00037049 File Offset: 0x00035249
		public TypedEdmEntityObject(object instance, IEdmEntityTypeReference edmType, IEdmModel edmModel)
			: base(instance, edmType, edmModel)
		{
		}
	}
}
