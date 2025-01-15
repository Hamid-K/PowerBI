using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200010A RID: 266
	public sealed class CountVirtualPropertyNode : SingleValueNode
	{
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x00026039 File Offset: 0x00024239
		public override QueryNodeKind Kind
		{
			get
			{
				return QueryNodeKind.Count;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x0002603D File Offset: 0x0002423D
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return EdmCoreModel.Instance.GetPrimitive(EdmPrimitiveTypeKind.Int64, false);
			}
		}
	}
}
