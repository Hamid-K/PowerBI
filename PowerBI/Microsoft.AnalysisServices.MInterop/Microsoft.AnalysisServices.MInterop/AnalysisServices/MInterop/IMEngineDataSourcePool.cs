using System;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.PlatformHost;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x0200001C RID: 28
	[ComVisible(true)]
	[Guid("911FE31D-AF99-4565-AAF9-50E19CFCF776")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IMEngineDataSourcePool
	{
		// Token: 0x0600006C RID: 108
		void Close();

		// Token: 0x0600006D RID: 109
		EngineErrorInfo AddUsingResourcePath(string resourcePath, int maxConnections);

		// Token: 0x0600006E RID: 110
		EngineErrorInfo AddUsingDSRJson(string dsrJson, int maxConnections);
	}
}
