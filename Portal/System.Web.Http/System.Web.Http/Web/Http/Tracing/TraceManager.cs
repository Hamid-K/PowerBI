using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Services;
using System.Web.Http.Tracing.Tracers;

namespace System.Web.Http.Tracing
{
	// Token: 0x0200011B RID: 283
	internal class TraceManager : ITraceManager
	{
		// Token: 0x06000782 RID: 1922 RVA: 0x00012D54 File Offset: 0x00010F54
		public void Initialize(HttpConfiguration configuration)
		{
			ITraceWriter traceWriter = configuration.Services.GetTraceWriter();
			if (traceWriter != null)
			{
				TraceManager.CreateAllTracers(configuration, traceWriter);
			}
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00012D78 File Offset: 0x00010F78
		private static void CreateAllTracers(HttpConfiguration configuration, ITraceWriter traceWriter)
		{
			TraceManager.CreateActionInvokerTracer(configuration, traceWriter);
			TraceManager.CreateActionSelectorTracer(configuration, traceWriter);
			TraceManager.CreateActionValueBinderTracer(configuration, traceWriter);
			TraceManager.CreateContentNegotiatorTracer(configuration, traceWriter);
			TraceManager.CreateControllerActivatorTracer(configuration, traceWriter);
			TraceManager.CreateControllerSelectorTracer(configuration, traceWriter);
			TraceManager.CreateHttpControllerTypeResolverTracer(configuration, traceWriter);
			TraceManager.CreateMessageHandlerTracers(configuration, traceWriter);
			TraceManager.CreateMediaTypeFormatterTracers(configuration, traceWriter);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00012DC4 File Offset: 0x00010FC4
		private static TService GetService<TService>(ServicesContainer services)
		{
			return (TService)((object)services.GetService(typeof(TService)));
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00012DDC File Offset: 0x00010FDC
		private static void CreateActionInvokerTracer(HttpConfiguration configuration, ITraceWriter traceWriter)
		{
			IHttpActionInvoker service = TraceManager.GetService<IHttpActionInvoker>(configuration.Services);
			if (service != null && !(service is HttpActionInvokerTracer))
			{
				HttpActionInvokerTracer httpActionInvokerTracer = new HttpActionInvokerTracer(service, traceWriter);
				configuration.Services.Replace(typeof(IHttpActionInvoker), httpActionInvokerTracer);
			}
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00012E20 File Offset: 0x00011020
		private static void CreateActionSelectorTracer(HttpConfiguration configuration, ITraceWriter traceWriter)
		{
			IHttpActionSelector service = TraceManager.GetService<IHttpActionSelector>(configuration.Services);
			if (service != null && !(service is HttpActionSelectorTracer))
			{
				HttpActionSelectorTracer httpActionSelectorTracer = new HttpActionSelectorTracer(service, traceWriter);
				configuration.Services.Replace(typeof(IHttpActionSelector), httpActionSelectorTracer);
			}
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00012E64 File Offset: 0x00011064
		private static void CreateActionValueBinderTracer(HttpConfiguration configuration, ITraceWriter traceWriter)
		{
			IActionValueBinder service = TraceManager.GetService<IActionValueBinder>(configuration.Services);
			if (service != null && !(service is ActionValueBinderTracer))
			{
				ActionValueBinderTracer actionValueBinderTracer = new ActionValueBinderTracer(service, traceWriter);
				configuration.Services.Replace(typeof(IActionValueBinder), actionValueBinderTracer);
			}
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00012EA8 File Offset: 0x000110A8
		private static void CreateContentNegotiatorTracer(HttpConfiguration configuration, ITraceWriter traceWriter)
		{
			IContentNegotiator contentNegotiator = configuration.Services.GetContentNegotiator();
			if (contentNegotiator != null && !(contentNegotiator is ContentNegotiatorTracer))
			{
				ContentNegotiatorTracer contentNegotiatorTracer = new ContentNegotiatorTracer(contentNegotiator, traceWriter);
				configuration.Services.Replace(typeof(IContentNegotiator), contentNegotiatorTracer);
			}
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00012EEC File Offset: 0x000110EC
		private static void CreateControllerActivatorTracer(HttpConfiguration configuration, ITraceWriter traceWriter)
		{
			IHttpControllerActivator service = TraceManager.GetService<IHttpControllerActivator>(configuration.Services);
			if (service != null && !(service is HttpControllerActivatorTracer))
			{
				HttpControllerActivatorTracer httpControllerActivatorTracer = new HttpControllerActivatorTracer(service, traceWriter);
				configuration.Services.Replace(typeof(IHttpControllerActivator), httpControllerActivatorTracer);
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00012F30 File Offset: 0x00011130
		private static void CreateControllerSelectorTracer(HttpConfiguration configuration, ITraceWriter traceWriter)
		{
			IHttpControllerSelector httpControllerSelector = configuration.Services.GetHttpControllerSelector();
			if (httpControllerSelector != null && !(httpControllerSelector is HttpControllerSelectorTracer))
			{
				HttpControllerSelectorTracer httpControllerSelectorTracer = new HttpControllerSelectorTracer(httpControllerSelector, traceWriter);
				configuration.Services.Replace(typeof(IHttpControllerSelector), httpControllerSelectorTracer);
			}
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00012F74 File Offset: 0x00011174
		private static void CreateHttpControllerTypeResolverTracer(HttpConfiguration configuration, ITraceWriter traceWriter)
		{
			DefaultHttpControllerTypeResolver defaultHttpControllerTypeResolver = configuration.Services.GetHttpControllerTypeResolver() as DefaultHttpControllerTypeResolver;
			if (defaultHttpControllerTypeResolver != null)
			{
				IHttpControllerTypeResolver httpControllerTypeResolver = new DefaultHttpControllerTypeResolverTracer(defaultHttpControllerTypeResolver, traceWriter);
				configuration.Services.Replace(typeof(IHttpControllerTypeResolver), httpControllerTypeResolver);
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00012FB4 File Offset: 0x000111B4
		private static void CreateMediaTypeFormatterTracers(HttpConfiguration configuration, ITraceWriter traceWriter)
		{
			for (int i = 0; i < configuration.Formatters.Count; i++)
			{
				if (!(configuration.Formatters[i] is IFormatterTracer))
				{
					configuration.Formatters[i] = MediaTypeFormatterTracer.CreateTracer(configuration.Formatters[i], traceWriter, null);
				}
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001300C File Offset: 0x0001120C
		private static void CreateMessageHandlerTracers(HttpConfiguration configuration, ITraceWriter traceWriter)
		{
			int num = configuration.MessageHandlers.Count;
			if (num > 0 && configuration.MessageHandlers[0].InnerHandler != null)
			{
				return;
			}
			if (!TraceManager.AreMessageHandlerTracersRegistered(configuration.MessageHandlers))
			{
				for (int i = num - 1; i >= 0; i--)
				{
					if (configuration.MessageHandlers[i] is RequestMessageHandlerTracer || configuration.MessageHandlers[i] is MessageHandlerTracer)
					{
						configuration.MessageHandlers.RemoveAt(i);
					}
				}
				num = configuration.MessageHandlers.Count;
				for (int j = 0; j < num * 2; j += 2)
				{
					DelegatingHandler delegatingHandler = new MessageHandlerTracer(configuration.MessageHandlers[j], traceWriter);
					configuration.MessageHandlers.Insert(j, delegatingHandler);
				}
				configuration.MessageHandlers.Insert(0, new RequestMessageHandlerTracer(traceWriter));
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x000130DC File Offset: 0x000112DC
		private static bool AreMessageHandlerTracersRegistered(Collection<DelegatingHandler> messageHandlers)
		{
			int count = messageHandlers.Count;
			if (count == 0)
			{
				return false;
			}
			if (!(messageHandlers[0] is RequestMessageHandlerTracer))
			{
				return false;
			}
			if (count % 2 != 1)
			{
				return false;
			}
			for (int i = 2; i < count; i += 2)
			{
				DelegatingHandler delegatingHandler = messageHandlers[i - 1];
				DelegatingHandler delegatingHandler2 = messageHandlers[i];
				if (!(delegatingHandler is MessageHandlerTracer))
				{
					return false;
				}
				if (Decorator.GetInner<DelegatingHandler>(delegatingHandler) != delegatingHandler2)
				{
					return false;
				}
			}
			return true;
		}
	}
}
