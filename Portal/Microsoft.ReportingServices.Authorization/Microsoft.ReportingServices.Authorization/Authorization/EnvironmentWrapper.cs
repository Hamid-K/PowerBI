using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x0200000E RID: 14
	internal class EnvironmentWrapper : IEnvironmentWrapper
	{
		// Token: 0x06000035 RID: 53 RVA: 0x000028ED File Offset: 0x00000AED
		public bool IsClientLocal()
		{
			return WebRequestUtil.IsClientLocal();
		}
	}
}
