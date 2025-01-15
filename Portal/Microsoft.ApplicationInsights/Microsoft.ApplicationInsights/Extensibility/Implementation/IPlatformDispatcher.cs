using System;
using System.Threading.Tasks;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x0200006E RID: 110
	internal interface IPlatformDispatcher
	{
		// Token: 0x0600035D RID: 861
		Task RunAsync(Action action);
	}
}
