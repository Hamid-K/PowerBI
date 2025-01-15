using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Http.Dependencies
{
	// Token: 0x02000088 RID: 136
	internal class EmptyResolver : IDependencyResolver, IDependencyScope, IDisposable
	{
		// Token: 0x0600035D RID: 861 RVA: 0x00003AA7 File Offset: 0x00001CA7
		private EmptyResolver()
		{
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00009F1F File Offset: 0x0000811F
		public static IDependencyResolver Instance
		{
			get
			{
				return EmptyResolver._instance;
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00009F26 File Offset: 0x00008126
		public IDependencyScope BeginScope()
		{
			return this;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00005744 File Offset: 0x00003944
		public void Dispose()
		{
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000413B File Offset: 0x0000233B
		public object GetService(Type serviceType)
		{
			return null;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00009F29 File Offset: 0x00008129
		public IEnumerable<object> GetServices(Type serviceType)
		{
			return Enumerable.Empty<object>();
		}

		// Token: 0x040000BF RID: 191
		private static readonly IDependencyResolver _instance = new EmptyResolver();
	}
}
