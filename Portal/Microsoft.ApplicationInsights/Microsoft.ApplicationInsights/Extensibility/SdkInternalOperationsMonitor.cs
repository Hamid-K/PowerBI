using System;
using System.Runtime.Remoting.Messaging;

namespace Microsoft.ApplicationInsights.Extensibility
{
	// Token: 0x0200005B RID: 91
	public static class SdkInternalOperationsMonitor
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x0000D170 File Offset: 0x0000B370
		public static bool IsEntered()
		{
			object obj = null;
			try
			{
				obj = CallContext.LogicalGetData("Microsoft.ApplicationInsights.InternalSdkOperation");
			}
			catch (Exception)
			{
			}
			return obj != null;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000D1A4 File Offset: 0x0000B3A4
		public static void Enter()
		{
			try
			{
				CallContext.LogicalSetData("Microsoft.ApplicationInsights.InternalSdkOperation", SdkInternalOperationsMonitor.syncObj);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000D1D8 File Offset: 0x0000B3D8
		public static void Exit()
		{
			try
			{
				CallContext.FreeNamedDataSlot("Microsoft.ApplicationInsights.InternalSdkOperation");
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x04000121 RID: 289
		internal const string InternalOperationsMonitorSlotName = "Microsoft.ApplicationInsights.InternalSdkOperation";

		// Token: 0x04000122 RID: 290
		private static object syncObj = new object();
	}
}
