using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.Request;
using Microsoft.Owin;

namespace Microsoft.BIServer.Owin.Common.Middleware
{
	// Token: 0x0200001E RID: 30
	public class RequestLoggingMiddleWare : OwinMiddleware
	{
		// Token: 0x06000086 RID: 134 RVA: 0x000035FD File Offset: 0x000017FD
		public RequestLoggingMiddleWare(OwinMiddleware next, ILogger logger)
			: base(next)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._logger = logger;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000361B File Offset: 0x0000181B
		public RequestLoggingMiddleWare(OwinMiddleware next, ILogger logger, bool isLogClientIPAddress)
			: base(next)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._logger = logger;
			this._isLogClientIPAddress = isLogClientIPAddress;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003640 File Offset: 0x00001840
		public override async Task Invoke(IOwinContext context)
		{
			string remoteIp = context.Request.RemoteIpAddress;
			string method = context.Request.Method;
			PathString path = context.Request.Path;
			using (ScopeMeter.Use(new string[]
			{
				"owin",
				base.GetType().Name
			}))
			{
				string text = context.Request.GetHeaderValue("RequestId");
				if (string.IsNullOrEmpty(text))
				{
					text = string.Format("s_{0}", Guid.NewGuid().ToString());
				}
				string text2 = WebUtility.UrlDecode(context.Request.GetHeaderValue("User-Agent"));
				RequestContext requestContext = new RequestContext(text, context.Request.GetHeaderValue("X-SSRS-ClientSessionId"), text2.Contains("Power BI"));
				string user = string.Empty;
				if (context.Authentication != null && context.Authentication.User != null && context.Authentication.User.Identity != null)
				{
					user = context.Authentication.User.Identity.Name;
				}
				Stopwatch stopwatch = Stopwatch.StartNew();
				try
				{
					requestContext.SaveOnCallContext();
					this._logger.Trace(TraceLevel.Info, string.Format("Received request {0} {1}", context.Request.Method, context.Request.Path));
					await base.Next.Invoke(context);
					stopwatch.Stop();
					TraceLevel traceLevel = (context.Request.Path.Value.Contains("/api/") ? TraceLevel.Info : TraceLevel.Verbose);
					if (this._isLogClientIPAddress)
					{
						this._logger.Trace(traceLevel, string.Format("Sending response. Response code {0} {3}: {1}, Elapsed time {2:g}", new object[]
						{
							user,
							context.Response.StatusCode,
							stopwatch.Elapsed,
							remoteIp
						}));
					}
					else
					{
						this._logger.Trace(traceLevel, string.Format("Sending response. Response code {0} {1}, Elapsed time {2:g}", user, context.Response.StatusCode, stopwatch.Elapsed));
					}
				}
				catch (OperationCanceledException)
				{
					stopwatch.Stop();
					this._logger.Trace(TraceLevel.Info, string.Format("Connection dropped. {0} {4}: {1} {2}, Elapsed time {3:g}", new object[] { user, method, path, stopwatch.Elapsed, remoteIp }));
				}
				catch (AggregateException ex)
				{
					if (!ex.Flatten().InnerExceptions.All((Exception p) => p is OperationCanceledException))
					{
						throw;
					}
					stopwatch.Stop();
					this._logger.Trace(TraceLevel.Info, string.Format("Connection dropped. {0} {4}: {1} {2}, Elapsed time {3:g}", new object[] { user, method, path, stopwatch.Elapsed, remoteIp }));
				}
				catch (Exception ex2)
				{
					stopwatch.Stop();
					this._logger.Trace(TraceLevel.Error, string.Format("{0} {4}: {1} {2} - {3:g}\r\nException: {5}", new object[] { user, method, path, stopwatch.Elapsed, remoteIp, ex2 }));
					throw;
				}
				user = null;
				stopwatch = null;
			}
			IDisposable disposable = null;
		}

		// Token: 0x04000055 RID: 85
		private readonly ILogger _logger;

		// Token: 0x04000056 RID: 86
		private bool _isLogClientIPAddress;
	}
}
