using System;
using System.Threading.Tasks;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.HostingInfo;
using Microsoft.Owin;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.BIServer.Owin.Common.Middleware
{
	// Token: 0x0200001C RID: 28
	public sealed class RequestThrottlingMiddleware : OwinMiddleware
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00003484 File Offset: 0x00001684
		public RequestThrottlingMiddleware(OwinMiddleware next)
			: base(next)
		{
			this._maxActiveReqForOneUser = StaticConfig.Current.GetIntOrDefault(ConfigSettings.MaxActiveReqForOneUser.ToString(), 20);
			this._maxActiveReqForAnonymous = StaticConfig.Current.GetIntOrDefault(ConfigSettings.MaxActiveReqForAnonymous.ToString(), 200);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000034DF File Offset: 0x000016DF
		public RequestThrottlingMiddleware(OwinMiddleware next, int maxActiveReqForOneUser, int maxActiveReqForAnonymous)
			: this(next)
		{
			this._maxActiveReqForOneUser = maxActiveReqForOneUser;
			this._maxActiveReqForAnonymous = maxActiveReqForAnonymous;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000034F6 File Offset: 0x000016F6
		public override Task Invoke(IOwinContext context)
		{
			return this.InvokeInternal(context);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003500 File Offset: 0x00001700
		internal async Task<int> InvokeInternal(IOwinContext context)
		{
			string requestUrl = context.Request.Uri.AbsoluteUri;
			string userName;
			using (ScopeMeter.Use(new string[]
			{
				"owin",
				base.GetType().Name
			}))
			{
				int num;
				if (context.Request.User != null)
				{
					userName = context.Request.User.Identity.Name;
					num = this._maxActiveReqForOneUser;
				}
				else
				{
					userName = "<Anonymous>";
					num = this._maxActiveReqForAnonymous;
				}
				if (!RunningRequests.Current.BeginRequest(requestUrl, userName, num))
				{
					Logger.Debug("Request rejected. Too many requests from user. User map:'{0}'", new object[] { RunningRequests.Current.UsersXml() });
					context.Response.StatusCode = 503;
					return 3;
				}
			}
			int num2;
			try
			{
				num2 = await this.InvokeNext(context);
			}
			finally
			{
				RunningRequests.Current.RemoveRequest(requestUrl, userName);
			}
			return num2;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003550 File Offset: 0x00001750
		private async Task<int> InvokeNext(IOwinContext context)
		{
			if (base.Next != null)
			{
				await base.Next.Invoke(context);
			}
			return 0;
		}

		// Token: 0x0400004F RID: 79
		private readonly int _maxActiveReqForOneUser;

		// Token: 0x04000050 RID: 80
		private readonly int _maxActiveReqForAnonymous;

		// Token: 0x04000051 RID: 81
		private const int RequestThrottledReturnCode = 3;

		// Token: 0x04000052 RID: 82
		private const int MaxActiveReqForOneUserDefault = 20;

		// Token: 0x04000053 RID: 83
		private const int MaxActiveReqForAnonymousDefault = 200;
	}
}
