using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Communication
{
	// Token: 0x02000457 RID: 1111
	[Serializable]
	public sealed class UrisRouterSectionConfiguration : ConfigurationClass
	{
		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060022A4 RID: 8868 RVA: 0x0007E404 File Offset: 0x0007C604
		// (set) Token: 0x060022A5 RID: 8869 RVA: 0x0007E40C File Offset: 0x0007C60C
		[ConfigurationProperty]
		public string Id { get; set; }

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060022A6 RID: 8870 RVA: 0x0007E415 File Offset: 0x0007C615
		// (set) Token: 0x060022A7 RID: 8871 RVA: 0x0007E41D File Offset: 0x0007C61D
		[ConfigurationProperty]
		public ConfigurationCollection<string> Uris { get; set; }

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x0007E426 File Offset: 0x0007C626
		// (set) Token: 0x060022A9 RID: 8873 RVA: 0x0007E42E File Offset: 0x0007C62E
		[ConfigurationProperty]
		public ConfigurationCollection<TypeIdentifier> RetryToDifferentEndPointExceptions { get; set; }

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060022AA RID: 8874 RVA: 0x0007E437 File Offset: 0x0007C637
		// (set) Token: 0x060022AB RID: 8875 RVA: 0x0007E43F File Offset: 0x0007C63F
		[ConfigurationProperty]
		public ConfigurationCollection<TypeIdentifier> RetryToSameEndPointExceptions { get; set; }
	}
}
