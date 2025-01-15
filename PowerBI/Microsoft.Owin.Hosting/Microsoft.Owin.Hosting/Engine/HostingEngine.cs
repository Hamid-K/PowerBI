using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Microsoft.Owin.Hosting.Builder;
using Microsoft.Owin.Hosting.Loader;
using Microsoft.Owin.Hosting.ServerFactory;
using Microsoft.Owin.Hosting.Tracing;
using Microsoft.Owin.Hosting.Utilities;
using Microsoft.Owin.Logging;

namespace Microsoft.Owin.Hosting.Engine
{
	// Token: 0x02000027 RID: 39
	public class HostingEngine : IHostingEngine
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00004218 File Offset: 0x00002418
		public HostingEngine(IAppBuilderFactory appBuilderFactory, ITraceOutputFactory traceOutputFactory, IAppLoader appLoader, IServerFactoryLoader serverFactoryLoader, ILoggerFactory loggerFactory)
		{
			if (appBuilderFactory == null)
			{
				throw new ArgumentNullException("appBuilderFactory");
			}
			if (traceOutputFactory == null)
			{
				throw new ArgumentNullException("traceOutputFactory");
			}
			if (appLoader == null)
			{
				throw new ArgumentNullException("appLoader");
			}
			if (loggerFactory == null)
			{
				throw new ArgumentNullException("loggerFactory");
			}
			this._appBuilderFactory = appBuilderFactory;
			this._traceOutputFactory = traceOutputFactory;
			this._appLoader = appLoader;
			this._serverFactoryLoader = serverFactoryLoader;
			this._loggerFactory = loggerFactory;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00004289 File Offset: 0x00002489
		public static int DefaultPort
		{
			get
			{
				return 5000;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004290 File Offset: 0x00002490
		public IDisposable Start(StartContext context)
		{
			this.ResolveOutput(context);
			this.InitializeBuilder(context);
			this.EnableTracing(context);
			IDisposable disposablePipeline = HostingEngine.EnableDisposing(context);
			this.ResolveServerFactory(context);
			HostingEngine.InitializeServerFactory(context);
			this.ResolveApp(context);
			IDisposable disposableServer = HostingEngine.StartServer(context);
			return new Disposable(delegate
			{
				try
				{
					disposableServer.Dispose();
				}
				finally
				{
					disposablePipeline.Dispose();
					context.TraceOutput.Flush();
				}
			});
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004324 File Offset: 0x00002524
		public static bool TryDetermineCustomPort(StartOptions options, out int port)
		{
			string portString;
			if (options != null)
			{
				if (options.Port != null)
				{
					port = options.Port.Value;
					return true;
				}
				IDictionary<string, string> settings = options.Settings;
				if (settings == null || !settings.TryGetValue("owin:Port", out portString))
				{
					portString = HostingEngine.GetPortEnvironmentVariable();
				}
			}
			else
			{
				portString = HostingEngine.GetPortEnvironmentVariable();
			}
			return int.TryParse(portString, NumberStyles.Integer, CultureInfo.InvariantCulture, out port);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000438C File Offset: 0x0000258C
		private void ResolveOutput(StartContext context)
		{
			if (context.TraceOutput == null)
			{
				string traceoutput;
				context.Options.Settings.TryGetValue("traceoutput", out traceoutput);
				context.TraceOutput = this._traceOutputFactory.Create(traceoutput);
			}
			context.EnvironmentData.Add(new KeyValuePair<string, object>("host.TraceOutput", context.TraceOutput));
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000043E8 File Offset: 0x000025E8
		private void InitializeBuilder(StartContext context)
		{
			if (context.Builder == null)
			{
				context.Builder = this._appBuilderFactory.Create();
			}
			List<IDictionary<string, object>> addresses = new List<IDictionary<string, object>>();
			foreach (string url in context.Options.Urls)
			{
				string scheme;
				string host;
				string port;
				string path;
				if (HostingEngine.DeconstructUrl(url, out scheme, out host, out port, out path))
				{
					addresses.Add(new Dictionary<string, object>
					{
						{ "scheme", scheme },
						{ "host", host },
						{
							"port",
							port.ToString(CultureInfo.InvariantCulture)
						},
						{ "path", path }
					});
				}
			}
			if (addresses.Count == 0)
			{
				int port2 = HostingEngine.DeterminePort(context);
				addresses.Add(new Dictionary<string, object> { 
				{
					"port",
					port2.ToString(CultureInfo.InvariantCulture)
				} });
			}
			context.Builder.Properties["host.Addresses"] = addresses;
			if (!string.IsNullOrWhiteSpace(context.Options.AppStartup))
			{
				context.Builder.Properties["host.AppName"] = context.Options.AppStartup;
				context.EnvironmentData.Add(new KeyValuePair<string, object>("host.AppName", context.Options.AppStartup));
			}
			string vsVersion = Environment.GetEnvironmentVariable("VisualStudioVersion");
			if (!string.IsNullOrWhiteSpace(vsVersion))
			{
				context.Builder.Properties["host.AppMode"] = "development";
				context.EnvironmentData.Add(new KeyValuePair<string, object>("host.AppMode", "development"));
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000458C File Offset: 0x0000278C
		internal static bool DeconstructUrl(string url, out string scheme, out string host, out string port, out string path)
		{
			url = url ?? string.Empty;
			int delimiterStart = url.IndexOf(Uri.SchemeDelimiter, StringComparison.Ordinal);
			if (delimiterStart < 0)
			{
				scheme = null;
				host = null;
				port = null;
				path = null;
				return false;
			}
			int delimiterEnd = delimiterStart + Uri.SchemeDelimiter.Length;
			int delimiterStart2 = url.IndexOf("/", delimiterEnd, StringComparison.Ordinal);
			if (delimiterStart2 < 0)
			{
				delimiterStart2 = url.Length;
			}
			int delimiterStart3 = url.LastIndexOf(":", delimiterStart2 - 1, delimiterStart2 - delimiterEnd, StringComparison.Ordinal);
			int delimiterEnd2 = delimiterStart3 + ":".Length;
			if (delimiterStart3 < 0)
			{
				delimiterStart3 = delimiterStart2;
				delimiterEnd2 = delimiterStart2;
			}
			scheme = url.Substring(0, delimiterStart);
			string portString = url.Substring(delimiterEnd2, delimiterStart2 - delimiterEnd2);
			int ignored;
			if (int.TryParse(portString, NumberStyles.Integer, CultureInfo.InvariantCulture, out ignored))
			{
				host = url.Substring(delimiterEnd, delimiterStart3 - delimiterEnd);
				port = portString;
			}
			else
			{
				if (string.Equals(scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase))
				{
					port = "80";
				}
				else if (string.Equals(scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
				{
					port = "443";
				}
				else
				{
					port = string.Empty;
				}
				host = url.Substring(delimiterEnd, delimiterStart2 - delimiterEnd);
			}
			path = url.Substring(delimiterStart2);
			return true;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000046A0 File Offset: 0x000028A0
		private void EnableTracing(StartContext context)
		{
			TextWriterTraceListener textListener = new TextWriterTraceListener(context.TraceOutput, "HostingTraceListener");
			Trace.Listeners.Add(textListener);
			TraceSource source = new TraceSource("HostingTraceSource", SourceLevels.All);
			source.Listeners.Add(textListener);
			context.Builder.Properties["host.TraceOutput"] = context.TraceOutput;
			context.Builder.Properties["host.TraceSource"] = source;
			context.Builder.SetLoggerFactory(this._loggerFactory);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004728 File Offset: 0x00002928
		private static IDisposable EnableDisposing(StartContext context)
		{
			CancellationTokenSource cts = new CancellationTokenSource();
			context.Builder.Properties["host.OnAppDisposing"] = cts.Token;
			context.EnvironmentData.Add(new KeyValuePair<string, object>("host.OnAppDisposing", cts.Token));
			return new Disposable(delegate
			{
				cts.Cancel(false);
			});
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000047A4 File Offset: 0x000029A4
		private void ResolveServerFactory(StartContext context)
		{
			if (context.ServerFactory != null)
			{
				return;
			}
			string serverName = HostingEngine.DetermineOwinServer(context);
			context.ServerFactory = this._serverFactoryLoader.Load(serverName);
			if (context.ServerFactory == null)
			{
				throw new MissingMemberException(string.Format(CultureInfo.InvariantCulture, Resources.Exception_ServerNotFound, new object[] { serverName }));
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000047FC File Offset: 0x000029FC
		private static string DetermineOwinServer(StartContext context)
		{
			StartOptions options = context.Options;
			IDictionary<string, string> settings = context.Options.Settings;
			string serverName = options.ServerFactory;
			if (!string.IsNullOrWhiteSpace(serverName))
			{
				return serverName;
			}
			if (settings != null && settings.TryGetValue("owin:Server", out serverName) && !string.IsNullOrWhiteSpace(serverName))
			{
				return serverName;
			}
			serverName = Environment.GetEnvironmentVariable("OWIN_SERVER", EnvironmentVariableTarget.Process);
			if (!string.IsNullOrWhiteSpace(serverName))
			{
				return serverName;
			}
			return "Microsoft.Owin.Host.HttpListener";
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004864 File Offset: 0x00002A64
		private static int DeterminePort(StartContext context)
		{
			int port;
			if (!HostingEngine.TryDetermineCustomPort(context.Options, out port))
			{
				port = HostingEngine.DefaultPort;
			}
			return port;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004887 File Offset: 0x00002A87
		private static string GetPortEnvironmentVariable()
		{
			return Environment.GetEnvironmentVariable("PORT", EnvironmentVariableTarget.Process);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004894 File Offset: 0x00002A94
		private static string DetermineApplicationName(StartContext context)
		{
			StartOptions options = context.Options;
			IDictionary<string, string> settings = context.Options.Settings;
			if (options != null && !string.IsNullOrWhiteSpace(options.AppStartup))
			{
				return options.AppStartup;
			}
			string appName;
			if (settings.TryGetValue("owin:AppStartup", out appName) && !string.IsNullOrWhiteSpace(appName))
			{
				return appName;
			}
			return null;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000048E5 File Offset: 0x00002AE5
		private static void InitializeServerFactory(StartContext context)
		{
			context.ServerFactory.Initialize(context.Builder);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000048F8 File Offset: 0x00002AF8
		private void ResolveApp(StartContext context)
		{
			context.Builder.Use(typeof(Encapsulate), new object[] { context.EnvironmentData });
			if (context.App != null)
			{
				context.Builder.Use(new Func<object, object>((object _) => context.App), new object[0]);
				return;
			}
			IList<string> errors = new List<string>();
			if (context.Startup == null)
			{
				string appName = HostingEngine.DetermineApplicationName(context);
				context.Startup = this._appLoader.Load(appName, errors);
			}
			if (context.Startup == null)
			{
				throw new EntryPointNotFoundException(Resources.Exception_AppLoadFailure + Environment.NewLine + " - " + string.Join(Environment.NewLine + " - ", errors));
			}
			context.Startup(context.Builder);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004A03 File Offset: 0x00002C03
		private static IDisposable StartServer(StartContext context)
		{
			return context.ServerFactory.Create(context.Builder);
		}

		// Token: 0x0400003E RID: 62
		private readonly IAppBuilderFactory _appBuilderFactory;

		// Token: 0x0400003F RID: 63
		private readonly ITraceOutputFactory _traceOutputFactory;

		// Token: 0x04000040 RID: 64
		private readonly IAppLoader _appLoader;

		// Token: 0x04000041 RID: 65
		private readonly IServerFactoryLoader _serverFactoryLoader;

		// Token: 0x04000042 RID: 66
		private readonly ILoggerFactory _loggerFactory;
	}
}
