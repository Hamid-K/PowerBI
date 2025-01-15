using System;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x02000010 RID: 16
	internal class CreateInstanceCallSite : IServiceCallSite
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000358B File Offset: 0x0000178B
		internal ServiceDescriptor Descriptor { get; }

		// Token: 0x06000087 RID: 135 RVA: 0x00003593 File Offset: 0x00001793
		public CreateInstanceCallSite(ServiceDescriptor descriptor)
		{
			this.Descriptor = descriptor;
		}
	}
}
