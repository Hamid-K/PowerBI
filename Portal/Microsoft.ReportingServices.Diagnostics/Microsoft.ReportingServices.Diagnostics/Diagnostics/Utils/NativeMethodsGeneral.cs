using System;
using System.Diagnostics;
using System.Web;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics.Utils
{
	// Token: 0x02000081 RID: 129
	internal static class NativeMethodsGeneral
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x00010678 File Offset: 0x0000E878
		public static void RecycleOnSevereException(Exception x)
		{
			Exception ex = x;
			while (!(x is StackOverflowException))
			{
				x = x.InnerException;
				if (x == null)
				{
					return;
				}
			}
			if (RSTrace.CatalogTrace.TraceError)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Recycling appdomain {0} on severe exception {1}: {2}", new object[]
				{
					AppDomain.CurrentDomain.FriendlyName,
					x.GetType().ToString(),
					ex.ToString()
				});
			}
			HttpRuntime.UnloadAppDomain();
		}
	}
}
