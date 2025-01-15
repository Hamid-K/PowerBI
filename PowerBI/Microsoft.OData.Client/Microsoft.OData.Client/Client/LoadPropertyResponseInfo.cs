using System;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x02000065 RID: 101
	internal class LoadPropertyResponseInfo : ResponseInfo
	{
		// Token: 0x06000377 RID: 887 RVA: 0x0000D506 File Offset: 0x0000B706
		internal LoadPropertyResponseInfo(RequestInfo requestInfo, MergeOption mergeOption, EntityDescriptor entityDescriptor, ClientPropertyAnnotation property)
			: base(requestInfo, mergeOption)
		{
			this.EntityDescriptor = entityDescriptor;
			this.Property = property;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000D51F File Offset: 0x0000B71F
		// (set) Token: 0x06000379 RID: 889 RVA: 0x0000D527 File Offset: 0x0000B727
		internal EntityDescriptor EntityDescriptor { get; private set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000D530 File Offset: 0x0000B730
		// (set) Token: 0x0600037B RID: 891 RVA: 0x0000D538 File Offset: 0x0000B738
		internal ClientPropertyAnnotation Property { get; private set; }
	}
}
