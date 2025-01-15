using System;
using System.Net.Http;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Internal;
using System.Web.Http.Properties;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x0200007F RID: 127
	public class DefaultHttpControllerActivator : IHttpControllerActivator
	{
		// Token: 0x06000333 RID: 819 RVA: 0x00009480 File Offset: 0x00007680
		public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (controllerDescriptor == null)
			{
				throw Error.ArgumentNull("controllerDescriptor");
			}
			if (controllerType == null)
			{
				throw Error.ArgumentNull("controllerType");
			}
			IHttpController httpController;
			try
			{
				Func<IHttpController> func;
				object obj;
				if (this._fastCache == null)
				{
					IHttpController instanceOrActivator = DefaultHttpControllerActivator.GetInstanceOrActivator(request, controllerType, out func);
					if (instanceOrActivator != null)
					{
						return instanceOrActivator;
					}
					Tuple<HttpControllerDescriptor, Func<IHttpController>> tuple = Tuple.Create<HttpControllerDescriptor, Func<IHttpController>>(controllerDescriptor, func);
					Interlocked.CompareExchange<Tuple<HttpControllerDescriptor, Func<IHttpController>>>(ref this._fastCache, tuple, null);
				}
				else if (this._fastCache.Item1 == controllerDescriptor)
				{
					func = this._fastCache.Item2;
				}
				else if (controllerDescriptor.Properties.TryGetValue(this._cacheKey, out obj))
				{
					func = (Func<IHttpController>)obj;
				}
				else
				{
					IHttpController instanceOrActivator2 = DefaultHttpControllerActivator.GetInstanceOrActivator(request, controllerType, out func);
					if (instanceOrActivator2 != null)
					{
						return instanceOrActivator2;
					}
					controllerDescriptor.Properties.TryAdd(this._cacheKey, func);
				}
				httpController = func();
			}
			catch (Exception ex)
			{
				throw Error.InvalidOperation(ex, SRResources.DefaultControllerFactory_ErrorCreatingController, new object[] { controllerType.Name });
			}
			return httpController;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00009588 File Offset: 0x00007788
		private static IHttpController GetInstanceOrActivator(HttpRequestMessage request, Type controllerType, out Func<IHttpController> activator)
		{
			IHttpController httpController = (IHttpController)request.GetDependencyScope().GetService(controllerType);
			if (httpController != null)
			{
				activator = null;
				return httpController;
			}
			activator = TypeActivator.Create<IHttpController>(controllerType);
			return null;
		}

		// Token: 0x040000B0 RID: 176
		private Tuple<HttpControllerDescriptor, Func<IHttpController>> _fastCache;

		// Token: 0x040000B1 RID: 177
		private object _cacheKey = new object();
	}
}
