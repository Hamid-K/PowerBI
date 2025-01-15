using System;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;
using System.Web.Services.Protocols;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Diagnostics.Utils;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200000D RID: 13
	internal static class ExceptionExtensions
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000022C4 File Offset: 0x000004C4
		public static SoapException WebServerToSoapException(this RSException exception, bool recycleOnSevereErrors, bool enableRemoteErrors)
		{
			if (recycleOnSevereErrors)
			{
				NativeMethodsGeneral.RecycleOnSevereException(exception);
			}
			XmlQualifiedName xmlQualifiedName = SoapException.ClientFaultCode;
			if (exception.Code == ErrorCode.rsInternalError)
			{
				xmlQualifiedName = SoapException.ServerFaultCode;
			}
			bool flag = enableRemoteErrors || ExceptionExtensions.IsClientLocal();
			StringBuilder stringBuilder;
			XmlNode xmlNode = exception.DetailsAsXml(flag, out stringBuilder);
			return new SoapException(stringBuilder.ToString(), xmlQualifiedName, exception.ActorUri, xmlNode);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000231C File Offset: 0x0000051C
		private static bool IsClientLocal()
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null)
			{
				bool? flag = (bool?)CallContext.GetData("ReportingServices.IsLocalRequest");
				return flag == null || flag.Value;
			}
			bool isLocal = httpContext.Request.IsLocal;
			bool flag2 = httpContext.Request.Headers["RSClientNotLocalHeader"] == null;
			return isLocal && flag2;
		}
	}
}
