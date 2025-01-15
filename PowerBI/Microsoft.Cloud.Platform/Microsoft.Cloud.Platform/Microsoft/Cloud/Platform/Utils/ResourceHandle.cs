using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000248 RID: 584
	public class ResourceHandle<TKey, TValue> : IResourceHandle<TValue> where TKey : IEquatable<TKey>
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x0003384C File Offset: 0x00031A4C
		// (set) Token: 0x06000EF4 RID: 3828 RVA: 0x00033854 File Offset: 0x00031A54
		internal Resource<TValue> Resource { get; private set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0003385D File Offset: 0x00031A5D
		// (set) Token: 0x06000EF6 RID: 3830 RVA: 0x00033865 File Offset: 0x00031A65
		internal TKey Key { get; private set; }

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0003386E File Offset: 0x00031A6E
		internal ResourceHandle(TKey key, Resource<TValue> resource)
		{
			this.Key = key;
			this.Value = resource.ResourceValue;
			this.Resource = resource;
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x00033890 File Offset: 0x00031A90
		// (set) Token: 0x06000EF9 RID: 3833 RVA: 0x00033898 File Offset: 0x00031A98
		public TValue Value { get; private set; }
	}
}
