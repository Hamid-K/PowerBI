using System;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Web;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000011 RID: 17
	internal static class WebRequestUtil
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002596 File Offset: 0x00000796
		public static string ClientHostHeaderName
		{
			get
			{
				return WebRequestUtil.m_clientHostHeaderName;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000025A0 File Offset: 0x000007A0
		public static string GetHostFromRequest(HttpRequest request)
		{
			string text = request.Headers["host"];
			if (text == null || text.Length == 0)
			{
				string leftPart = request.Url.GetLeftPart(UriPartial.Authority);
				string leftPart2 = request.Url.GetLeftPart(UriPartial.Scheme);
				text = leftPart.Substring(leftPart2.Length);
			}
			return text;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000025F0 File Offset: 0x000007F0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		[PermissionSet(SecurityAction.Assert, Name = "FullTrust")]
		internal static void DeclareClientRequestAsLocal(bool isLocal)
		{
			bool? flag = new bool?(isLocal);
			CallContext.SetData("ReportingServices.IsLocalRequest", flag);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002618 File Offset: 0x00000818
		public static bool IsClientLocal()
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null)
			{
				bool? flag = (bool?)CallContext.GetData("ReportingServices.IsLocalRequest");
				return flag == null || flag.Value;
			}
			bool isLocal = httpContext.Request.IsLocal;
			bool flag2 = httpContext.Request.Headers[LocalClientConstants.ClientNotLocalHeaderName] == null;
			return isLocal && flag2;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002679 File Offset: 0x00000879
		public static bool IsClientLocal(HttpContext context)
		{
			return WebRequestUtil.IsClientLocal();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002680 File Offset: 0x00000880
		public static bool IsClientLocal(ILocalClient clientDetection)
		{
			if (clientDetection == null)
			{
				return WebRequestUtil.IsClientLocal();
			}
			return clientDetection.IsClientLocal;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002691 File Offset: 0x00000891
		public static bool IsViaPortal()
		{
			return WebRequestUtil.IsViaPortal(HttpContext.Current);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000269D File Offset: 0x0000089D
		public static bool IsViaPortal(HttpContext context)
		{
			return context != null && context.Request.IsLocal && context.Request.Headers["RSViaWebApp"] != null;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000026CB File Offset: 0x000008CB
		internal static string SharepointExternalReportServerUrl
		{
			get
			{
				return HttpUtility.UrlDecode(HttpContext.Current.Request.Headers["SharepointExternalReportServerUrl"]);
			}
		}

		// Token: 0x04000040 RID: 64
		private static readonly string m_clientHostHeaderName = "RSClientHostName";

		// Token: 0x04000041 RID: 65
		internal const string SharepointPreambleLengthHeaderName = "SharepointPreambleLength";

		// Token: 0x04000042 RID: 66
		internal const string SharepointExternalReportServerUrlHeaderName = "SharepointExternalReportServerUrl";

		// Token: 0x04000043 RID: 67
		internal const string SharepointClientHttpMethodHeaderName = "SharepointClientHttpMethod";
	}
}
