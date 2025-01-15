using System;
using System.Diagnostics;
using System.Threading;

namespace Microsoft.Mashup.Shims.Debug
{
	// Token: 0x0200001A RID: 26
	public static class DebuggingUtilities
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002603 File Offset: 0x00000803
		public static void WaitForDebuggerAttach()
		{
			while (!Debugger.IsAttached)
			{
				Thread.Sleep(1000);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002618 File Offset: 0x00000818
		public static void WaitForDebuggerAttachAndBreak()
		{
			DebuggingUtilities.WaitForDebuggerAttach();
			Debugger.Break();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002624 File Offset: 0x00000824
		public static void WaitForDebuggerAttachAndBreakIfEnabled(string enabledEnvironmentVariable)
		{
			bool flag = false;
			bool.TryParse(Environment.GetEnvironmentVariable(enabledEnvironmentVariable), out flag);
			if (flag)
			{
				DebuggingUtilities.WaitForDebuggerAttachAndBreak();
			}
		}
	}
}
