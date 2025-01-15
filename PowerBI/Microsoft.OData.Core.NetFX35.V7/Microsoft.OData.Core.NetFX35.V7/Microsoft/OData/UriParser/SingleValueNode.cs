using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200015E RID: 350
	public abstract class SingleValueNode : QueryNode
	{
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000F18 RID: 3864
		public abstract IEdmTypeReference TypeReference { get; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x00028DFB File Offset: 0x00026FFB
		public override QueryNodeKind Kind
		{
			get
			{
				return (QueryNodeKind)this.InternalKind;
			}
		}
	}
}
