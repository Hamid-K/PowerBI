using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x02000020 RID: 32
	[ComVisible(true)]
	[Guid("6BD16599-9BC6-4719-B416-D8A5ED78CC65")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IMEngineSessionHandle
	{
		// Token: 0x0600007F RID: 127
		void Close();

		// Token: 0x06000080 RID: 128
		string GetUpdatedDSRCredential(string resourcePath, string dataSourceCredential);
	}
}
