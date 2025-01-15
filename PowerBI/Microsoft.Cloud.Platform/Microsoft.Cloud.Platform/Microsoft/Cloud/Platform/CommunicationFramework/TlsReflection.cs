using System;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Authentication;
using System.ServiceModel.Channels;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.CommunicationFramework
{
	// Token: 0x02000471 RID: 1137
	internal static class TlsReflection
	{
		// Token: 0x06002373 RID: 9075 RVA: 0x0008019C File Offset: 0x0007E39C
		static TlsReflection()
		{
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
			TlsReflection.httpContentStreamGetter = (HttpContent httpContent) => null;
			TlsReflection.streamProtocolGetter = (Stream stream) => null;
			TlsReflection.requestContextHttpListenerContextGetter = (RequestContext requestContext) => null;
			TlsReflection.httpListenerRequestRequestBufferGetter = (HttpListenerRequest httpListenerRequest) => null;
			TlsReflection.httpListenerRequestOriginalBlobAddressGetter = (HttpListenerRequest httpListenerRequest) => null;
			try
			{
				Assembly assembly = typeof(HttpContent).Assembly;
				Type type = assembly.GetType("System.Net.Http.StreamContent");
				Type type2 = assembly.GetType("System.Net.Http.DelegatingStream");
				FieldInfo field = type.GetField("content", bindingFlags);
				FieldInfo field2 = type2.GetField("innerStream", bindingFlags);
				Func<object, object> contentGetter = TlsReflection.GetField(field);
				Func<object, object> innerStreamGetter = TlsReflection.GetField(field2);
				TlsReflection.httpContentStreamGetter = (HttpContent httpContent) => innerStreamGetter(contentGetter(httpContent));
				Assembly assembly2 = typeof(HttpWebRequest).Assembly;
				Type type3 = assembly2.GetType("System.Net.ConnectStream");
				Type type4 = assembly2.GetType("System.Net.Connection");
				Type type5 = assembly2.GetType("System.Net.TlsStream");
				Type type6 = assembly2.GetType("System.Net.Security.SslState");
				PropertyInfo property = type3.GetProperty("Connection", bindingFlags);
				PropertyInfo property2 = type4.GetProperty("NetworkStream", bindingFlags);
				FieldInfo field3 = type5.GetField("m_Worker", bindingFlags);
				PropertyInfo property3 = type6.GetProperty("SslProtocol", bindingFlags);
				Func<object, object> connectionGetter = TlsReflection.GetProperty(property);
				Func<object, object> networkStreamGetter = TlsReflection.GetProperty(property2);
				Func<object, object> workerGetter = TlsReflection.GetField(field3);
				Func<object, object> sslProtocolPropertyGetter = TlsReflection.GetProperty(property3);
				TlsReflection.streamProtocolGetter = (Stream stream) => sslProtocolPropertyGetter(workerGetter(networkStreamGetter(connectionGetter(stream))));
				FieldInfo field4 = typeof(RequestContext).Assembly.GetType("System.ServiceModel.Channels.HttpRequestContext+ListenerHttpContext").GetField("listenerContext", bindingFlags);
				Func<object, object> listenerContextGetter = TlsReflection.GetField(field4);
				TlsReflection.requestContextHttpListenerContextGetter = (RequestContext requestContext) => listenerContextGetter(requestContext);
				Type type7 = assembly2.GetType("System.Net.HttpListenerRequest");
				PropertyInfo property4 = type7.GetProperty("RequestBuffer", bindingFlags);
				Func<object, object> requestBufferGetter = TlsReflection.GetProperty(property4);
				TlsReflection.httpListenerRequestRequestBufferGetter = (HttpListenerRequest httpListenerRequest) => requestBufferGetter(httpListenerRequest);
				PropertyInfo property5 = type7.GetProperty("OriginalBlobAddress", bindingFlags);
				Func<object, object> originalBlobAddressGetter = TlsReflection.GetProperty(property5);
				TlsReflection.httpListenerRequestOriginalBlobAddressGetter = (HttpListenerRequest httpListenerRequest) => originalBlobAddressGetter(httpListenerRequest);
			}
			catch (Exception ex)
			{
				TraceSourceBase<UtilsTrace>.Tracer.TraceError("Failed to initialize TlsReflection class.  Exception: {0}", new object[] { ex.ToString() });
			}
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x00080460 File Offset: 0x0007E660
		private static Func<object, object> GetProperty(PropertyInfo property)
		{
			ParameterExpression parameterExpression;
			return Expression.Lambda<Func<object, object>>(Expression.Condition(Expression.Call(Expression.Constant(property.DeclaringType), TlsReflection.isInstanceOf, new Expression[] { parameterExpression }), Expression.Convert(Expression.Property(Expression.Convert(parameterExpression, property.DeclaringType), property), typeof(object)), Expression.Constant(null, typeof(object))), new ParameterExpression[] { parameterExpression }).Compile();
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000804EC File Offset: 0x0007E6EC
		private static Func<object, object> GetField(FieldInfo field)
		{
			ParameterExpression parameterExpression;
			return Expression.Lambda<Func<object, object>>(Expression.Condition(Expression.Call(Expression.Constant(field.DeclaringType), TlsReflection.isInstanceOf, new Expression[] { parameterExpression }), Expression.Convert(Expression.Field(Expression.Convert(parameterExpression, field.DeclaringType), field), typeof(object)), Expression.Constant(null, typeof(object))), new ParameterExpression[] { parameterExpression }).Compile();
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x00080577 File Offset: 0x0007E777
		internal static Stream GetStream(this HttpContent httpContent)
		{
			return (Stream)TlsReflection.httpContentStreamGetter(httpContent);
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x0008058C File Offset: 0x0007E78C
		internal static string GetSslProtocol(this Stream stream)
		{
			return ((SslProtocols)TlsReflection.streamProtocolGetter(stream)).ToString();
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x000805B7 File Offset: 0x0007E7B7
		internal static HttpListenerContext GetHttpListenerContext(this RequestContext requestContext)
		{
			return (HttpListenerContext)TlsReflection.requestContextHttpListenerContextGetter(requestContext);
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x000805C9 File Offset: 0x0007E7C9
		internal static byte[] GetRequestBuffer(this HttpListenerRequest httpListenerRequest)
		{
			return (byte[])TlsReflection.httpListenerRequestRequestBufferGetter(httpListenerRequest);
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x000805DB File Offset: 0x0007E7DB
		internal static IntPtr GetOriginalBlobAddress(this HttpListenerRequest httpListenerRequest)
		{
			return (IntPtr)TlsReflection.httpListenerRequestOriginalBlobAddressGetter(httpListenerRequest);
		}

		// Token: 0x04000C5A RID: 3162
		private static MethodInfo isInstanceOf = typeof(Type).GetMethod("IsInstanceOfType", new Type[] { typeof(object) });

		// Token: 0x04000C5B RID: 3163
		private static Func<Stream, object> streamProtocolGetter;

		// Token: 0x04000C5C RID: 3164
		private static Func<HttpContent, object> httpContentStreamGetter;

		// Token: 0x04000C5D RID: 3165
		private static Func<RequestContext, object> requestContextHttpListenerContextGetter;

		// Token: 0x04000C5E RID: 3166
		private static Func<HttpListenerRequest, object> httpListenerRequestRequestBufferGetter;

		// Token: 0x04000C5F RID: 3167
		private static Func<HttpListenerRequest, object> httpListenerRequestOriginalBlobAddressGetter;
	}
}
