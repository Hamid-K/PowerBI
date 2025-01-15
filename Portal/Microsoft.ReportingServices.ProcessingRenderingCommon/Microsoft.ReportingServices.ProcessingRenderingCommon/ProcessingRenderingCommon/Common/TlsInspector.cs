using System;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Authentication;
using Microsoft.ReportingServices.ProcessingRenderingCommon.Tracing;

namespace Microsoft.ReportingServices.ProcessingRenderingCommon.Common
{
	// Token: 0x020000D7 RID: 215
	public static class TlsInspector
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x00013D10 File Offset: 0x00011F10
		public static string GetTlsProtocol(HttpListenerRequest httpListenerRequest)
		{
			string text;
			try
			{
				if (httpListenerRequest.IsSecureConnection || httpListenerRequest.Url.Scheme == Uri.UriSchemeHttps)
				{
					text = HttpApi.GetTlsProtocolInfo(httpListenerRequest.GetRequestBuffer(), httpListenerRequest.GetOriginalBlobAddress()).ProtocolName();
				}
				else
				{
					text = "UnknownTLS";
				}
			}
			catch (Exception ex)
			{
				EngineTracer.Error(string.Format("Error retrieving SSL protocol from http response message. Exception: {0}", ex));
				text = "TlsDiscoveryError";
			}
			return text;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00013D8C File Offset: 0x00011F8C
		public static string GetTlsProtocol(WebResponse response)
		{
			if (response.GetResponseStream() == null)
			{
				return "NoContent";
			}
			return TlsInspector.GetTlsProtocol(response.GetResponseStream());
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00013DA8 File Offset: 0x00011FA8
		public static string GetTlsProtocol(HttpResponseMessage httpResponseMessage)
		{
			if (httpResponseMessage.Content == null)
			{
				return "NoContent";
			}
			if (httpResponseMessage.RequestMessage.RequestUri.Scheme != Uri.UriSchemeHttps)
			{
				return "NoTLS";
			}
			return TlsInspector.GetTlsProtocol(httpResponseMessage.Content.GetStream());
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00013DF8 File Offset: 0x00011FF8
		public static string GetTlsProtocol(Stream stream)
		{
			string text;
			try
			{
				if (stream == null)
				{
					text = "NoContent";
				}
				else
				{
					text = ((SslProtocols)TlsInspector.streamProtocolGetter(stream)).ToString();
				}
			}
			catch (Exception ex)
			{
				EngineTracer.Error(string.Format("Error retrieving SSL protocol from http response message. Exception: {0}", ex));
				text = "TlsDiscoveryError";
			}
			return text;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00013E5C File Offset: 0x0001205C
		static TlsInspector()
		{
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
			TlsInspector.httpListenerRequestRequestBufferGetter = (HttpListenerRequest httpListenerRequest) => null;
			TlsInspector.httpListenerRequestOriginalBlobAddressGetter = (HttpListenerRequest httpListenerRequest) => null;
			TlsInspector.httpContentStreamGetter = (HttpContent httpContent) => null;
			TlsInspector.streamProtocolGetter = (Stream stream) => null;
			try
			{
				PropertyInfo property = typeof(HttpListenerRequest).GetProperty("RequestBuffer", bindingFlags);
				Func<object, object> requestBufferGetter = TlsInspector.GetProperty(property);
				TlsInspector.httpListenerRequestRequestBufferGetter = (HttpListenerRequest httpListenerRequest) => requestBufferGetter(httpListenerRequest);
				PropertyInfo property2 = typeof(HttpListenerRequest).GetProperty("OriginalBlobAddress", bindingFlags);
				Func<object, object> originalBlobAddressGetter = TlsInspector.GetProperty(property2);
				TlsInspector.httpListenerRequestOriginalBlobAddressGetter = (HttpListenerRequest httpListenerRequest) => originalBlobAddressGetter(httpListenerRequest);
				Assembly assembly = typeof(HttpContent).Assembly;
				Type type = assembly.GetType("System.Net.Http.StreamContent");
				Type type2 = assembly.GetType("System.Net.Http.DelegatingStream");
				FieldInfo fieldInfo = type.GetField("content", bindingFlags);
				if (fieldInfo == null)
				{
					fieldInfo = type.GetField("_content", bindingFlags);
				}
				FieldInfo fieldInfo2 = type2.GetField("innerStream", bindingFlags);
				if (fieldInfo2 == null)
				{
					fieldInfo2 = type2.GetField("_innerStream", bindingFlags);
				}
				Func<object, object> contentGetter = TlsInspector.GetField(fieldInfo);
				Func<object, object> innerStreamGetter = TlsInspector.GetField(fieldInfo2);
				TlsInspector.httpContentStreamGetter = (HttpContent httpContent) => innerStreamGetter(contentGetter(httpContent));
				Assembly assembly2 = typeof(HttpWebRequest).Assembly;
				PropertyInfo property3 = assembly2.GetType("System.Net.ConnectStream").GetProperty("Connection", bindingFlags);
				PropertyInfo property4 = assembly2.GetType("System.Net.Connection").GetProperty("NetworkStream", bindingFlags);
				FieldInfo field = assembly2.GetType("System.Net.TlsStream").GetField("m_Worker", bindingFlags);
				PropertyInfo property5 = assembly2.GetType("System.Net.Security.SslState").GetProperty("SslProtocol", bindingFlags);
				Func<object, object> connectionGetter = TlsInspector.GetProperty(property3);
				Func<object, object> networkStreamGetter = TlsInspector.GetProperty(property4);
				Func<object, object> workerGetter = TlsInspector.GetField(field);
				Func<object, object> sslProtocolPropertyGetter = TlsInspector.GetProperty(property5);
				TlsInspector.streamProtocolGetter = (Stream stream) => sslProtocolPropertyGetter(workerGetter(networkStreamGetter(connectionGetter(stream))));
			}
			catch (Exception ex)
			{
				EngineTracer.Error(string.Format("Failed to initialize TlsUtils class. Exception: {0}", ex));
			}
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x000140DC File Offset: 0x000122DC
		private static byte[] GetRequestBuffer(this HttpListenerRequest httpListenerRequest)
		{
			return (byte[])TlsInspector.httpListenerRequestRequestBufferGetter(httpListenerRequest);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x000140EE File Offset: 0x000122EE
		private static IntPtr GetOriginalBlobAddress(this HttpListenerRequest httpListenerRequest)
		{
			return (IntPtr)TlsInspector.httpListenerRequestOriginalBlobAddressGetter(httpListenerRequest);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00014100 File Offset: 0x00012300
		private static Stream GetStream(this HttpContent httpContent)
		{
			return (Stream)TlsInspector.httpContentStreamGetter(httpContent);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00014114 File Offset: 0x00012314
		private static Func<object, object> GetProperty(PropertyInfo property)
		{
			ParameterExpression parameterExpression;
			return Expression.Lambda<Func<object, object>>(Expression.Condition(Expression.Call(Expression.Constant(property.DeclaringType), TlsInspector.isInstanceOf, new Expression[] { parameterExpression }), Expression.Convert(Expression.Property(Expression.Convert(parameterExpression, property.DeclaringType), property), typeof(object)), Expression.Constant(null, typeof(object))), new ParameterExpression[] { parameterExpression }).Compile();
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x000141A0 File Offset: 0x000123A0
		private static Func<object, object> GetField(FieldInfo field)
		{
			ParameterExpression parameterExpression;
			return Expression.Lambda<Func<object, object>>(Expression.Condition(Expression.Call(Expression.Constant(field.DeclaringType), TlsInspector.isInstanceOf, new Expression[] { parameterExpression }), Expression.Convert(Expression.Field(Expression.Convert(parameterExpression, field.DeclaringType), field), typeof(object)), Expression.Constant(null, typeof(object))), new ParameterExpression[] { parameterExpression }).Compile();
		}

		// Token: 0x0400043B RID: 1083
		private static MethodInfo isInstanceOf = typeof(Type).GetMethod("IsInstanceOfType", new Type[] { typeof(object) });

		// Token: 0x0400043C RID: 1084
		private static Func<HttpListenerRequest, object> httpListenerRequestRequestBufferGetter;

		// Token: 0x0400043D RID: 1085
		private static Func<HttpListenerRequest, object> httpListenerRequestOriginalBlobAddressGetter;

		// Token: 0x0400043E RID: 1086
		private static Func<HttpContent, object> httpContentStreamGetter;

		// Token: 0x0400043F RID: 1087
		private static Func<Stream, object> streamProtocolGetter;
	}
}
