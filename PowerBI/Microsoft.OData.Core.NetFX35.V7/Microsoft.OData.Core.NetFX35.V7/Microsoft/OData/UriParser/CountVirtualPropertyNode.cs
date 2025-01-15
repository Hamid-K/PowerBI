using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001B1 RID: 433
	public sealed class CountVirtualPropertyNode : SingleValueNode
	{
		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x00029054 File Offset: 0x00027254
		public override QueryNodeKind Kind
		{
			get
			{
				return QueryNodeKind.Count;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x0003071D File Offset: 0x0002E91D
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return EdmCoreModel.Instance.GetPrimitive(EdmPrimitiveTypeKind.Int64, false);
			}
		}
	}
}
