using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001AC RID: 428
	public abstract class SingleValueNode : QueryNode
	{
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x0600144F RID: 5199
		public abstract IEdmTypeReference TypeReference { get; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x0003872F File Offset: 0x0003692F
		public override QueryNodeKind Kind
		{
			get
			{
				return (QueryNodeKind)this.InternalKind;
			}
		}
	}
}
