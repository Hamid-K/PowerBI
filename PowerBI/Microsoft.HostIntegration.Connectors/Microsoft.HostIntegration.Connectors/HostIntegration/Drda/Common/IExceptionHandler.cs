using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000817 RID: 2071
	public interface IExceptionHandler
	{
		// Token: 0x060041A4 RID: 16804
		bool HandleException(Exception ex, DdmReader reader, DdmWriter writer);
	}
}
