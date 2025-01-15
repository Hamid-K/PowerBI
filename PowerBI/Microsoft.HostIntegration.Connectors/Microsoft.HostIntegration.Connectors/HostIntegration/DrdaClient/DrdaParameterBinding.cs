using System;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009FE RID: 2558
	internal class DrdaParameterBinding : DrdaBinding
	{
		// Token: 0x06005068 RID: 20584 RVA: 0x001420AD File Offset: 0x001402AD
		public bool Initialize(bool UseHIS2013Constants)
		{
			return base.InferProperties(UseHIS2013Constants);
		}
	}
}
