using System;
using Microsoft.AnalysisServices.PlatformHost;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x0200001D RID: 29
	internal sealed class MEngineDataSourcePool : ManagedErrorHandler, IMEngineDataSourcePool
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003E40 File Offset: 0x00002040
		internal MEngineDataSourcePool(string name, IEngineTracer tracer)
		{
			this.impl = new MEngineDataSourcePoolImpl(name);
			this.engineTracer = tracer;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003E5C File Offset: 0x0000205C
		public void Close()
		{
			using (new MInteropHelper.UILocaleContext())
			{
				if (this.impl != null)
				{
					this.impl.Dispose();
					this.impl = null;
				}
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003EA8 File Offset: 0x000020A8
		public EngineErrorInfo AddUsingResourcePath(string resourcePath, int maxConnections)
		{
			EngineErrorInfo engineErrorInfo;
			using (new MInteropHelper.UILocaleContext())
			{
				engineErrorInfo = this.HandleExceptions(delegate
				{
					this.impl.AddUsingResourcePath(resourcePath, maxConnections);
				}, "AddUsingResourcePath", false, null);
			}
			return engineErrorInfo;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003F10 File Offset: 0x00002110
		public EngineErrorInfo AddUsingDSRJson(string dsrJson, int maxConnections)
		{
			EngineErrorInfo engineErrorInfo;
			using (new MInteropHelper.UILocaleContext())
			{
				Func<Exception, EngineException> func = delegate(Exception exception)
				{
					if (exception is NotSupportedException)
					{
						return EngineException.PFE_M_MAX_CONNECTIONS_UNSUPPORTED_FOR_DATASOURCE();
					}
					return null;
				};
				engineErrorInfo = this.HandleExceptions(delegate
				{
					this.impl.AddUsingDSRJson(dsrJson, maxConnections);
				}, "AddUsingDSRJson", true, func);
			}
			return engineErrorInfo;
		}

		// Token: 0x040000AF RID: 175
		private MEngineDataSourcePoolImpl impl;
	}
}
