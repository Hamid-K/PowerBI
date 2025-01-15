using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x0200039C RID: 924
	[DataContract]
	public class SerializedProvidersManifests
	{
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x0006C22A File Offset: 0x0006A42A
		// (set) Token: 0x06001C73 RID: 7283 RVA: 0x0006C232 File Offset: 0x0006A432
		[DataMember(Order = 0)]
		public List<string> SerializedManifests { get; private set; }

		// Token: 0x06001C74 RID: 7284 RVA: 0x0006C23B File Offset: 0x0006A43B
		public SerializedProvidersManifests(IEnumerable<string> manifests)
		{
			this.SerializedManifests = manifests.ToList<string>();
		}
	}
}
