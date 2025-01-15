using System;

namespace Azure.Core
{
	// Token: 0x02000014 RID: 20
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	internal class AzureResourceProviderNamespaceAttribute : Attribute
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002A94 File Offset: 0x00000C94
		public string ResourceProviderNamespace { get; }

		// Token: 0x06000053 RID: 83 RVA: 0x00002A9C File Offset: 0x00000C9C
		public AzureResourceProviderNamespaceAttribute(string resourceProviderNamespace)
		{
			this.ResourceProviderNamespace = resourceProviderNamespace;
		}
	}
}
