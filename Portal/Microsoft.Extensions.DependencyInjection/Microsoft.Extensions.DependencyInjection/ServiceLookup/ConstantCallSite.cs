using System;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x0200000E RID: 14
	internal class ConstantCallSite : IServiceCallSite
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000354E File Offset: 0x0000174E
		internal object DefaultValue { get; }

		// Token: 0x06000082 RID: 130 RVA: 0x00003556 File Offset: 0x00001756
		public ConstantCallSite(object defaultValue)
		{
			this.DefaultValue = defaultValue;
		}
	}
}
