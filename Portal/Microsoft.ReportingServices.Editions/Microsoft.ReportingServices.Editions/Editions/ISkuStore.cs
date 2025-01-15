using System;

namespace Microsoft.ReportingServices.Editions
{
	// Token: 0x02000007 RID: 7
	public interface ISkuStore
	{
		// Token: 0x0600001B RID: 27
		SkuInfo Load(string instanceName);

		// Token: 0x0600001C RID: 28
		void Save(SkuInfo info);
	}
}
