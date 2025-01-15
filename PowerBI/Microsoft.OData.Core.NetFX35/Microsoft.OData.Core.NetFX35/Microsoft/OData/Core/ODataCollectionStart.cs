using System;

namespace Microsoft.OData.Core
{
	// Token: 0x02000153 RID: 339
	public sealed class ODataCollectionStart : ODataAnnotatable
	{
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x000304DB File Offset: 0x0002E6DB
		// (set) Token: 0x06000CD3 RID: 3283 RVA: 0x000304E3 File Offset: 0x0002E6E3
		public string Name { get; set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x000304EC File Offset: 0x0002E6EC
		// (set) Token: 0x06000CD5 RID: 3285 RVA: 0x000304F4 File Offset: 0x0002E6F4
		public long? Count { get; set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x000304FD File Offset: 0x0002E6FD
		// (set) Token: 0x06000CD7 RID: 3287 RVA: 0x00030505 File Offset: 0x0002E705
		public Uri NextPageLink { get; set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0003050E File Offset: 0x0002E70E
		// (set) Token: 0x06000CD9 RID: 3289 RVA: 0x00030516 File Offset: 0x0002E716
		internal ODataCollectionStartSerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = ODataCollectionStartSerializationInfo.Validate(value);
			}
		}

		// Token: 0x04000561 RID: 1377
		private ODataCollectionStartSerializationInfo serializationInfo;
	}
}
