using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.PlatformHost
{
	// Token: 0x02000033 RID: 51
	[Guid("988C16BF-8C02-44CF-B772-9539C3067BA0")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	public interface IEngineTracer
	{
		// Token: 0x060020FC RID: 8444
		void LogMessage(string message);

		// Token: 0x060020FD RID: 8445
		void LogMessage(Guid rootActivityId, Guid parentActivityId, string message);

		// Token: 0x060020FE RID: 8446
		void LogPrivateMessage(string message);

		// Token: 0x060020FF RID: 8447
		void LogPrivateMessage(Guid rootActivityId, Guid parentActivityId, string message);
	}
}
