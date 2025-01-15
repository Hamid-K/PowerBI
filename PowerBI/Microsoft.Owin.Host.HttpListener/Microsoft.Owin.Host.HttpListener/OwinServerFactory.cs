using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace Microsoft.Owin.Host.HttpListener
{
	// Token: 0x02000008 RID: 8
	public static class OwinServerFactory
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002A4C File Offset: 0x00000C4C
		public static void Initialize(IDictionary<string, object> properties)
		{
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			properties["owin.Version"] = "1.0";
			IDictionary<string, object> capabilities = properties.Get("server.Capabilities") ?? new Dictionary<string, object>();
			properties["server.Capabilities"] = capabilities;
			OwinServerFactory.DetectWebSocketSupport(properties);
			OwinHttpListener wrapper = new OwinHttpListener();
			properties[typeof(OwinHttpListener).FullName] = wrapper;
			properties[typeof(HttpListener).FullName] = wrapper.Listener;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002AD8 File Offset: 0x00000CD8
		private static void DetectWebSocketSupport(IDictionary<string, object> properties)
		{
			if (Environment.OSVersion.Version >= new Version(6, 2))
			{
				IDictionary<string, object> capabilities = properties.Get("server.Capabilities");
				capabilities["websocket.Version"] = "1.0";
				return;
			}
			Func<string, Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool>> loggerFactory = properties.Get("server.LoggerFactory");
			Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool> logger = LogHelper.CreateLogger(loggerFactory, typeof(OwinServerFactory));
			LogHelper.LogInfo(logger, "No WebSocket support detected, Windows 8 or later is required.");
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002B44 File Offset: 0x00000D44
		public static IDisposable Create(Func<IDictionary<string, object>, Task> app, IDictionary<string, object> properties)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			OwinHttpListener wrapper = properties.Get(typeof(OwinHttpListener).FullName) ?? new OwinHttpListener();
			HttpListener listener = properties.Get(typeof(HttpListener).FullName) ?? new HttpListener();
			IList<IDictionary<string, object>> addresses = properties.Get("host.Addresses") ?? new List<IDictionary<string, object>>();
			IDictionary<string, object> capabilities = properties.Get("server.Capabilities") ?? new Dictionary<string, object>();
			Func<string, Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool>> loggerFactory = properties.Get("server.LoggerFactory");
			wrapper.Start(listener, app, addresses, capabilities, loggerFactory);
			return wrapper;
		}
	}
}
