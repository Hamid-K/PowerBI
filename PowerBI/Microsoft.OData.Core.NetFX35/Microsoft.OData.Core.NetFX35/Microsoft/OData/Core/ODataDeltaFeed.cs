using System;

namespace Microsoft.OData.Core
{
	// Token: 0x02000164 RID: 356
	public sealed class ODataDeltaFeed : ODataFeedBase
	{
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x0003108F File Offset: 0x0002F28F
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x00031097 File Offset: 0x0002F297
		internal ODataDeltaFeedSerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = ODataDeltaFeedSerializationInfo.Validate(value);
			}
		}

		// Token: 0x040005B6 RID: 1462
		private ODataDeltaFeedSerializationInfo serializationInfo;
	}
}
