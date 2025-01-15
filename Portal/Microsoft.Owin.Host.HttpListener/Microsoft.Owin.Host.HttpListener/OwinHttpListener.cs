using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Host.HttpListener.RequestProcessing;

namespace Microsoft.Owin.Host.HttpListener
{
	// Token: 0x02000007 RID: 7
	public sealed class OwinHttpListener : IDisposable
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002424 File Offset: 0x00000624
		internal OwinHttpListener()
		{
			this._listener = new HttpListener();
			this._startNextRequestAsync = new Action(this.ProcessRequestsAsync);
			this._startNextRequestError = new Action<Task>(this.StartNextRequestError);
			this.SetRequestProcessingLimits(OwinHttpListener.DefaultMaxAccepts, int.MaxValue);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002476 File Offset: 0x00000676
		public HttpListener Listener
		{
			get
			{
				return this._listener;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002480 File Offset: 0x00000680
		private bool CanAcceptMoreRequests
		{
			get
			{
				PumpLimits limits = this._pumpLimits;
				return this._currentOutstandingAccepts < limits.MaxOutstandingAccepts && this._currentOutstandingRequests < limits.MaxOutstandingRequests - this._currentOutstandingAccepts;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024B9 File Offset: 0x000006B9
		public void SetRequestProcessingLimits(int maxAccepts, int maxRequests)
		{
			this._pumpLimits = new PumpLimits(maxAccepts, maxRequests);
			if (this._listener.IsListening)
			{
				this.OffloadStartNextRequest();
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024DC File Offset: 0x000006DC
		public void GetRequestProcessingLimits(out int maxAccepts, out int maxRequests)
		{
			PumpLimits limits = this._pumpLimits;
			maxAccepts = limits.MaxOutstandingAccepts;
			maxRequests = limits.MaxOutstandingRequests;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002500 File Offset: 0x00000700
		public void SetRequestQueueLimit(long limit)
		{
			if (limit <= 0L)
			{
				throw new ArgumentOutOfRangeException("limit", limit, string.Empty);
			}
			if ((this._requestQueueLength == null && limit == 1000L) || (this._requestQueueLength != null && limit == this._requestQueueLength.Value))
			{
				return;
			}
			this._requestQueueLength = new long?(limit);
			this.SetRequestQueueLimit();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000256C File Offset: 0x0000076C
		private void SetRequestQueueLimit()
		{
			if (OwinHttpListener.IsMono || !this._listener.IsListening || this._requestQueueLength == null || Environment.OSVersion.Version.Major < 6)
			{
				return;
			}
			NativeMethods.SetRequestQueueLength(this._listener, this._requestQueueLength.Value);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000025C4 File Offset: 0x000007C4
		internal void Start(HttpListener listener, Func<IDictionary<string, object>, Task> appFunc, IList<IDictionary<string, object>> addresses, IDictionary<string, object> capabilities, Func<string, Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool>> loggerFactory)
		{
			this._listener = listener;
			this._appFunc = appFunc;
			this._logger = LogHelper.CreateLogger(loggerFactory, typeof(OwinHttpListener));
			this._basePaths = new List<string>();
			foreach (IDictionary<string, object> address in addresses)
			{
				string scheme = address.Get("scheme") ?? Uri.UriSchemeHttp;
				string host = address.Get("host") ?? "localhost";
				string port = address.Get("port") ?? "5000";
				string path = address.Get("path") ?? string.Empty;
				if (!string.IsNullOrWhiteSpace(port))
				{
					port = ":" + port;
				}
				if (!path.EndsWith("/", StringComparison.Ordinal))
				{
					path += "/";
				}
				this._basePaths.Add(path);
				string url = string.Concat(new string[] { scheme, "://", host, port, path });
				this._listener.Prefixes.Add(url);
			}
			this._capabilities = capabilities;
			if (!this._listener.IsListening)
			{
				this._listener.Start();
			}
			this.SetRequestQueueLimit();
			this._disconnectHandler = new DisconnectHandler(this._listener, this._logger);
			this.OffloadStartNextRequest();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002750 File Offset: 0x00000950
		private void OffloadStartNextRequest()
		{
			if (this._listener.IsListening && this.CanAcceptMoreRequests)
			{
				Task.Factory.StartNew(this._startNextRequestAsync).ContinueWith(this._startNextRequestError, TaskContinuationOptions.OnlyOnFaulted);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002788 File Offset: 0x00000988
		private async void ProcessRequestsAsync()
		{
			while (this._listener.IsListening && this.CanAcceptMoreRequests)
			{
				Interlocked.Increment(ref this._currentOutstandingAccepts);
				HttpListenerContext context;
				try
				{
					HttpListenerContext httpListenerContext = await this._listener.GetContextAsync();
					context = httpListenerContext;
				}
				catch (Exception ex)
				{
					Interlocked.Decrement(ref this._currentOutstandingAccepts);
					LogHelper.LogException(this._logger, "Accept", ex);
					continue;
				}
				Interlocked.Decrement(ref this._currentOutstandingAccepts);
				Interlocked.Increment(ref this._currentOutstandingRequests);
				this.OffloadStartNextRequest();
				await this.ProcessRequestAsync(context);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000027C0 File Offset: 0x000009C0
		private async Task ProcessRequestAsync(HttpListenerContext context)
		{
			OwinHttpListenerContext owinContext = null;
			try
			{
				string pathBase;
				string path;
				string query;
				this.GetPathAndQuery(context.Request, out pathBase, out path, out query);
				owinContext = new OwinHttpListenerContext(context, pathBase, path, query, this._disconnectHandler);
				this.PopulateServerKeys(owinContext.Environment);
				await this._appFunc(owinContext.Environment);
				await owinContext.Response.CompleteResponseAsync();
				owinContext.Response.Close();
				owinContext.End();
				owinContext.Dispose();
				Interlocked.Decrement(ref this._currentOutstandingRequests);
			}
			catch (Exception ex)
			{
				Interlocked.Decrement(ref this._currentOutstandingRequests);
				LogHelper.LogException(this._logger, "Exception during request processing.", ex);
				if (owinContext != null)
				{
					owinContext.End(ex);
					owinContext.Dispose();
				}
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000280B File Offset: 0x00000A0B
		private void StartNextRequestError(Task faultedTask)
		{
			LogHelper.LogException(this._logger, "Unexpected exception.", faultedTask.Exception);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002824 File Offset: 0x00000A24
		private void GetPathAndQuery(HttpListenerRequest request, out string pathBase, out string path, out string query)
		{
			string cookedPath;
			if (OwinHttpListener.IsMono)
			{
				cookedPath = "/" + request.Url.GetComponents(UriComponents.Path, UriFormat.SafeUnescaped);
				query = request.Url.Query;
			}
			else
			{
				cookedPath = ((string)OwinHttpListener.CookedPathField.GetValue(request)) ?? string.Empty;
				query = ((string)OwinHttpListener.CookedQueryField.GetValue(request)) ?? string.Empty;
			}
			if (!string.IsNullOrEmpty(query) && query[0] == '?')
			{
				query = query.Substring(1);
			}
			bool endsInSlash = true;
			string bestMatch = "/";
			for (int i = 0; i < this._basePaths.Count; i++)
			{
				string pathTest = this._basePaths[i];
				if (pathTest.Length > bestMatch.Length)
				{
					if (pathTest.Length <= cookedPath.Length && cookedPath.StartsWith(pathTest, StringComparison.OrdinalIgnoreCase))
					{
						bestMatch = pathTest;
						endsInSlash = true;
					}
					else if (pathTest.Length == cookedPath.Length + 1 && string.Compare(pathTest, 0, cookedPath, 0, cookedPath.Length, StringComparison.OrdinalIgnoreCase) == 0)
					{
						bestMatch = pathTest;
						endsInSlash = false;
					}
				}
			}
			if (endsInSlash)
			{
				pathBase = cookedPath.Substring(0, bestMatch.Length - 1);
				path = cookedPath.Substring(bestMatch.Length - 1);
				return;
			}
			pathBase = cookedPath;
			path = string.Empty;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000296E File Offset: 0x00000B6E
		private void PopulateServerKeys(CallEnvironment env)
		{
			env.ServerCapabilities = this._capabilities;
			env.Listener = this._listener;
			env.OwinHttpListener = this;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002990 File Offset: 0x00000B90
		internal void Stop()
		{
			try
			{
				this._listener.Stop();
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000029C0 File Offset: 0x00000BC0
		public void Dispose()
		{
			if (this._listener.IsListening)
			{
				this._listener.Stop();
			}
			((IDisposable)this._listener).Dispose();
		}

		// Token: 0x04000035 RID: 53
		private const int DefaultMaxRequests = 2147483647;

		// Token: 0x04000036 RID: 54
		private const long DefaultRequestQueueLength = 1000L;

		// Token: 0x04000037 RID: 55
		private static readonly int DefaultMaxAccepts = 5 * Environment.ProcessorCount;

		// Token: 0x04000038 RID: 56
		private static readonly bool IsMono = Type.GetType("Mono.Runtime") != null;

		// Token: 0x04000039 RID: 57
		private static readonly FieldInfo CookedPathField = typeof(HttpListenerRequest).GetField("m_CookedUrlPath", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x0400003A RID: 58
		private static readonly FieldInfo CookedQueryField = typeof(HttpListenerRequest).GetField("m_CookedUrlQuery", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x0400003B RID: 59
		private Action _startNextRequestAsync;

		// Token: 0x0400003C RID: 60
		private Action<Task> _startNextRequestError;

		// Token: 0x0400003D RID: 61
		private HttpListener _listener;

		// Token: 0x0400003E RID: 62
		private IList<string> _basePaths;

		// Token: 0x0400003F RID: 63
		private Func<IDictionary<string, object>, Task> _appFunc;

		// Token: 0x04000040 RID: 64
		private DisconnectHandler _disconnectHandler;

		// Token: 0x04000041 RID: 65
		private IDictionary<string, object> _capabilities;

		// Token: 0x04000042 RID: 66
		private PumpLimits _pumpLimits;

		// Token: 0x04000043 RID: 67
		private int _currentOutstandingAccepts;

		// Token: 0x04000044 RID: 68
		private int _currentOutstandingRequests;

		// Token: 0x04000045 RID: 69
		private Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool> _logger;

		// Token: 0x04000046 RID: 70
		private long? _requestQueueLength;
	}
}
