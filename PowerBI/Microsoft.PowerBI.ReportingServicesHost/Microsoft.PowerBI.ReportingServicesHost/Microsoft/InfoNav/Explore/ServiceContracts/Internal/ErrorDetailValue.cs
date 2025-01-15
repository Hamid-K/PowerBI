using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ServiceContracts.Internal
{
	// Token: 0x0200000B RID: 11
	[DataContract]
	public sealed class ErrorDetailValue
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002237 File Offset: 0x00000437
		public ErrorDetailValue(ErrorResourceType resourceType, string resourceValue)
		{
			this.ResourceType = resourceType;
			this.ResourceValue = resourceValue;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000224D File Offset: 0x0000044D
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002255 File Offset: 0x00000455
		[DataMember(IsRequired = true, Order = 10, Name = "type")]
		public ErrorResourceType ResourceType { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000225E File Offset: 0x0000045E
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002266 File Offset: 0x00000466
		[DataMember(IsRequired = true, Order = 20, Name = "value")]
		public string ResourceValue { get; private set; }
	}
}
