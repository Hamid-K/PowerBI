using System;

namespace Microsoft.Cloud.Platform.CommunicationFramework.Attributes
{
	// Token: 0x0200047B RID: 1147
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class ECFResponseHeaderAttribute : Attribute
	{
		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06002399 RID: 9113 RVA: 0x000809C2 File Offset: 0x0007EBC2
		// (set) Token: 0x0600239A RID: 9114 RVA: 0x000809CA File Offset: 0x0007EBCA
		public string HeaderName { get; private set; }

		// Token: 0x0600239B RID: 9115 RVA: 0x000809D3 File Offset: 0x0007EBD3
		public ECFResponseHeaderAttribute(string headerName)
		{
			this.HeaderName = headerName;
		}
	}
}
